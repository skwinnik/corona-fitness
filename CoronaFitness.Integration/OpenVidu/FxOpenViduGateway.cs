using System.Threading.Tasks;
using CoronaFitness.Integration.OpenVidu.Models;
using CoronaFitness.Integration.OpenVidu.Rest;
using RestSharp;

namespace CoronaFitness.Integration.OpenVidu
{
    public class FxOpenViduGateway : IxOpenViduGateway
    {
        private readonly OpenViduRestClientBuilder restClientBuilder;

        public FxOpenViduGateway(OpenViduRestClientBuilder restClientBuilder)
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
            var restRequest = new RestRequest("/api/tokens", DataFormat.Json)
                .AddParameter("session", request.Session)
                .AddParameter("data", request.Data)
                .AddParameter("role", request.Role.ToString());
            
            return client.PostAsync<CreateTokenResponse>(restRequest);
        }
    }
}