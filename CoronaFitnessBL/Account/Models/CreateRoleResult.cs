using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CoronaFitnessBL.Account.Models
{
    public class CreateRoleResult
    {
        public bool Success { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}