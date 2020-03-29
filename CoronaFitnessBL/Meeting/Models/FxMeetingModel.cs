using System.Collections.Generic;
using CoronaFitnessDb.Entities;

namespace CoronaFitnessBL.Meeting.Models
{
    public class FxMeetingModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public string[] Attendees { get; set; }

        public FxMeetingModel()
        {
        }

        public FxMeetingModel(FxMeeting dbMeeting)
        {
            this.Id = dbMeeting.Id;
            this.Title = dbMeeting.Title;
            this.Description = dbMeeting.Description;
            this.OwnerId = dbMeeting.OwnerId;
            this.Attendees = dbMeeting.Attendees;
        }
    }
}