using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitness.Integration.OpenVidu;
using CoronaFitness.Integration.OpenVidu.Models;
using CoronaFitnessBL.Exceptions;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.Models;
using CoronaFitnessBL.User.UserContext;
using CoronaFitnessDb;
using CoronaFitnessDb.Entities;
using MongoDB.Driver;

namespace CoronaFitnessBL.Meeting
{
    public class FxMeetingBusinessOperations : IxMeetingBusinessOperations
    {
        private readonly IxMongoDataContext dbContext;
        private readonly IxOpenViduGateway ovGateway;

        public FxMeetingBusinessOperations(IxMongoDataContext dbContext, IxOpenViduGateway ovGateway)
        {
            this.dbContext = dbContext;
            this.ovGateway = ovGateway;
        }

        public Task<List<FxMeetingModel>> GetMeetings(FxUserModel user)
        {
            return dbContext.Meetings
                .GetAsync(meeting => meeting.OwnerId == user.Id
                                     || meeting.Attendees.Any(a => a.UserId == user.Id))
                .ContinueWith(x => x.Result.Select(dbMeeting =>
                    new FxMeetingModel(dbMeeting)).ToList());
        }

        public async Task<FxMeetingModel> GetMeeting(string id, string userId)
        {
            var meeting = await dbContext.Meetings
                .GetSingleAsync(m => m.Id == id);

            if (!IsAllowedToSeeMeeting(meeting, userId))
                return null;

            return new FxMeetingModel(meeting);
        }

        public Task CreateMeeting(FxMeetingModel meeting)
        {
            if (meeting.Attendees.All(x => x.UserId != meeting.OwnerId))
                meeting.Attendees.Add(new FxMeetingAttendeeModel()
                    {UserId = meeting.OwnerId, Role = EnOvSessionRole.MODERATOR});

            return dbContext.Meetings.AddAsync(meeting.ToDbModel());
        }

        public async Task<string> GetToken(string meetingId, string userId)
        {
            var meeting = await this.dbContext.Meetings
                .GetSingleAsync(m => m.Id == meetingId &&
                                     m.Attendees.Any(a => a.UserId == userId));

            if (meeting == null) throw new ExNotFoundException<FxMeeting>();

            var user = await this.dbContext.Users.GetSingleAsync(x => x.Id == userId);
            var attendee = meeting.Attendees.Single(x => x.UserId == userId);

            //if no session, then create it
            if (string.IsNullOrEmpty(meeting.SessionId))
                meeting.SessionId = await this.CreateSession();

            //and generate a new token
            var token = await this.CreateToken(meeting.SessionId, user.Name,
                Enum.Parse<EnOvSessionRole>(attendee.Role, true));

            await this.dbContext.Meetings.UpdateAsync(x => x.Id == meeting.Id,
                new UpdateDefinitionBuilder<FxMeeting>()
                    .Set(x => x.SessionId, meeting.SessionId));

            return token;
        }

        private async Task<string> CreateSession()
        {
            var result = await this.ovGateway.CreateSession(CreateSessionRequest.Empty);
            return result.Id;
        }

        private async Task<string> CreateToken(string sessionId, string userName,
            EnOvSessionRole role = EnOvSessionRole.PUBLISHER)
        {
            var result = await this.ovGateway.CreateToken(new CreateTokenRequest()
            {
                Session = sessionId,
                Data = userName,
                Role = role
            });

            return result.Token;
        }

        private bool IsAllowedToSeeMeeting(FxMeeting meeting, string userId)
        {
            return meeting.OwnerId == userId || meeting.Attendees.Any(a => a.UserId == userId);
        }
    }
}