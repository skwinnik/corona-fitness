using CoronaFitnessDb.Entities;

namespace CoronaFitnessBL.User.Models
{
    public class CxUserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IdentityId { get; set; }
        public bool CanCreateMeetings { get; set; }

        public CxUserModel()
        {
        }

        public CxUserModel(CxUser dbUser)
        {
            this.Id = dbUser.Id;
            this.Name = dbUser.Name;
            this.Email = dbUser.Email;
            this.IdentityId = dbUser.IdentityId;
            this.CanCreateMeetings = dbUser.CanCreateMeetings;
        }
    }
}