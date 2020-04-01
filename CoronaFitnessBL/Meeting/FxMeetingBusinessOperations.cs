using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitness.Integration.OpenVidu;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.Models;
using CoronaFitnessBL.User.UserContext;
using CoronaFitnessDb;
using CoronaFitnessDb.Entities;

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
                .GetAsync(meeting => meeting.OwnerId == user.Id)
                .ContinueWith(x => x.Result.Select(dbMeeting =>
                    new FxMeetingModel(dbMeeting)).ToList());
        }

        public Task CreateMeeting(FxMeetingModel meeting)
        {
            if (meeting.Attendees.All(x => x.UserId != meeting.OwnerId))
                meeting.Attendees.Add(new FxMeetingAttendeeModel() {UserId = meeting.OwnerId});
            
            return dbContext.Meetings.AddAsync(new FxMeeting()
            {
                Id = meeting.Id,
                Title = meeting.Title,
                Description = meeting.Description,
                OwnerId = meeting.OwnerId,
                SessionId = meeting.SessionId ?? string.Empty,
                Attendees = meeting.Attendees.Select(a => new FxMeetingAttendee()
                {
                    UserId = a.UserId,
                    Token = a.Token ?? string.Empty
                }).ToList()
            });
        }

        public async Task<string> GetToken(string meetingId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}