using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.Models;

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
        /// Get a meeting by id for specified user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<FxMeetingModel> GetMeeting(string id, string userId);

        /// <summary>
        /// Add user to meeting as AttendeeRequest
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RequestToAttend(string id, string userId);

        /// <summary>
        /// Get requests to attend to a specific meeting; ownership check by userId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<FxMeetingAttendeeRequestModel>> GetRequestsToAttend(string id, string userId);

        /// <summary>
        /// Approve a user request to attend a specific meeting; ownership check by ownerId 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task ApproveRequestToAttend(string id, string userId, string ownerId);

        /// <summary>
        /// Reject a user request to attend a specific meeting; ownership check by ownerId 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task RejectRequestToAttend(string id, string userId, string ownerId);

        Task CreateMeeting(FxMeetingModel meeting);
        Task<string> GetToken(string meetingId, string userId);
    }
}