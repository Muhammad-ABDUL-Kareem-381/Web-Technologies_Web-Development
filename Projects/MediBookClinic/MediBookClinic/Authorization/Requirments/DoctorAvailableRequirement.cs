using Microsoft.AspNetCore.Authorization;

namespace MediBookClinic.Authorization.Requirments
{
    public class DoctorAvailableRequirement : IAuthorizationRequirement
    {
        public DoctorAvailableRequirement()
        {
            
        }
    }
}
