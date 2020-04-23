using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoronaFitnessDb.Entities
{
    public class CxMeeting
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string OwnerId { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public bool IsPublic { get; set; }
        
        public bool IsArchived { get; set; }

        public string SessionId { get; set; }

        public List<CxMeetingAttendee> Attendees { get; set; }
        public List<CxMeetingAttendeeRequest> AttendeeRequests { get; set; }

        public CxMeeting()
        {
            this.Id = string.Empty;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.OwnerId = string.Empty;
            this.SessionId = string.Empty;
            this.IsPublic = false;
            this.IsArchived = false;
            this.Attendees = new List<CxMeetingAttendee>();
            this.AttendeeRequests = new List<CxMeetingAttendeeRequest>();
        }
    }

    [BsonNoId]
    public class CxMeetingAttendee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        public CxMeetingAttendee()
        {
            this.UserId = string.Empty;
            this.Token = string.Empty;
        }
    }

    [BsonNoId]
    public class CxMeetingAttendeeRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}