using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb;
using Microsoft.AspNetCore.Http;

namespace CoronaFitnessBL.User.UserContext
{
    public class FxUserContext : IxUserContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IxUserBusinessOperations usersBop;
        
        public FxUserContext(IHttpContextAccessor httpContextAccessor, IxUserBusinessOperations usersBop)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.usersBop = usersBop;
        }
        
        public async Task<FxUserModel> GetCurrentUser()
        {
            var user = httpContextAccessor.HttpContext.User;

            var claim = user?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (claim == null) return null;

            return await usersBop.GetByIdentityId(claim.Value);
        }
    }
}