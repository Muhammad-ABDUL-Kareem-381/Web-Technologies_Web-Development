using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Requirments
{
    public class DoctorProfileOperationAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Operation { get; }

        public DoctorProfileOperationAuthorizationRequirement(string operation)
        {
            Operation = operation;
        }
    }
}
