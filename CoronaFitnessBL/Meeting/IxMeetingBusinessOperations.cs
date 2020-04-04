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
        /// Get a meeting by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FxMeetingModel> GetMeeting(string id, string userId);

        Task CreateMeeting(FxMeetingModel meeting);
        Task<string> GetToken(string meetingId, string userId);
    }
}