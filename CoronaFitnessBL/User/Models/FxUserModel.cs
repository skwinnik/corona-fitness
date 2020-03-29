using CoronaFitnessDb.Entities;

namespace CoronaFitnessBL.User.Models
{
    public class FxUserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IdentityId { get; set; }

        public FxUserModel()
        {
        }

        public FxUserModel(FxUser dbUser)
        {
            this.Id = dbUser.Id;
            this.Name = dbUser.Name;
            this.Email = dbUser.Email;
            this.IdentityId = dbUser.IdentityId;
        }
    }
}