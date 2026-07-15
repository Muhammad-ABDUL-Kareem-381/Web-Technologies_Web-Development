using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Requirments
{
    public class AppointmentOperationAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Operation { get; }
        public AppointmentOperationAuthorizationRequirement(string operation)
        {
            Operation = operation;
        }
    }
}
