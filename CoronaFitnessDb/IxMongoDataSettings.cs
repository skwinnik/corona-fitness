namespace CoronaFitnessDb
{
    public interface IxMongoDataSettings
    {
        string ConnectionString { get; set; }
        string DbName { get; set; }
    }
}