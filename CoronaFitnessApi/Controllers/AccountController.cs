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
            var result = await accountBop.SignUp(request.Email, request.Password, request.Name);
            if (!result.Success)
                return BadRequest(result.Errors);

            var loginResult = await accountBop.Login(request.Email, request.Password);
            if (!loginResult.Success)
                return Unauthorized();
            
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await accountBop.Login(request.Email, request.Password);
            if (!result.Success) return Unauthorized(result.Errors);

            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await accountBop.Logout();
            return Ok();
        }
    }
}