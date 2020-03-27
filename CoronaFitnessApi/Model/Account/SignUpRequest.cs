using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoronaFitnessApi.Model.Account
{
    public class SignUpRequest
    {
        [Required] [PasswordPropertyText] public string Password { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
    }
}