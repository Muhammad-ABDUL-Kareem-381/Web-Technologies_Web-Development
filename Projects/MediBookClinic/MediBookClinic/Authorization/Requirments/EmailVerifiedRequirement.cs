using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Requirments
{
    public class EmailVerifiedRequirement : IAuthorizationRequirement
    {
        public EmailVerifiedRequirement()
        {
            
        }
    }
}
