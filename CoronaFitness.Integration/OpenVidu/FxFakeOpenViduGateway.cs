using System.Threading.Tasks;
using CoronaFitness.Integration.OpenVidu.Models;

namespace CoronaFitness.Integration.OpenVidu
{
    public class CxFakeOpenViduGateway : IxOpenViduGateway
    {
        public Task<CreateSessionResponse> CreateSession(CreateSessionRequest request)
        {
            return Task.FromResult<CreateSessionResponse>(new CreateSessionResponse()
                {Id = "fake_session", CreatedAt = "fake"});
        }

        public Task<CreateTokenResponse> CreateToken(CreateTokenRequest request)
        {
            return Task.FromResult<CreateTokenResponse>(new CreateTokenResponse() {Token = "fake_token"});
        }
    }
}