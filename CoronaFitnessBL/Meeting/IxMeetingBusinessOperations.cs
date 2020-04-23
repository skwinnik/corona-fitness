using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb.Entities;

namespace CoronaFitnessBL.Meeting
{
    public interface IxMeetingBusinessOperations
    {
        /// <summary>
        /// Get all meetings for specified user
        /// </summary>
        /// <returns></returns>
        Task<List<FxMeetingModel>> GetMeetings(FxUserModel user);

        /// <summary>
        /// Get a meeting by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<FxMeetingModel> GetMeeting(string id);
        
        /// <summary>
        /// Get a meeting attendees
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<FxMeetingAttendeeModel>> GetAttendees(string id);

        /// <summary>
        /// Add user to meeting as AttendeeRequest
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task AddAttendeeRequest(string id, string userId);

        /// <summary>
        /// Get requests to attend to a specific meeting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<FxMeetingAttendeeRequestModel>> GetAttendeeRequests(string id);

        /// <summary>
        /// Approve a request to attend a specific meeting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task ApproveAttendeeRequest(string id, string userId);

        /// <summary>
        /// Reject a request to attend a specific meeting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task RejectAttendeeRequest(string id, string userId);

        /// <summary>
        /// Set IsArchived to true
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        Task ArchiveMeeting(string meetingId);

        /// <summary>
        /// Removes attendee from a meeting; can't remove owner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">attendeeId must not equal ownerId</exception>
        Task RemoveAttendee(string id, string attendeeId);

        /// <summary>
        /// Creates a new meeting
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        Task CreateMeeting(FxMeetingModel meeting);
        /// <summary>
        /// Updates an existing meeting
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        Task UpdateMeeting(FxMeetingModel meeting);
        /// <summary>
        /// Gets OpenVidu Token for a specified user
        /// </summary>
        /// <param name="meetingId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetToken(string meetingId, string userId);
        
        /// <summary>
        /// Checks user permissions
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="meetingId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<bool> CheckMeetingAccessLevel(string userId, string meetingId, EnMeetingAccessLevel level);
    }
}