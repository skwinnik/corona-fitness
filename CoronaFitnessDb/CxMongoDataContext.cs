using System;
using CoronaFitnessDb.Conventions;
using CoronaFitnessDb.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CoronaFitnessDb
{
    public class CxMongoDataContext : IxMongoDataContext
    {
        private string ConnectionString { get; set; }
        private string DbName { get; set; }

        public CxMongoDbSet<CxUser> Users { get; }
        public CxMongoDbSet<CxMeeting> Meetings { get; }

        public CxMongoDataContext(IxMongoDataSettings settings)
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
                type => type.Namespace == "CoronaFitnessDb.Entities" 
                        && Attribute.GetCustomAttribute(type, typeof(BsonNoIdAttribute)) == null);
            
            this.Users = new CxMongoDbSet<CxUser>(database.GetCollection<CxUser>("Users"));
            this.Meetings = new CxMongoDbSet<CxMeeting>(database.GetCollection<CxMeeting>("Meetings"));
        }
    }
}