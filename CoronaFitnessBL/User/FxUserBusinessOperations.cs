using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitnessBL.Mongo;
using CoronaFitnessBL.User.Models;

namespace CoronaFitnessBL.User
{
    public class FxUserBusinessOperations : IxUserBusinessOperations
    {
        private IxMongoContext DbContext { get; set; }

        public FxUserBusinessOperations(IxMongoContext context)
        {
            this.DbContext = context;
        }

        public async Task<List<FxUserModel>> GetAll()
        {
            var users = await DbContext.Users.Get(x => true);
            return users.Select(x => new FxUserModel {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            }).ToList();
        }
    }
}