using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitnessBL.Meeting;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoronaFitnessApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MeetingController : ControllerBase
    {
        private readonly IxUserContext userContext;
        private readonly IxMeetingBusinessOperations meetingBop;

        public MeetingController(IxUserContext userContext, IxMeetingBusinessOperations meetingBop)
        {
            this.userContext = userContext;
            this.meetingBop = meetingBop;
        }

        /// <summary>
        /// Returns all meetings for current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMeetings()
        {
            var currentUser = await userContext.GetCurrentUser();
            return Ok(await meetingBop.GetMeetings(currentUser));
        }
    }
}