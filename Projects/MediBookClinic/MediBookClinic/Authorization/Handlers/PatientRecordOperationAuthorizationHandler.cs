using MediBookClinic.Authorization.Operations;
using MediBookClinic.Authorization.Requirments;
using MediBookClinic.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MediBookClinic.Authorization.Handlers
{
    public class PatientRecordOperationAuthorizationHandler :
            AuthorizationHandler<PatientRecordOperationAuthorizationRequirement, Patient>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PatientRecordOperationAuthorizationRequirement requirement, Patient patient)
        {

            // Guard clause
            if (patient == null)
            {
                return Task.CompletedTask;
            }

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            var patientIdClaim = context.User.FindFirst("PatientId")?.Value;
            var doctorIdClaim = context.User.FindFirst("DoctorId")?.Value;

            // Admin can do everything
            if (userRole == "Admin")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            switch (requirement.Operation)
            {
                case var op when op == PatientRecordOperations.Read:
                    // Patient can read their own, Doctor can read their patients'
                    if ((userRole == "Patient" && patientIdClaim == patient.PatientId.ToString()) ||
                        (userRole == "Doctor" && doctorIdClaim != null))
                    {
                        context.Succeed(requirement);
                    }
                    break;

                case var op when op == PatientRecordOperations.Update:
                    // Only the patient themselves can update basic info
                    if (userRole == "Patient" && patientIdClaim == patient.PatientId.ToString())
                    {
                        context.Succeed(requirement);
                    }
                    break;

                case var op when op == PatientRecordOperations.ViewMedicalHistory:
                    // Patient can view their own, Doctor can view their patients'
                    if ((userRole == "Patient" && patientIdClaim == patient.PatientId.ToString()) ||
                        (userRole == "Doctor" && doctorIdClaim != null))
                    {
                        context.Succeed(requirement);
                    }
                    break;

                case var op when op == PatientRecordOperations.UpdateMedicalHistory:
                    // Only doctors can update medical history
                    if (userRole == "Doctor" && doctorIdClaim != null)
                    {
                        context.Succeed(requirement);
                    }
                    break;

                case var op when op == PatientRecordOperations.ManagePreferences:
                    // Only the patient themselves
                    if (userRole == "Patient" && patientIdClaim == patient.PatientId.ToString())
                    {
                        context.Succeed(requirement);
                    }
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
