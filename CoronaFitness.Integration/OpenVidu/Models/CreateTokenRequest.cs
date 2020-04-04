using System;
using System.Text.Json.Serialization;

namespace CoronaFitness.Integration.OpenVidu.Models
{
    public class CreateTokenRequest
    {
        [JsonPropertyName("session")]
        public string Session { get; set; }
        [JsonPropertyName("role")]
        public EnOvSessionRole Role { get; set; }
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}