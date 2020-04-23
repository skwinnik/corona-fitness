using System;
using System.Collections.Generic;
using System.Linq;
using CoronaFitness.Integration.OpenVidu.Models;
using CoronaFitnessDb.Entities;

namespace CoronaFitnessBL.Meeting.Models
{
    public class CxMeetingModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public string SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public bool IsPublic { get; set; }
        public bool IsArchived { get; set; }
        public List<CxMeetingAttendeeModel> Attendees { get; set; }
        public List<CxMeetingAttendeeRequestModel> AttendeeRequests { get; set; }

        public CxMeetingModel()
        {
            this.Attendees = new List<CxMeetingAttendeeModel>();
            this.AttendeeRequests = new List<CxMeetingAttendeeRequestModel>();
        }

        public CxMeetingModel(CxMeeting dbMeeting)
        {
            this.Id = dbMeeting.Id;
            this.Title = dbMeeting.Title;
            this.Description = dbMeeting.Description;
            this.StartTime = dbMeeting.StartTime;
            this.Duration = dbMeeting.Duration;
            this.OwnerId = dbMeeting.OwnerId;
            this.SessionId = dbMeeting.SessionId;
            this.IsPublic = dbMeeting.IsPublic;
            this.IsArchived = dbMeeting.IsArchived;
            this.Attendees = dbMeeting.Attendees
                .Select(a => new CxMeetingAttendeeModel(a)).ToList();
            this.AttendeeRequests =
                dbMeeting.AttendeeRequests.Select(a => new CxMeetingAttendeeRequestModel(a)).ToList();
        }

        public CxMeeting ToDbModel()
        {
            return new CxMeeting()
            {
                Id = this.Id,
                OwnerId = this.OwnerId,
                Title = this.Title,
                Description = this.Description,
                StartTime = this.StartTime,
                Duration = this.Duration,
                SessionId = string.Empty,
                IsPublic = this.IsPublic,
                IsArchived = this.IsArchived,
                Attendees = this.Attendees.Select(x => x.ToDbModel()).ToList(),
                AttendeeRequests = this.AttendeeRequests.Select(x => x.ToDbModel()).ToList()
            };
        }
    }

    public class CxMeetingAttendeeModel
    {
        public string UserId { get; set; }
        public EnOvSessionRole Role { get; set; }

        public CxMeetingAttendeeModel()
        {
        }

        public CxMeetingAttendeeModel(CxMeetingAttendee dbMeetingAttendee)
        {
            this.UserId = dbMeetingAttendee.UserId;
            this.Role = Enum.Parse<EnOvSessionRole>(dbMeetingAttendee.Role);
        }

        public CxMeetingAttendee ToDbModel()
        {
            return new CxMeetingAttendee()
            {
                UserId = this.UserId,
                Token = string.Empty,
                Role = this.Role.ToString()
            };
        }
    }

    public class CxMeetingAttendeeRequestModel
    {
        public string UserId { get; set; }

        public CxMeetingAttendeeRequestModel()
        {
        }

        public CxMeetingAttendeeRequestModel(CxMeetingAttendeeRequest dbAttendeeRequest)
        {
            this.UserId = dbAttendeeRequest.UserId;
        }

        public CxMeetingAttendeeRequest ToDbModel()
        {
            return new CxMeetingAttendeeRequest()
            {
                UserId = this.UserId
            };
        }
    }
}