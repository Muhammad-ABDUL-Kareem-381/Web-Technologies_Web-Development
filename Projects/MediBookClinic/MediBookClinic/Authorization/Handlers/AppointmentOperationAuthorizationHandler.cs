using MediBookClinic.Authorization.Operations;
using MediBookClinic.Authorization.Requirments;
using MediBookClinic.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MediBookClinic.Authorization.Handlers
{
    public class AppointmentOperationAuthorizationHandler : AuthorizationHandler<AppointmentOperationAuthorizationRequirement, Appointment>
    {
        private const int CancellationWindowHours = 6;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AppointmentOperationAuthorizationRequirement requirement,Appointment appointment)
        {
            if (appointment == null)
            {
                return Task.CompletedTask;
            }

            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            var doctorIdClaim = context.User.FindFirst("DoctorId")?.Value;
            var patientIdClaim = context.User.FindFirst("PatientId")?.Value;

            // Admin can do everything
            if (userRole == "Admin")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            bool isDoctorOfAppointment = userRole == "Doctor" && doctorIdClaim == appointment.DoctorId.ToString();

            bool isPatientOfAppointment = userRole == "Patient" && patientIdClaim == appointment.PatientId.ToString();

            switch (requirement.Operation)
            {
                case var op when op == AppointmentOperations.Create:
                    if (userRole == "Patient" && patientIdClaim != null)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == AppointmentOperations.Read:
                    if (isDoctorOfAppointment || isPatientOfAppointment)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == AppointmentOperations.Update:
                    if (isDoctorOfAppointment ||
                        (isPatientOfAppointment && appointment.Status == "Pending"))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == AppointmentOperations.Cancel:
                    if ((isDoctorOfAppointment || isPatientOfAppointment) &&  CanCancelAppointment(appointment))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == AppointmentOperations.Complete:
                    if (isDoctorOfAppointment)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;

                case var op when op == AppointmentOperations.Delete:
                    // Only admin can delete (handled above)
                    context.Fail();
                    break;

                default:
                    context.Fail();
                    break;
            }

            return Task.CompletedTask;
        }

        private bool CanCancelAppointment(Appointment appointment)
        {
            DateTime appointmentDateTime = appointment.AppointmentDate.Date.Add(appointment.AppointmentTime);
            TimeSpan timeUntilAppointment = appointmentDateTime - DateTime.Now;

            return timeUntilAppointment.TotalHours > CancellationWindowHours;
        }
    }
}