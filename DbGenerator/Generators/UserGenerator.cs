using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitnessBL.Account;
using CoronaFitnessDb.Identity;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace DbGenerator.Generators
{
    public class UserGenerator : IxGenerator
    {
        private List<string> userNames = new List<string>(){ "screen0994@gmail.com" };
        private string testPassword = "qwerty123";
        
        private readonly IxAccountBusinessOperations accountBop;
        private readonly ILogger<UserGenerator> logger;

        public UserGenerator(IxAccountBusinessOperations accountBusinessOperations, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<UserGenerator>();
            this.accountBop = accountBusinessOperations;
        }

        public async Task Generate()
        {
            var roles = Enum.GetNames(typeof(IdentityRole))
                .Select(x => new FxIdentityRole() {Name = x});
            
            foreach (var role in roles)
                await this.accountBop.CreateRole(role);

            foreach (var userName in userNames)
                await this.accountBop.SignUp(new FxIdentityUser() {Email = userName, UserName = userName}, testPassword);
            
            this.logger.Log(LogLevel.Information, "Users and Roles are created");
        }
    }
}