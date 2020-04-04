using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CoronaFitness.Integration.OpenVidu.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnOvSessionRole
    {
        SUBSCRIBER,
        PUBLISHER,
        MODERATOR
    }
}