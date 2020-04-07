using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb;
using CoronaFitnessDb.Entities;
using MongoDB.Driver;

namespace CoronaFitnessBL.User
{
    public class FxUserBusinessOperations : IxUserBusinessOperations
    {
        private IxMongoDataContext DbContext { get; set; }

        public FxUserBusinessOperations(IxMongoDataContext context)
        {
            this.DbContext = context;
        }

        public async Task<List<FxUserModel>> GetAll()
        {
            var users = await DbContext.Users.GetAsync(x => true);
            return users.Select(x => new FxUserModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                IdentityId = x.IdentityId
            }).ToList();
        }

        public async Task Create(FxUserModel user)
        {
            await DbContext.Users.AddAsync(new FxUser()
            {
                Id = "", Email = user.Email, Name = user.Name, IdentityId = user.IdentityId,
                CanCreateMeetings = user.CanCreateMeetings
            });
        }

        public async Task SetCanCreateMeetings(string id, bool canCreate)
        {
            await DbContext.Users.UpdateAsync(x => x.Id == id,
                new UpdateDefinitionBuilder<FxUser>()
                    .Set(x => x.CanCreateMeetings, canCreate)
            );
        }

        public Task<FxUserModel> GetByIdentityId(string identityId)
        {
            return DbContext.Users
                .GetSingleAsync(x => x.IdentityId == identityId)
                .ContinueWith(x => new FxUserModel(x.Result));
        }
        
        public async Task<List<FxUserModel>> GetById(List<string> ids)
        {
            var users = await DbContext.Users
                .GetAsync(x => ids.Contains(x.Id));

            return users.Select(x => new FxUserModel(x)).ToList();
        }

        public Task<FxUserModel> GetByEmail(string email)
        {
            return DbContext.Users
                .GetSingleAsync(x => x.Email == email)
                .ContinueWith(x => new FxUserModel(x.Result));
        }
    }
}