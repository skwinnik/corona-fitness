using System.Threading.Tasks;
using CoronaFitnessApi.Model.Account;
using CoronaFitnessBL.Account;
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
        private readonly IxAccountBusinessOperations accountBop;
        public AccountController(IxAccountBusinessOperations accountBop)
        {
            this.accountBop = accountBop;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var user = new FxIdentityUser {Email = request.Email, UserName = request.Email};
            var result = await accountBop.SignUp(user, request.Password);
            if (!result.Success)
                return BadRequest(new SignUpResponse() {Success = result.Success, Errors = result.Errors});

            await accountBop.Login(user);
            return Ok(new SignUpResponse() {Success = true});
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await accountBop.Login(request.Email, request.Password);
            if (!result.Success) return Ok(new LoginResponse() {Success = false});
            
            return Ok(new LoginResponse() {Success = true});
        }
    }
}