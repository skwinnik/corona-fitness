﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CoronaFitnessApi.Model.Account
{
    public class SignUpResponse
    {
        public bool Success { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}