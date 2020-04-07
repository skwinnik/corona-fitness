using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitnessApi.Model.Meeting;
using CoronaFitnessBL.Meeting;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User;
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
        private readonly IxUserBusinessOperations userBop;

        public MeetingController(IxUserContext userContext,
            IxMeetingBusinessOperations meetingBop,
            IxUserBusinessOperations userBop)
        {
            this.userContext = userContext;
            this.meetingBop = meetingBop;
            this.userBop = userBop;
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
            var meetings = await meetingBop.GetMeetings(currentUser);
            return Ok(meetings.Select(x => new MeetingViewModel(x, currentUser)));
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
            return Ok(new MeetingViewModel(meeting, currentUser));
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
            var currentUser = await userContext.GetCurrentUser();

            if (!string.IsNullOrEmpty(request.Id))
            {
                var m = await this.meetingBop.GetMeeting(request.Id, currentUser.Id);
                if (m.OwnerId == currentUser.Id)
                {
                    await this.meetingBop.UpdateMeeting(new FxMeetingModel()
                    {
                        Id = request.Id,
                        Title = request.Title,
                        Description = request.Description,
                        StartTime = request.StartTime,
                        Duration = request.Duration
                    });

                    return Ok();
                }

                return NotFound();
            }

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
            var meeting = await this.meetingBop.GetMeeting(meetingId, currentUser.Id);
            if (string.IsNullOrEmpty(meeting.SessionId) && meeting.OwnerId != currentUser.Id)
                return NotFound("Meeting is not started yet");
            
            return Ok(new GetTokenResponse()
            {
                Token = await this.meetingBop.GetToken(meetingId, currentUser.Id)
            });
        }

        [HttpPost]
        [Route("requestToAttend")]
        public async Task<IActionResult> RequestToAttend(MeetingUserIdRequest request)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            await this.meetingBop.RequestToAttend(request.MeetingId, currentUser.Id);
            return Ok(true);
        }

        [HttpGet]
        [Route("getRequestsToAttend")]
        public async Task<IActionResult> GetRequestsToAttend([FromQuery] MeetingUserIdRequest request)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            var requests = await this.meetingBop.GetRequestsToAttend(request.MeetingId, currentUser.Id);
            var users = await this.userBop.GetById(requests.Select(r => r.UserId).ToList());
            return Ok(users.Select(x => new RequestToAttendDto() {Name = x.Name, UserId = x.Id}));
        }

        [HttpPost]
        [Route("rejectRequestToAttend")]
        public async Task<IActionResult> RejectRequestToAttend(MeetingUserIdRequest request)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            await this.meetingBop.RejectRequestToAttend(request.MeetingId, request.UserId, currentUser.Id);
            return Ok(true);
        }

        [HttpPost]
        [Route("approveRequestToAttend")]
        public async Task<IActionResult> ApproveRequestToAttend(MeetingUserIdRequest request)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            await this.meetingBop.ApproveRequestToAttend(request.MeetingId, request.UserId, currentUser.Id);
            return Ok(true);
        }
    }
}