using System.Threading.Tasks;
using CoronaFitnessBL.Account.Models;
using CoronaFitnessBL.User;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb.Identity;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver.Core.Operations;
using IdentityRole = CoronaFitnessDb.Identity.IdentityRole;

namespace CoronaFitnessBL.Account
{
    public class CxAccountBusinessOperations : IxAccountBusinessOperations
    {
        private readonly UserManager<CxIdentityUser> userManager;
        private readonly RoleManager<CxIdentityRole> roleManager;
        private readonly SignInManager<CxIdentityUser> signInManager;
        private readonly IxUserBusinessOperations userBop;

        public CxAccountBusinessOperations(UserManager<CxIdentityUser> userManager,
            RoleManager<CxIdentityRole> roleManager,
            SignInManager<CxIdentityUser> signInManager,
            IxUserBusinessOperations userBop)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.userBop = userBop;
        }

        /// <summary>
        /// Creates a user in identity and main DB, adds to a role. Name goes to main DB only
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<SignUpResult> SignUp(string email, string password, string name,
            IdentityRole role = IdentityRole.User)
        {
            var user = new CxIdentityUser() {Email = email, UserName = email};
            var userExists = await this.userManager.FindByNameAsync(user.UserName);
            if (userExists != null) return new SignUpResult() {Success = true};

            var result = await this.userManager.CreateAsync(user, password);
            if (!result.Succeeded) return new SignUpResult(result);

            result = await this.userManager.AddToRoleAsync(user, role.ToString());

            user = await this.userManager.FindByEmailAsync(user.Email);
            await this.userBop.Create(new CxUserModel()
                {Name = name, Email = user.Email, IdentityId = user.Id, CanCreateMeetings = false});

            return new SignUpResult(result);
        }

        public async Task<LoginResult> Login(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, true, false);
            return new LoginResult()
            {
                Success = result.Succeeded
            };
        }

        public async Task<LoginResult> Login(CxIdentityUser user)
        {
            await signInManager.SignInAsync(user, true);
            return new LoginResult()
            {
                Success = true
            };
        }

        public async Task<LogoutResult> Logout()
        {
            await signInManager.SignOutAsync();
            return new LogoutResult {Success = true};
        }

        public async Task<CreateRoleResult> CreateRole(CxIdentityRole role)
        {
            var roleExists = await this.roleManager.FindByNameAsync(role.Name);
            if (roleExists != null)
                return new CreateRoleResult() {Success = false};

            var result = await this.roleManager.CreateAsync(role);

            return new CreateRoleResult() {Success = result.Succeeded, Errors = result.Errors};
        }
    }
}