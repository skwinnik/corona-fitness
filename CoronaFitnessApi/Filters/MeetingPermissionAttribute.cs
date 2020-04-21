﻿using System.Threading.Tasks;
using CoronaFitnessBL.Meeting;
using CoronaFitnessBL.Meeting.Models;
using CoronaFitnessBL.User.UserContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoronaFitnessApi.Filters
{
    public class MeetingPermissionAttribute : TypeFilterAttribute
    {
        public MeetingPermissionAttribute(EnMeetingAccessLevel level) : base(typeof(MeetingPermissionAttributeImpl))
        {
            Arguments = new object[] { level };
        }
    }

    public class MeetingPermissionAttributeImpl : IAsyncActionFilter
    {
        private readonly IxMeetingBusinessOperations meetingBop;
        private readonly IxUserContext userContext;
        private readonly EnMeetingAccessLevel level;

        public MeetingPermissionAttributeImpl(IxMeetingBusinessOperations meetingBop, IxUserContext userContext, EnMeetingAccessLevel level)
        {
            this.meetingBop = meetingBop;
            this.userContext = userContext;
            this.level = level;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var currentUser = await userContext.GetCurrentUser();
            var result = await meetingBop.CheckMeetingAccessLevel(currentUser.Id, context.RouteData.Values["meetingId"].ToString(), this.level);

            if (!result)
            {
                context.Result = new BadRequestObjectResult("Not allowed");
                return;
            }
            
            await next();
        }
    }
}