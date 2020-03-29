using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitnessBL.Account;
using CoronaFitnessBL.Meeting;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User;
using Microsoft.Extensions.Logging;

namespace DbGenerator.Generators
{
    public class MeetingGenerator : IxGenerator
    {
        public Type After => typeof(UserGenerator);
        public int Priority => 1;

        private string forUser = "screen0994@gmail.com";

        private readonly IxUserBusinessOperations userBop;
        private readonly IxMeetingBusinessOperations meetingBop;
        private readonly ILogger<MeetingGenerator> logger;

        public MeetingGenerator(IxUserBusinessOperations userBop, IxMeetingBusinessOperations meetingBop,
            ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<MeetingGenerator>();
            this.userBop = userBop;
            this.meetingBop = meetingBop;
        }

        public async Task Generate()
        {
            var user = await userBop.GetByEmail(forUser);
            
            var meeting = new FxMeetingModel()
            {
                Id = "",
                Title = "Test Meeting",
                Description = "This meeting was created in DBGenerator",
                Attendees = new string[0],
                OwnerId = user.Id
            };

            await meetingBop.CreateMeeting(meeting);
            
            this.logger.Log(LogLevel.Information, "Meetings are created");

        }
    }
}