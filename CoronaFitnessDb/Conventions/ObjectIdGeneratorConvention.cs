using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;

namespace CoronaFitnessDb.Conventions
{
    public class ObjectIdGeneratorConvention : IClassMapConvention
    {
        public string Name => "StringObjectIdGeneratorConvention";

        public void Apply(BsonClassMap classMap)
        {
            classMap.MapIdProperty("Id")
                .SetIgnoreIfDefault(true)
                .SetIdGenerator(StringObjectIdGenerator.Instance);
        }
    }
}