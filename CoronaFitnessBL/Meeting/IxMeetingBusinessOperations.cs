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

        Task CreateMeeting(FxMeetingModel meeting);
    }
}