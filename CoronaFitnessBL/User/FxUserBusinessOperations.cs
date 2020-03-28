﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaFitnessBL.User.Models;
using CoronaFitnessDb;
using CoronaFitnessDb.Entities;

namespace CoronaFitnessBL.User
{
    public class FxUserBusinessOperations : IxUserBusinessOperations
    {
        private IxMongoDataContext DbContext { get; set; }

        public FxUserBusinessOperations(IxMongoDataContext context)
        {
            this.DbContext = context;
        }

        public async Task<List<FxUserModel>> GetAll()
        {
            var users = await DbContext.Users.Get(x => true);
            return users.Select(x => new FxUserModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            }).ToList();
        }

        public async Task Create(FxUserModel user)
        {
            await DbContext.Users.AddAsync(new FxUser()
                {Id="", Email = user.Email, Name = user.Name, IdentityId = user.IdentityId});
        }
    }
}