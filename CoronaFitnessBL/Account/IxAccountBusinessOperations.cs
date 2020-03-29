using System.Threading.Tasks;
using CoronaFitnessBL.Account.Models;
using CoronaFitnessDb.Identity;

namespace CoronaFitnessBL.Account
{
    public interface IxAccountBusinessOperations
    {
        Task<SignUpResult> SignUp(string email, string password, string name, IdentityRole role = IdentityRole.User);
        Task<LoginResult> Login(string email, string password);
        Task<LogoutResult> Logout();
        Task<LoginResult> Login(FxIdentityUser user);
        Task<CreateRoleResult> CreateRole(FxIdentityRole role);
    }
}