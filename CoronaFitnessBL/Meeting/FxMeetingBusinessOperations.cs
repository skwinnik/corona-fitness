using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb;
using CoronaFitnessDb.Entities;

namespace CoronaFitnessBL.Meeting
{
    public class FxMeetingBusinessOperations : IxMeetingBusinessOperations
    {
        private readonly IxMongoDataContext dbContext;

        public FxMeetingBusinessOperations(IxMongoDataContext dbContext)
        {
            this.dbContext = dbContext;
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
            return dbContext.Meetings.AddAsync(new FxMeeting()
            {
                Id = meeting.Id,
                Title = meeting.Title,
                Description = meeting.Description,
                OwnerId = meeting.OwnerId,
                Attendees = meeting.Attendees
            });
        }
    }
}