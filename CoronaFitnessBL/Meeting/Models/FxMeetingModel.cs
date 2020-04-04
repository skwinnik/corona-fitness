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

        public FxMeeting ToDbModel()
        {
            return new FxMeeting()
            {
                Id = this.Id,
                OwnerId = this.OwnerId,
                Title = this.Title,
                Description = this.Description,
                SessionId = this.SessionId ?? string.Empty,
                Attendees = this.Attendees.Select(x => x.ToDbModel()).ToList()
            };
        }
    }

    public class FxMeetingAttendeeModel
    {
        public string UserId { get; set; }
        public EnOvSessionRole Role { get; set; }
        public string Token { get; set; }

        public FxMeetingAttendeeModel()
        {
        }

        public FxMeetingAttendeeModel(FxMeetingAttendee dbMeetingAttendee)
        {
            this.UserId = dbMeetingAttendee.UserId;
            this.Token = dbMeetingAttendee.Token;
            this.Role = Enum.Parse<EnOvSessionRole>(dbMeetingAttendee.Role);
        }

        public FxMeetingAttendee ToDbModel()
        {
            return new FxMeetingAttendee()
            {
                UserId = this.UserId,
                Token = this.Token ?? string.Empty,
                Role = this.Role.ToString()
            };
        }
    }
}