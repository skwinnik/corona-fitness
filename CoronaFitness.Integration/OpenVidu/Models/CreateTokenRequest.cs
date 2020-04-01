namespace CoronaFitness.Integration.OpenVidu.Models
{
    public class CreateTokenRequest
    {
        public string Session { get; set; }
        public EnOvSessionRole Role { get; set; }
        public string Data { get; set; }
    }
}