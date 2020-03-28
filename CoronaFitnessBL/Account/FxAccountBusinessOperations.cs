using System.Threading.Tasks;
using CoronaFitnessBL.Account.Models;
using CoronaFitnessDb.Identity;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver.Core.Operations;

namespace CoronaFitnessBL.Account
{
    public class FxAccountBusinessOperations : IxAccountBusinessOperations
    {
        private readonly UserManager<FxIdentityUser> userManager;
        private readonly RoleManager<FxIdentityRole> roleManager;
        private readonly SignInManager<FxIdentityUser> signInManager;

        public FxAccountBusinessOperations(UserManager<FxIdentityUser> userManager,
            RoleManager<FxIdentityRole> roleManager,
            SignInManager<FxIdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<SignUpResult> SignUp(FxIdentityUser user, string password)
        {
            var userExists = await this.userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
                return new SignUpResult() { Success = false };

            var result = await this.userManager.CreateAsync(user, password);
            return new SignUpResult()
            {
                Success = result.Succeeded,
                Errors = result.Errors
            };
        }

        public async Task<LoginResult> Login(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, true, false);
            return new LoginResult()
            {
                Success = result.Succeeded
            };
        }

        public async Task<LoginResult> Login(FxIdentityUser user)
        {
            await signInManager.SignInAsync(user, true);
            return new LoginResult()
            {
                Success = true
            };
        }

        public async Task<CreateRoleResult> CreateRole(FxIdentityRole role)
        {
            var roleExists = await this.roleManager.FindByNameAsync(role.Name);
            if (roleExists != null)
                return new CreateRoleResult() { Success = false };

            var result = await this.roleManager.CreateAsync(role);
            
            return new CreateRoleResult() { Success = result.Succeeded, Errors = result.Errors};
        }
    }
}