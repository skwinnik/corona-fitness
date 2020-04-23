using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitnessBL.User.Models;

namespace CoronaFitnessBL.User
{
    public interface IxUserBusinessOperations
    {
        Task<List<CxUserModel>> GetAll();
        Task Create(CxUserModel user);
        Task SetCanCreateMeetings(string id, bool canCreate);
        Task<CxUserModel> GetByIdentityId(string identityId);
        Task<List<CxUserModel>> GetById(List<string> ids);
        Task<CxUserModel> GetByEmail(string email);
    }
}