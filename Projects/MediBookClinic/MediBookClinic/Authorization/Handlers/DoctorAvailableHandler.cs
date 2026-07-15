using MediBookClinic.Authorization.Requirments;
using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Handlers
{
    public class DoctorAvailableHandler : AuthorizationHandler<DoctorAvailableRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,DoctorAvailableRequirement requirement)
        {
            var isAvailableClaim = context.User.FindFirst("IsAvailableForBooking");

            if (isAvailableClaim != null && bool.TryParse(isAvailableClaim.Value, out bool isAvailable) && isAvailable)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}