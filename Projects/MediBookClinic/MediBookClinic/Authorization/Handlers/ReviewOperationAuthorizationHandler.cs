using MediBookClinic.Authorization.Operations;
using MediBookClinic.Authorization.Requirments;
using MediBookClinic.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MediBookClinic.Authorization.Handlers
{
    public class ReviewOperationAuthorizationHandler : AuthorizationHandler<ReviewOperationAuthorizationRequirement, AppointmentReview>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ReviewOperationAuthorizationRequirement requirement,AppointmentReview review)
        {

            // Guard clauses
            if (context.User == null || review == null)
            {
                return Task.CompletedTask;
            }

            //var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            var patientIdClaim = context.User.FindFirst("PatientId")?.Value;

            // Admin can do everything
            if (userRole == "Admin")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            switch (requirement.Operation)
            {
                case var op when op == ReviewOperations.Create:
                    // Only patients who completed appointment can review
                    if (userRole == "Patient" && patientIdClaim != null)
                    {
                        context.Succeed(requirement);
                    }
                    break;

                case var op when op == ReviewOperations.Read:
                    // Everyone can read reviews (unless anonymous)
                    context.Succeed(requirement);
                    break;

                case var op when op == ReviewOperations.Update:
                    // Only the patient who wrote the review can update within 24 hours
                    if (userRole == "Patient" && patientIdClaim == review.PatientId.ToString())
                    {
                        var hoursSinceCreation = (DateTime.UtcNow - review.CreatedAt).TotalHours;
                        if (hoursSinceCreation <= 24)
                        {
                            context.Succeed(requirement);
                        }
                    }
                    break;

                case var op when op == ReviewOperations.Delete:
                    // Only the patient who wrote the review or admin
                    if (userRole == "Patient" && patientIdClaim == review.PatientId.ToString())
                    {
                        context.Succeed(requirement);
                    }
                    break;
            }

            return Task.CompletedTask;
        }
    }
}

