namespace CoronaFitnessDb
{
    public class FxMongoDataSettings : IxMongoDataSettings
    {
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
    }
}