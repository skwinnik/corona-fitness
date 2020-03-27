using CoronaFitnessBL.Mongo.Entities;

namespace CoronaFitnessBL.Mongo
{
    public interface IxMongoContext
    {
        FxMongoDbSet<FxUser> Users { get; }
    }
}