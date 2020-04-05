using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitnessApi.Model.Meeting;
using CoronaFitnessBL.Meeting;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

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
        [Route("getMeetings")]
        public async Task<IActionResult> GetMeetings()
        {
            var currentUser = await userContext.GetCurrentUser();
            return Ok(await meetingBop.GetMeetings(currentUser));
        }
        
        /// <summary>
        /// Returns meeting by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getMeetingById")]
        public async Task<IActionResult> GetMeetingById(string id)
        {
            var currentUser = await userContext.GetCurrentUser();
            var meeting = await meetingBop.GetMeeting(id, currentUser.Id);
            return Ok(meeting);
        }

        /// <summary>
        /// If ID is null, creates a new meeting; otherwise updates an existing one
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveMeeting")]
        public async Task<IActionResult> SaveMeeting(SaveMeetingRequest request)
        {
            if (!string.IsNullOrEmpty(request.Id))
                throw new NotImplementedException("Editing is not yet implemented");

            var currentUser = await userContext.GetCurrentUser();
            if (!currentUser.CanCreateMeetings) return NotFound();
            
            await this.meetingBop.CreateMeeting(new FxMeetingModel()
            {
                Id = "",
                Title = request.Title,
                Description = request.Description,
                OwnerId = currentUser.Id,
                StartTime = request.StartTime,
                Duration = request.Duration,
                IsPublic = request.IsPublic
            });
            
            return Ok(true);
        }

        [HttpGet]
        [Route("getToken")]
        public async Task<IActionResult> GetToken(string meetingId)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            return Ok(new GetTokenResponse()
            {
                Token = await this.meetingBop.GetToken(meetingId, currentUser.Id)
            });
        }
    }
}