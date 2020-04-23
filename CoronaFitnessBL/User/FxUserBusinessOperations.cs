using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb;
using CoronaFitnessDb.Entities;
using MongoDB.Driver;

namespace CoronaFitnessBL.User
{
    public class CxUserBusinessOperations : IxUserBusinessOperations
    {
        private IxMongoDataContext DbContext { get; set; }

        public CxUserBusinessOperations(IxMongoDataContext context)
        {
            this.DbContext = context;
        }

        public async Task<List<CxUserModel>> GetAll()
        {
            var users = await DbContext.Users.GetAsync(x => true);
            return users.Select(x => new CxUserModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                IdentityId = x.IdentityId
            }).ToList();
        }

        public async Task Create(CxUserModel user)
        {
            await DbContext.Users.AddAsync(new CxUser()
            {
                Id = "", Email = user.Email, Name = user.Name, IdentityId = user.IdentityId,
                CanCreateMeetings = user.CanCreateMeetings
            });
        }

        public async Task SetCanCreateMeetings(string id, bool canCreate)
        {
            await DbContext.Users.UpdateAsync(x => x.Id == id,
                new UpdateDefinitionBuilder<CxUser>()
                    .Set(x => x.CanCreateMeetings, canCreate)
            );
        }

        public Task<CxUserModel> GetByIdentityId(string identityId)
        {
            return DbContext.Users
                .GetSingleAsync(x => x.IdentityId == identityId)
                .ContinueWith(x => new CxUserModel(x.Result));
        }
        
        public async Task<List<CxUserModel>> GetById(List<string> ids)
        {
            var users = await DbContext.Users
                .GetAsync(x => ids.Contains(x.Id));

            return users.Select(x => new CxUserModel(x)).ToList();
        }

        public Task<CxUserModel> GetByEmail(string email)
        {
            return DbContext.Users
                .GetSingleAsync(x => x.Email == email)
                .ContinueWith(x => new CxUserModel(x.Result));
        }
    }
}