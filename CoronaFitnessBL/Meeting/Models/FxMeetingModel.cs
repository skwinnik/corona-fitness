using System.Collections.Generic;
using System.Linq;
using CoronaFitnessDb.Entities;

namespace CoronaFitnessBL.Meeting.Models
{
    public class FxMeetingModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public string SessionId { get; set; }
        public List<FxMeetingAttendeeModel> Attendees { get; set; }

        public FxMeetingModel()
        {
            this.Attendees = new List<FxMeetingAttendeeModel>();
        }

        public FxMeetingModel(FxMeeting dbMeeting)
        {
            this.Id = dbMeeting.Id;
            this.Title = dbMeeting.Title;
            this.Description = dbMeeting.Description;
            this.OwnerId = dbMeeting.OwnerId;
            this.Attendees = dbMeeting.Attendees
                .Select(a => new FxMeetingAttendeeModel(a)).ToList();
        }
    }

    public class FxMeetingAttendeeModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }

        public FxMeetingAttendeeModel()
        {
        }

        public FxMeetingAttendeeModel(FxMeetingAttendee dbMeetingAttendee)
        {
            this.UserId = dbMeetingAttendee.UserId;
            this.Token = dbMeetingAttendee.Token;
        }
    }
}