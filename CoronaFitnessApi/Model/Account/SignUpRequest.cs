using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoronaFitnessApi.Model.Account
{
    public class SignUpRequest
    {
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [PasswordPropertyText] public string Password { get; set; }

        [Required] public string Name { get; set; }
    }
}