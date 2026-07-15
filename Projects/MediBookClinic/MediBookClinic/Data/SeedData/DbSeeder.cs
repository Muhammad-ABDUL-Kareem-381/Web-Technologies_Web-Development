using MediBookClinic.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MediBookClinic.Data.SeedData
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define all roles
            string[] roleNames = { "MasterAdmin", "Admin", "Doctor", "Patient" };

            // Create roles if they don't exist
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task CreateMasterAdminAsync(UserManager<ApplicationUser> userManager)
        {
            // Check if MasterAdmin already exists
            var masterAdminEmail = "masteradmin@medibookclinic.com";
            var existingMasterAdmin = await userManager.FindByEmailAsync(masterAdminEmail);

            if (existingMasterAdmin == null)
            {
                var masterAdmin = new ApplicationUser
                {
                    UserName = masterAdminEmail,
                    Email = masterAdminEmail,
                    FirstName = "Master",
                    LastName = "Administrator",
                    EmailConfirmed = true,
                    PhoneNumber = "+1234567890",
                    PhoneNumberConfirmed = true,
                    DateOfBirth = new DateTime(1980, 1, 1),
                    Gender = "Other",
                    Address = "System Generated",
                    City = "System",
                    Country = "System",
                    PreferredLanguage = "en-US",
                    PreferredTheme = "light",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Create user with password
                var result = await userManager.CreateAsync(masterAdmin, "MasterAdmin@123");

                if (result.Succeeded)
                {
                    // Assign MasterAdmin role
                    await userManager.AddToRoleAsync(masterAdmin, "MasterAdmin");

                    // Add claims for MasterAdmin
                    var claims = new[]
                    {
                        new System.Security.Claims.Claim("UserType", "MasterAdmin"),
                        new System.Security.Claims.Claim("IsActive", "True"),
                        new System.Security.Claims.Claim("Permission", "ManageUsers"),
                        new System.Security.Claims.Claim("Permission", "ManageAdmins"),
                        new System.Security.Claims.Claim("Permission", "ManageSystemSettings"),
                        new System.Security.Claims.Claim("Permission", "ViewAllAppointments"),
                        new System.Security.Claims.Claim("Permission", "ViewReports"),
                        new System.Security.Claims.Claim("Permission", "FullSystemAccess")
                    };

                    await userManager.AddClaimsAsync(masterAdmin, claims);
                }
            }
        }
    }
}
