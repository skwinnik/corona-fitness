using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoronaFitness.Integration.OpenVidu.Models;
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
        private string forAttendee = "simon.skrinnik@home.com";
        
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
            var attendee = await userBop.GetByEmail(forAttendee);
            
            var meeting = new CxMeetingModel()
            {
                Id = "",
                Title = "Test Meeting",
                Description = "This meeting was created in DBGenerator",
                StartTime = DateTime.UtcNow.AddHours(3),
                Duration = 60,
                OwnerId = user.Id,
                IsPublic = true,
                Attendees = new List<CxMeetingAttendeeModel>()
                {
                    new CxMeetingAttendeeModel()
                    {
                        UserId = attendee.Id,
                        Role = EnOvSessionRole.PUBLISHER
                    }
                }
            };

            await meetingBop.CreateMeeting(meeting);
            
            meeting = new CxMeetingModel()
            {
                Id = "",
                Title = "Private Test Meeting",
                Description = "This meeting was created in DBGenerator",
                StartTime = DateTime.UtcNow.AddHours(5),
                Duration = 60,
                OwnerId = user.Id,
                IsPublic = false,
                Attendees = new List<CxMeetingAttendeeModel>()
                {
                    new CxMeetingAttendeeModel()
                    {
                        UserId = attendee.Id,
                        Role = EnOvSessionRole.PUBLISHER
                    }
                }
            };

            await meetingBop.CreateMeeting(meeting);
            
            this.logger.Log(LogLevel.Information, "Meetings are created");

        }
    }
}