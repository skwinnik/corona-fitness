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

        public async Task<FxMeetingModel> GetMeeting(string id, string userId = null)
        {
            var meeting = await GetMeetingDb(id);

            if (userId != null && !IsAllowedToSeeMeeting(meeting, userId))
                throw new ExNotFoundException<FxMeeting>();

            return new FxMeetingModel(meeting);
        }

        public async Task<List<FxMeetingAttendeeModel>> GetAttendees(string id, string userId)
        {
            var meeting = await this.GetMeeting(id, userId);
            return meeting.Attendees.Select(x => new FxMeetingAttendeeModel() {UserId = x.UserId}).ToList();
        }

        public async Task RequestToAttend(string id, string userId)
        {
            var meeting = await GetMeetingDb(id);

            if (meeting == null || !IsAllowedToSeeMeeting(meeting, userId))
                throw new ExNotFoundException<FxMeeting>();

            if (meeting.AttendeeRequests.Any(a => a.UserId == userId))
                return;

            meeting.AttendeeRequests.Add(new FxMeetingAttendeeRequest() {UserId = userId});

            await dbContext.Meetings.UpdateAsync(m => m.Id == meeting.Id,
                new UpdateDefinitionBuilder<FxMeeting>()
                    .Set(x => x.AttendeeRequests, meeting.AttendeeRequests));
        }

        public async Task<List<FxMeetingAttendeeRequestModel>> GetRequestsToAttend(string id, string userId)
        {
            var meeting = await GetMeetingDb(id);
            if (meeting.OwnerId != userId)
                return null;

            return meeting.AttendeeRequests.Select(x => new FxMeetingAttendeeRequestModel(x)).ToList();
        }

        public async Task ApproveRequestToAttend(string id, string userId, string ownerId)
        {
            var meeting = await GetMeetingDb(id);
            if (meeting.OwnerId != ownerId)
                return;

            var attendeeRequst = meeting.AttendeeRequests.Single(x => x.UserId == userId);
            meeting.AttendeeRequests.Remove(attendeeRequst);
            meeting.Attendees.Add(new FxMeetingAttendee()
            {
                UserId = attendeeRequst.UserId,
                Role = EnOvSessionRole.PUBLISHER.ToString()
            });

            await this.dbContext.Meetings.UpdateAsync(m => m.Id == id,
                new UpdateDefinitionBuilder<FxMeeting>()
                    .Set(x => x.Attendees, meeting.Attendees)
                    .Set(x => x.AttendeeRequests, meeting.AttendeeRequests));
        }

        public async Task RejectRequestToAttend(string id, string userId, string ownerId)
        {
            var meeting = await GetMeetingDb(id);
            if (meeting.OwnerId != ownerId)
                return;

            var attendeeRequst = meeting.AttendeeRequests.Single(x => x.UserId == userId);
            meeting.AttendeeRequests.Remove(attendeeRequst);

            await this.dbContext.Meetings.UpdateAsync(m => m.Id == id,
                new UpdateDefinitionBuilder<FxMeeting>()
                    .Set(x => x.AttendeeRequests, meeting.AttendeeRequests));
        }

        public async Task RemoveAttendee(string id, string attendeeId, string ownerId)
        {
            var meeting = await GetMeetingDb(id);
            if (meeting.OwnerId != ownerId || attendeeId == ownerId)
                return;

            var attendee = meeting.Attendees.Find(x => x.UserId == attendeeId);
            meeting.Attendees.Remove(attendee);

            await this.dbContext.Meetings.UpdateAsync(m => m.Id == id,
                new UpdateDefinitionBuilder<FxMeeting>()
                    .Set(x => x.Attendees, meeting.Attendees));
        }

        public Task CreateMeeting(FxMeetingModel meeting)
        {
            if (meeting.Attendees.All(x => x.UserId != meeting.OwnerId))
                meeting.Attendees.Add(new FxMeetingAttendeeModel()
                    {UserId = meeting.OwnerId, Role = EnOvSessionRole.MODERATOR});

            return dbContext.Meetings.AddAsync(meeting.ToDbModel());
        }

        public async Task UpdateMeeting(FxMeetingModel meeting)
        {
            await this.dbContext.Meetings.UpdateAsync(m => m.Id == meeting.Id,
                new UpdateDefinitionBuilder<FxMeeting>()
                    .Set(x => x.Title, meeting.Title)
                    .Set(x => x.Description, meeting.Description)
                    .Set(x => x.StartTime, meeting.StartTime)
                    .Set(x => x.Duration, meeting.Duration)
            );
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

        public async Task<bool> CheckMeetingAccessLevel(string userId, string meetingId, EnMeetingAccessLevel level)
        {
            var meeting = await this.GetMeetingDb(meetingId);
            if (level == EnMeetingAccessLevel.View)
                return IsAllowedToSeeMeeting(meeting, userId);
            
            if (level == EnMeetingAccessLevel.Manage || level == EnMeetingAccessLevel.Manage)
                return meeting.OwnerId == userId;

            return false;
        }

        public async Task Archive(string meetingId)
        {
            await this.dbContext.Meetings.UpdateAsync(m => m.Id == meetingId, new UpdateDefinitionBuilder<FxMeeting>()
                .Set(m => m.IsArchived, true));
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

        private static bool IsAllowedToSeeMeeting(FxMeeting meeting, string userId)
        {
            return meeting.OwnerId == userId
                   || meeting.Attendees.Any(a => a.UserId == userId)
                   || meeting.IsPublic;
        }

        private Task<FxMeeting> GetMeetingDb(string meetingId)
        {
            return this.dbContext.Meetings.GetSingleAsync(m => m.Id == meetingId);
        }
    }
}