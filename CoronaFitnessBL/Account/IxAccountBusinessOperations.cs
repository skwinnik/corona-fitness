using System.Threading.Tasks;
using CoronaFitnessBL.Account.Models;
using CoronaFitnessDb.Identity;

namespace CoronaFitnessBL.Account
{
    public interface IxAccountBusinessOperations
    {
        Task<SignUpResult> SignUp(FxIdentityUser user, string password);
        Task<LoginResult> Login(string email, string password);
        Task<LoginResult> Login(FxIdentityUser user);
        Task<CreateRoleResult> CreateRole(FxIdentityRole role);
    }
}