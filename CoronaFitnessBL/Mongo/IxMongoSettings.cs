namespace CoronaFitnessBL.Mongo
{
    public interface IxMongoSettings
    {
        string ConnectionString { get; set; }
        string DbName { get; set; }
    }
}