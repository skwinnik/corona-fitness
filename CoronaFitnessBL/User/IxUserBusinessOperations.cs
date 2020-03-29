using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitnessBL.User.Models;

namespace CoronaFitnessBL.User
{
    public interface IxUserBusinessOperations
    {
        Task<List<FxUserModel>> GetAll();
        Task Create(FxUserModel user);
        Task<FxUserModel> GetByIdentityId(string identityId);
        Task<FxUserModel> GetByEmail(string email);
    }
}