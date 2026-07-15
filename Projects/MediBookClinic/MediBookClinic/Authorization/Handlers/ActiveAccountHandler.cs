using MediBookClinic.Authorization.Requirments;
using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Handlers
{
    public class ActiveAccountHandler : AuthorizationHandler<ActiveAccountRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,ActiveAccountRequirement requirement)
        {
            var isActiveClaim = context.User.FindFirst("IsActive");

            if (isActiveClaim != null && bool.TryParse(isActiveClaim.Value, out bool isActive) && isActive)
            {
                context.Succeed(requirement);
            }
            else
            {
                // Explicitly fail if not active or claim not found
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}