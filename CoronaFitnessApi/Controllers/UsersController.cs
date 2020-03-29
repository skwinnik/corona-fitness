using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoronaFitnessBL.User;
using CoronaFitnessBL.User.Models;
using CoronaFitnessBL.User.UserContext;
using CoronaFitnessDb.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoronaFitnessApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IxUserBusinessOperations usersBop;
        private readonly IxUserContext userContext;
        
        public UsersController(IxUserBusinessOperations usersBop, IxUserContext userContext)
        {
            this.usersBop = usersBop;
            this.userContext = userContext;
        }

        [HttpGet]
        [Route("getCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(await userContext.GetCurrentUser());
        }
    }
}