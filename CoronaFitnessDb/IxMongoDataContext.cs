using CoronaFitnessDb.Entities;

namespace CoronaFitnessDb
{
    public interface IxMongoDataContext
    {
        FxMongoDbSet<FxUser> Users { get; }
        FxMongoDbSet<FxMeeting> Meetings { get; }
    }
}