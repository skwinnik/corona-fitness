using CoronaFitnessDb.Conventions;
using CoronaFitnessDb.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CoronaFitnessDb
{
    public class FxMongoDataContext : IxMongoDataContext
    {
        private string ConnectionString { get; set; }
        private string DbName { get; set; }

        public FxMongoDbSet<FxUser> Users { get; }
        public FxMongoDbSet<FxMeeting> Meetings { get; }

        public FxMongoDataContext(IxMongoDataSettings settings)
        {
            this.ConnectionString = settings.ConnectionString;
            this.DbName = settings.DbName;

            var connectionUrl = new MongoUrl(this.ConnectionString);
            var clientSettings = MongoClientSettings.FromUrl(connectionUrl);

            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(this.DbName);

            ConventionRegistry.Register(
                "CamelCaseConvention",
                new ConventionPack {new CamelCaseElementNameConvention()},
                type => type.Namespace == "CoronaFitnessDb.Entities");

            ConventionRegistry.Register(
                "GenerateId",
                new ConventionPack {new ObjectIdGeneratorConvention()},
                type => type.Namespace == "CoronaFitnessDb.Entities");
            
            this.Users = new FxMongoDbSet<FxUser>(database.GetCollection<FxUser>("Users"));
            this.Meetings = new FxMongoDbSet<FxMeeting>(database.GetCollection<FxMeeting>("Meetings"));
        }
    }
}