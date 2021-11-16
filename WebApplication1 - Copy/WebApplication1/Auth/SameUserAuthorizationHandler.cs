using BidToBuy.Auth.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidToBuy.Auth
{
    public class SameUserAuthorizationHandler:AuthorizationHandler<SameUserRequirement, IUserOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, IUserOwnedResource resource)
        {
            if (context.User.IsInRole(RestUserRoles.Admin)||context.User.FindFirst(CustomClaims.UserId).Value == resource.User_id)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public record SameUserRequirement : IAuthorizationRequirement;
}
