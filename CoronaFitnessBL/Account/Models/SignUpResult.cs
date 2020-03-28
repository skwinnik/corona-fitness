using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CoronaFitnessBL.Account.Models
{
    public class SignUpResult
    {
        public bool Success { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }

        public SignUpResult()
        {
        }

        public SignUpResult(IdentityResult result)
        {
            this.Success = result.Succeeded;
            this.Errors = result.Errors;
        }
    }
}