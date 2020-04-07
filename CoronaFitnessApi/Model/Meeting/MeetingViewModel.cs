using System;
using System.Linq;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb.Entities;

namespace CoronaFitnessApi.Model.Meeting
{
    public class MeetingViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public bool IsPublic { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAttendee { get; set; }
        public bool IsAttendeeRequested { get; set; }

        public MeetingViewModel()
        {
        }

        public MeetingViewModel(FxMeetingModel model, FxUserModel currentUser)
        {
            this.Id = model.Id;
            this.Title = model.Title;
            this.Description = model.Description;
            this.StartTime = model.StartTime;
            this.Duration = model.Duration;
            this.IsPublic = model.IsPublic;

            this.IsOwner = model.OwnerId == currentUser.Id;
            this.IsAttendee = model.Attendees.Any(a => a.UserId == currentUser.Id);
            this.IsAttendeeRequested = model.AttendeeRequests.Any(a => a.UserId == currentUser.Id);
        }
    }
}