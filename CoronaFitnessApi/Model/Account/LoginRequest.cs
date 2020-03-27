using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoronaFitnessApi.Model.Account
{
    public class LoginRequest
    {
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [PasswordPropertyText] public string Password { get; set; }
    }
}