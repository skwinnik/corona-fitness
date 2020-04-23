using System.Threading.Tasks;
using CoronaFitnessBL.User.Models;

namespace CoronaFitnessBL.User.UserContext
{
    public interface IxUserContext
    {
        Task<CxUserModel> GetCurrentUser();
    }
}