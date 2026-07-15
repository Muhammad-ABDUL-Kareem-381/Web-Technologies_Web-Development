using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Requirments
{
    public class ReviewOperationAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Operation { get; }
        public ReviewOperationAuthorizationRequirement(string operation)
        {
            Operation = operation;
        }
    }
}
