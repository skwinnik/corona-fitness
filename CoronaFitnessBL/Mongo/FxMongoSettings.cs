namespace CoronaFitnessBL.Mongo
{
    public class FxMongoSettings : IxMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
    }
}