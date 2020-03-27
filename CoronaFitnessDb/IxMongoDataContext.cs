using CoronaFitnessBL.Mongo.Entities;

namespace CoronaFitnessDb
{
    public interface IxMongoDataContext
    {
        FxMongoDbSet<FxUser> Users { get; }
    }
}