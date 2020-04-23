namespace CoronaFitnessDb
{
    public class CxMongoDataSettings : IxMongoDataSettings
    {
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
    }
}