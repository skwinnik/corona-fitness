using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoronaFitnessApi.Filters;
using CoronaFitnessApi.Model.Meeting;
using CoronaFitnessBL.Meeting;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User;
using CoronaFitnessBL.User.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace CoronaFitnessApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MeetingsController : ControllerBase
    {
        private readonly IxUserContext userContext;
        private readonly IxMeetingBusinessOperations meetingBop;
        private readonly IxUserBusinessOperations userBop;

        public MeetingsController(IxUserContext userContext,
            IxMeetingBusinessOperations meetingBop,
            IxUserBusinessOperations userBop)
        {
            this.userContext = userContext;
            this.meetingBop = meetingBop;
            this.userBop = userBop;
        }

        #region meetings
        /// <summary>
        /// Get all meetings for current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var currentUser = await this.userContext.GetCurrentUser();
            var meetings = await this.meetingBop.GetMeetings(currentUser);
            return Ok(meetings.Select(x => new MeetingDto(x, currentUser)));
        }

        /// <summary>
        /// Get a specific meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{meetingId}")]
        [MeetingPermission(EnMeetingAccessLevel.View)]
        public async Task<IActionResult> Get([FromRoute] string meetingId)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            var meeting = await this.meetingBop.GetMeeting(meetingId);
            return Ok(new MeetingDto(meeting, currentUser));
        }

        /// <summary>
        /// Create a new meeting
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] MeetingDto meeting)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            if (!currentUser.CanCreateMeetings)
                return BadRequest();

            try
            {
                await this.meetingBop.CreateMeeting(new CxMeetingModel()
                {
                    Id = "",
                    Title = meeting.Title,
                    Description = meeting.Description,
                    OwnerId = currentUser.Id,
                    StartTime = meeting.StartTime,
                    Duration = meeting.Duration,
                    IsPublic = meeting.IsPublic
                });
            }

            catch (Exception)
            {
                return BadRequest();
            }


            return Ok();
        }
        
        /// <summary>
        /// Edit an existing meeting
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{meetingId}")]
        [MeetingPermission(EnMeetingAccessLevel.Edit)]
        public async Task<IActionResult> Edit([FromBody] MeetingDto meeting)
        {
            try
            {
                await this.meetingBop.UpdateMeeting(new CxMeetingModel()
                {
                    Id = this.RouteData.Values["meetingId"].ToString(),
                    Title = meeting.Title,
                    Description = meeting.Description,
                    StartTime = meeting.StartTime,
                    Duration = meeting.Duration
                });
            }

            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        /// <summary>
        /// Get a token for current user
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{meetingId}/token")]
        [MeetingPermission(EnMeetingAccessLevel.View)]
        public async Task<IActionResult> GetToken([FromRoute] string meetingId)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            var meeting = await this.meetingBop.GetMeeting(meetingId);
            if (string.IsNullOrEmpty(meeting.SessionId) && meeting.OwnerId != currentUser.Id)
                return BadRequest("Meeting is not started yet");

            return Ok(await this.meetingBop.GetToken(meetingId, currentUser.Id));
        }

        /// <summary>
        /// Archive a meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{meetingId}/archive")]
        [MeetingPermission(EnMeetingAccessLevel.Manage)]
        public async Task<IActionResult> Archive([FromRoute] string meetingId)
        {
            await this.meetingBop.ArchiveMeeting(meetingId);
            return Ok();
        }

        #endregion meetings

        #region attendees
        /// <summary>
        /// Get attendees for a meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{meetingId}/attendees")]
        [MeetingPermission(EnMeetingAccessLevel.Manage)]
        public async Task<IActionResult> GetAttendees([FromRoute] string meetingId)
        {
            var attendees = await meetingBop.GetAttendees(meetingId);
            var users = await this.userBop.GetById(attendees.Select(r => r.UserId).ToList());

            return Ok(users.Select(x => new AttendeeDto() {Name = x.Name, UserId = x.Id}));
        }

        /// <summary>
        /// Delete an attendee from a meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{meetingId}/attendees/{attendeeId}")]
        [MeetingPermission(EnMeetingAccessLevel.Manage)]
        public async Task<IActionResult> DeleteAttendee([FromRoute] string meetingId, [FromRoute] string attendeeId)
        {
            try
            {
                await this.meetingBop.RemoveAttendee(meetingId, attendeeId);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
        #endregion attendees
        
        #region attendee_requests
        /// <summary>
        /// Add attendee request to a meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{meetingId}/attendee_requests")]
        [MeetingPermission(EnMeetingAccessLevel.View)]
        public async Task<IActionResult> PutAttendeeRequest([FromRoute] string meetingId)
        {
            var currentUser = await this.userContext.GetCurrentUser();
            await this.meetingBop.AddAttendeeRequest(meetingId, currentUser.Id);
            return Ok();
        }

        /// <summary>
        /// Get attendee requests for a meeting
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{meetingId}/attendee_requests")]
        [MeetingPermission(EnMeetingAccessLevel.Manage)]
        public async Task<IActionResult> GetAttendeeRequests([FromRoute] string meetingId)
        {
            var requests = await this.meetingBop.GetAttendeeRequests(meetingId);
            var users = await this.userBop.GetById(requests.Select(r => r.UserId).ToList());
            return Ok(users.Select(x => new AttendeeRequestDto() {Name = x.Name, UserId = x.Id}));
        }

        /// <summary>
        /// Reject an attendee request
        /// </summary>
        /// <param name="meetingId"></param>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{meetingId}/attendee_requests/{attendeeId}/reject")]
        [MeetingPermission(EnMeetingAccessLevel.Manage)]
        public async Task<IActionResult> RejectAttendeeRequest([FromRoute] string meetingId, [FromRoute] string attendeeId)
        {
            await this.meetingBop.RejectAttendeeRequest(meetingId, attendeeId);
            return Ok();
        }
        
        /// <summary>
        /// Approve an attendee request
        /// </summary>
        /// <param name="meetingId"></param>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{meetingId}/attendee_requests/{attendeeId}/approve")]
        [MeetingPermission(EnMeetingAccessLevel.Manage)]
        public async Task<IActionResult> ApproveAttendeeRequest([FromRoute] string meetingId, [FromRoute] string attendeeId)
        {
            await this.meetingBop.ApproveAttendeeRequest(meetingId, attendeeId);
            return Ok();
        }
        
        #endregion attendee_requests
    }
}