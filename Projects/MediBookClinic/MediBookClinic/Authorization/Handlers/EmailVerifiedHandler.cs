using Microsoft.AspNetCore.Authorization;
using MediBookClinic.Authorization.Requirments;

namespace MediBookClinic.Authorization.Handlers
{
    public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
        {
            var emailVerifiedClaim = context.User.FindFirst("EmailConfirmed");

            if (emailVerifiedClaim != null && emailVerifiedClaim.Value == "True")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
