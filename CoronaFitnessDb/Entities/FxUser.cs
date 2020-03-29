using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoronaFitnessDb.Entities
{
    public class FxUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string IdentityId { get; set; }
        public bool CanCreateMeetings { get; set; }
    }
}