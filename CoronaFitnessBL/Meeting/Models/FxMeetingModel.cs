using System;
using System.Collections.Generic;
using System.Linq;
using CoronaFitness.Integration.OpenVidu.Models;
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
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public bool IsPublic { get; set; }
        public List<FxMeetingAttendeeModel> Attendees { get; set; }
        public List<FxMeetingAttendeeRequestModel> AttendeeRequests { get; set; }

        public FxMeetingModel()
        {
            this.Attendees = new List<FxMeetingAttendeeModel>();
            this.AttendeeRequests = new List<FxMeetingAttendeeRequestModel>();
        }

        public FxMeetingModel(FxMeeting dbMeeting)
        {
            this.Id = dbMeeting.Id;
            this.Title = dbMeeting.Title;
            this.Description = dbMeeting.Description;
            this.StartTime = dbMeeting.StartTime;
            this.Duration = dbMeeting.Duration;
            this.OwnerId = dbMeeting.OwnerId;
            this.SessionId = dbMeeting.SessionId;
            this.IsPublic = dbMeeting.IsPublic;
            this.Attendees = dbMeeting.Attendees
                .Select(a => new FxMeetingAttendeeModel(a)).ToList();
            this.AttendeeRequests =
                dbMeeting.AttendeeRequests.Select(a => new FxMeetingAttendeeRequestModel(a)).ToList();
        }

        public FxMeeting ToDbModel()
        {
            return new FxMeeting()
            {
                Id = this.Id,
                OwnerId = this.OwnerId,
                Title = this.Title,
                Description = this.Description,
                StartTime = this.StartTime,
                Duration = this.Duration,
                SessionId = string.Empty,
                IsPublic = this.IsPublic,
                Attendees = this.Attendees.Select(x => x.ToDbModel()).ToList(),
                AttendeeRequests = this.AttendeeRequests.Select(x => x.ToDbModel()).ToList()
            };
        }
    }

    public class FxMeetingAttendeeModel
    {
        public string UserId { get; set; }
        public EnOvSessionRole Role { get; set; }

        public FxMeetingAttendeeModel()
        {
        }

        public FxMeetingAttendeeModel(FxMeetingAttendee dbMeetingAttendee)
        {
            this.UserId = dbMeetingAttendee.UserId;
            this.Role = Enum.Parse<EnOvSessionRole>(dbMeetingAttendee.Role);
        }

        public FxMeetingAttendee ToDbModel()
        {
            return new FxMeetingAttendee()
            {
                UserId = this.UserId,
                Token = string.Empty,
                Role = this.Role.ToString()
            };
        }
    }

    public class FxMeetingAttendeeRequestModel
    {
        public string UserId { get; set; }

        public FxMeetingAttendeeRequestModel()
        {
        }

        public FxMeetingAttendeeRequestModel(FxMeetingAttendeeRequest dbAttendeeRequest)
        {
            this.UserId = dbAttendeeRequest.UserId;
        }

        public FxMeetingAttendeeRequest ToDbModel()
        {
            return new FxMeetingAttendeeRequest()
            {
                UserId = this.UserId
            };
        }
    }
}