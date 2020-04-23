using System.Threading.Tasks;
using CoronaFitness.Integration.OpenVidu.Models;
using CoronaFitness.Integration.OpenVidu.Rest;
using RestSharp;
using RestSharp.Serialization.Json;

namespace CoronaFitness.Integration.OpenVidu
{
    public class CxOpenViduGateway : IxOpenViduGateway
    {
        private readonly OpenViduRestClientBuilder restClientBuilder;

        public CxOpenViduGateway(OpenViduRestClientBuilder restClientBuilder)
        {
            this.restClientBuilder = restClientBuilder;
        }

        public Task<CreateSessionResponse> CreateSession(CreateSessionRequest request)
        {
            var client = restClientBuilder.Build();
            var restRequest = new RestRequest("/api/sessions", DataFormat.Json);
            return client.PostAsync<CreateSessionResponse>(restRequest);
        }

        public Task<CreateTokenResponse> CreateToken(CreateTokenRequest request)
        {
            var client = restClientBuilder.Build();
            var restRequest = new RestRequest("/api/tokens");
            restRequest.AddParameter("application/json", System.Text.Json.JsonSerializer.Serialize(request),
                ParameterType.RequestBody);

            return client.PostAsync<CreateTokenResponse>(restRequest);
        }
    }
}