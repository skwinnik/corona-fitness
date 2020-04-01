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

        public FxUser()
        {
            this.Id = string.Empty;
            this.Name = string.Empty;
            this.Email = string.Empty;
            this.IdentityId = string.Empty;
        }
    }
}