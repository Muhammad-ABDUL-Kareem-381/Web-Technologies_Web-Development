using MediBookClinic.Authorization.Operations;
using MediBookClinic.Authorization.Requirments;
using MediBookClinic.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MediBookClinic.Authorization.Handlers
{
    public class DoctorProfileOperationAuthorizationHandler : AuthorizationHandler<DoctorProfileOperationAuthorizationRequirement, Doctor>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,            
            DoctorProfileOperationAuthorizationRequirement requirement, Doctor doctor)
        {
            // Guard clause for null doctor
            if (doctor == null)
            {
                return Task.CompletedTask;
            }

            //var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            var doctorIdClaim = context.User.FindFirst("DoctorId")?.Value;

            // Admin can do everything
            if (userRole == "Admin")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // Check if user is the doctor
            bool isOwnProfile = userRole == "Doctor" && doctorIdClaim == doctor.DoctorId.ToString();

            switch (requirement.Operation)
            {
                case var op when op == DoctorProfileOperations.Read:
                    // Everyone can read public doctor profiles
                    context.Succeed(requirement);
                    break;

                case var op when op == DoctorProfileOperations.Update:
                    if (isOwnProfile)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == DoctorProfileOperations.ManageAvailability:
                    if (isOwnProfile)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == DoctorProfileOperations.ManageSpecialDates:
                    if (isOwnProfile)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == DoctorProfileOperations.ViewAnalytics:
                    if (isOwnProfile)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == DoctorProfileOperations.Delete:
                    // Only admin can delete (already handled above)
                    context.Fail();
                    break;

                default:
                    context.Fail();
                    break;
            }

            return Task.CompletedTask;
        }
    }
}