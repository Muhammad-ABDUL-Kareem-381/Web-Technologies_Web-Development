using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Requirments
{
    public class ActiveAccountRequirement : IAuthorizationRequirement
    {
        public ActiveAccountRequirement()
        {
            
        }
    }
}
