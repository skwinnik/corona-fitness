using CoronaFitnessDb.Entities;

namespace CoronaFitnessDb
{
    public interface IxMongoDataContext
    {
        CxMongoDbSet<CxUser> Users { get; }
        CxMongoDbSet<CxMeeting> Meetings { get; }
    }
}