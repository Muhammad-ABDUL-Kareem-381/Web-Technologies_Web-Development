using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Requirments
{
    public class PatientRecordOperationAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Operation { get; }
        public PatientRecordOperationAuthorizationRequirement(string operation)
        {
            Operation = operation;
        }
    }
}
