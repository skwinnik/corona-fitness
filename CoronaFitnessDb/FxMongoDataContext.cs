using CoronaFitnessBL.Mongo;
using CoronaFitnessBL.Mongo.Entities;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CoronaFitnessDb
{
    public class FxMongoDataContext : IxMongoDataContext
    {
        private string ConnectionString { get; set; }
        private string DbName { get; set; }
        
        public FxMongoDbSet<FxUser> Users { get; }

        public FxMongoDataContext(IxMongoDataSettings settings)
        {
            this.ConnectionString = settings.ConnectionString;
            this.DbName = settings.DbName;
            
            var connectionUrl = new MongoUrl(this.ConnectionString);
            var clientSettings = MongoClientSettings.FromUrl(connectionUrl);

            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(this.DbName);
            
            ConventionRegistry.Register(
                "FxCamelCaseConvention",
                new ConventionPack { new CamelCaseElementNameConvention() }, 
                type => type.Namespace == "CoronaFitnessBL.Mongo.Entities");
            
            this.Users = new FxMongoDbSet<FxUser>(database.GetCollection<FxUser>("Users"));
        }
    }
}