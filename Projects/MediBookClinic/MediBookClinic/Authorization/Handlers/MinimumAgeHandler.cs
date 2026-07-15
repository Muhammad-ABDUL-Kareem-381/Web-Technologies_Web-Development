using MediBookClinic.Authorization.Requirments;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MediBookClinic.Authorization.Handlers
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);

            if (dateOfBirthClaim == null)
            {
                return Task.CompletedTask;
            }

            if (!DateTime.TryParse(dateOfBirthClaim.Value, out var dateOfBirth))
            {
                return Task.CompletedTask;
            }

            // Calculate accurate age
            var today = DateTime.Today;
            var userAge = today.Year - dateOfBirth.Year;

            // Adjust if birthday hasn't occurred yet this year
            if (dateOfBirth.Date > today.AddYears(-userAge))
            {
                userAge--;
            }

            if (userAge >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}