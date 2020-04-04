using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoronaFitnessDb.Entities
{
    public class FxMeeting
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string OwnerId { get; set; }

        public string SessionId { get; set; }

        public List<FxMeetingAttendee> Attendees { get; set; }

        public FxMeeting()
        {
            this.Id = string.Empty;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.OwnerId = string.Empty;
            this.SessionId = string.Empty;
            this.Attendees = new List<FxMeetingAttendee>();
        }
    }

    [BsonNoId]
    public class FxMeetingAttendee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        public FxMeetingAttendee()
        {
            this.UserId = string.Empty;
            this.Token = string.Empty;
        }
    }
}