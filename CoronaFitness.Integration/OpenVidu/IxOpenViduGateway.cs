using System.Threading.Tasks;
using CoronaFitness.Integration.OpenVidu.Models;

namespace CoronaFitness.Integration.OpenVidu
{
    public interface IxOpenViduGateway
    {
        Task<CreateSessionResponse> CreateSession(CreateSessionRequest request);
        Task<CreateTokenResponse> CreateToken(CreateTokenRequest request);
    }
}