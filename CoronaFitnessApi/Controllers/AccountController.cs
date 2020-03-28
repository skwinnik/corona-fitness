using System.Threading.Tasks;
using CoronaFitnessApi.Model.Account;
using CoronaFitnessDb.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoronaFitnessApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<FxIdentityUser> userManager;
        private readonly SignInManager<FxIdentityUser> signInManager;

        public AccountController(UserManager<FxIdentityUser> userManager, SignInManager<FxIdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var user = new FxIdentityUser {Email = request.Email, UserName = request.Email};
            var result = await this.userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(result.Errors);

            await this.signInManager.SignInAsync(user, true);
            return Ok(new SignUpResponse() {Success = true});
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, true, false);
            if (!result.Succeeded) return Ok(new LoginResponse() {Success = false});
            return Ok(new LoginResponse() {Success = true});
        }
    }
}