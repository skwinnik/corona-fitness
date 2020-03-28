using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitnessBL.User;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoronaFitnessApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private IxUserBusinessOperations usersBop;
        
        public UsersController(IxUserBusinessOperations usersBop)
        {
            this.usersBop = usersBop;
        }
        
        [Route("get")]
        public Task<List<FxUserModel>> Get() => this.usersBop.GetAll();
    }
}