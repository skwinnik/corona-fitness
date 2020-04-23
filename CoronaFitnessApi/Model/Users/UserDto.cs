using CoronaFitnessBL.User.Models;

namespace CoronaFitnessApi.Model.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool CanCreateMeetings { get; set; }
        
        public UserDto()
        {
            
        }

        public UserDto(FxUserModel model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.CanCreateMeetings = model.CanCreateMeetings;
        }
    }
}