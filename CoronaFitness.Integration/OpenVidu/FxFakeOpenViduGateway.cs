using System.Threading.Tasks;
using CoronaFitness.Integration.OpenVidu.Models;

namespace CoronaFitness.Integration.OpenVidu
{
    public class FxFakeOpenViduGateway : IxOpenViduGateway
    {
        public Task<CreateSessionResponse> CreateSession(CreateSessionRequest request)
        {
            return Task.FromResult<CreateSessionResponse>(new CreateSessionResponse());
        }

        public Task<CreateTokenResponse> CreateToken(CreateTokenRequest request)
        {
            return Task.FromResult<CreateTokenResponse>(new CreateTokenResponse());
        }
    }
}