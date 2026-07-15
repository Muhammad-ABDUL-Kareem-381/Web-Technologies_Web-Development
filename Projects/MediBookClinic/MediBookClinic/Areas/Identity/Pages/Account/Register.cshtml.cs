// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace MediBookClinic.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IEmailService _emailService;

        public RegisterModel(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, SignInManager<ApplicationUser> signInManager, ILogger<RegisterModel> logger, IEmailSender emailSender,      ApplicationDbContext context, IDoctorService doctorService, IPatientService patientService, IEmailService emailService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _doctorService = doctorService;
            _patientService = patientService;
            _emailService = emailService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // Common fields
            [Required]
            [StringLength(50)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(50)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "I am registering as")]
            public string UserType { get; set; } // Only "Doctor" or "Patient" allowed

            [DataType(DataType.Date)]
            [Display(Name = "Date of Birth")]
            public DateTime? DateOfBirth { get; set; }

            [StringLength(10)]
            public string Gender { get; set; }

            [StringLength(200)]
            public string Address { get; set; }

            [StringLength(100)]
            public string City { get; set; }

            [StringLength(100)]
            public string Country { get; set; }

            // Doctor-specific fields
            [StringLength(100)]
            public string Specialization { get; set; }

            [StringLength(50)]
            [Display(Name = "License Number")]
            public string LicenseNumber { get; set; }

            [Display(Name = "Years of Experience")]
            public int YearsOfExperience { get; set; }

            [StringLength(200)]
            public string Qualification { get; set; }

            [Display(Name = "Consultation Fee")]
            public decimal ConsultationFee { get; set; }

            [StringLength(1000)]
            public string Biography { get; set; }

            // Patient-specific fields
            [StringLength(10)]
            [Display(Name = "Blood Group")]
            public string BloodGroup { get; set; }

            [StringLength(500)]
            public string Allergies { get; set; }

            [StringLength(1000)]
            [Display(Name = "Medical History")]
            public string MedicalHistory { get; set; }

            [StringLength(100)]
            [Display(Name = "Emergency Contact Name")]
            public string EmergencyContactName { get; set; }

            [Phone]
            [Display(Name = "Emergency Contact Phone")]
            public string EmergencyContactPhone { get; set; }

            [StringLength(100)]
            [Display(Name = "Insurance Provider")]
            public string InsuranceProvider { get; set; }

            [StringLength(50)]
            [Display(Name = "Insurance Number")]
            public string InsuranceNumber { get; set; }

            [Required]
            [Display(Name = "I accept the Terms and Conditions")]
            public bool AcceptTerms { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // Validate UserType - ONLY Doctor or Patient allowed (NO Admin)
                if (Input.UserType != "Doctor" && Input.UserType != "Patient")
                {
                    ModelState.AddModelError(string.Empty, "Invalid user type selected. Only Doctor or Patient registration is allowed.");
                    return Page();
                }

                // Validate Terms acceptance
                if (!Input.AcceptTerms)
                {
                    ModelState.AddModelError(string.Empty, "You must accept the Terms and Conditions.");
                    return Page();
                }

                // Validate role-specific required fields
                if (Input.UserType == "Doctor")
                {
                    if (string.IsNullOrEmpty(Input.Specialization) || string.IsNullOrEmpty(Input.LicenseNumber) ||  Input.YearsOfExperience <= 0 || Input.ConsultationFee <= 0)
                    {
                        ModelState.AddModelError(string.Empty, "All doctor-specific fields are required.");
                        return Page();
                    }
                }
                else if (Input.UserType == "Patient")
                {
                    if (string.IsNullOrEmpty(Input.BloodGroup))
                    {
                        ModelState.AddModelError(string.Empty, "Blood Group is required for patients.");
                        return Page();
                    }
                }

                // Create ApplicationUser
                var user = CreateUser();
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.DateOfBirth = Input.DateOfBirth;
                user.Gender = Input.Gender;
                user.Address = Input.Address;
                user.City = Input.City;
                user.Country = Input.Country;
                user.PhoneNumber = Input.PhoneNumber;
                user.PreferredLanguage = "en-US";
                user.PreferredTheme = "light";

                // CRITICAL: Set IsActive based on UserType
                // Doctors are INACTIVE until approved, Patients are ACTIVE immediately
                user.IsActive = Input.UserType == "Patient";

                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    try
                    {
                        // Assign role
                        await _userManager.AddToRoleAsync(user, Input.UserType);

                        // Add base claims
                        var claims = new List<Claim>
                        {
                            new Claim("UserType", Input.UserType),
                            new Claim("IsActive", user.IsActive.ToString())
                        };

                        // Create Doctor or Patient record and add specific claims
                        if (Input.UserType == "Doctor")
                        {
                            var doctor = new Doctor
                            {
                                UserId = user.Id,
                                Specialization = Input.Specialization,
                                LicenseNumber = Input.LicenseNumber,
                                YearsOfExperience = Input.YearsOfExperience,
                                Qualification = Input.Qualification,
                                Biography = Input.Biography,
                                ConsultationFee = Input.ConsultationFee,
                                Rating = 0.00m,
                                TotalReviews = 0,
                                IsAvailableForBooking = false, // Not available until approved
                                CreatedAt = DateTime.UtcNow
                            };

                            var createdDoctor = await _doctorService.CreateDoctorAsync(doctor);

                            // Add DoctorId claims (even though inactive)
                            claims.Add(new Claim("DoctorId", createdDoctor.DoctorId.ToString()));
                            claims.Add(new Claim(ClaimTypes.DateOfBirth, (Input.DateOfBirth ?? DateTime.Now).ToString("yyyy-MM-dd")));
                            claims.Add(new Claim("EmailConfirmed", user.EmailConfirmed.ToString()));
                            claims.Add(new Claim("Specialization", Input.Specialization ?? "General"));
                            claims.Add(new Claim("IsAvailableForBooking", "False"));

                            // Send pending approval email to doctor
                            await _emailService.SendEmailAsync(
                                user.Email,
                                "Registration Received - Pending Approval",
                                $@"<html>
                                <body style='font-family: Arial, sans-serif;'>
                                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                                        <h2 style='color: #FF9800;'>Registration Received</h2>
                                        <p>Dear Dr. {user.FirstName} {user.LastName},</p>
                                        <p>Thank you for registering with MediBook Clinic.</p>
                                        <p><strong>Your account is currently pending approval by our administrative team.</strong></p>
                                        <p>You will receive an email notification once your account has been reviewed and approved.</p>
                                        <p>This process typically takes 1-2 business days.</p>
                                        <p style='margin-top: 30px; color: #666; font-size: 12px;'>
                                            If you have any questions, please contact our support team.
                                        </p>
                                    </div>
                                </body>
                                </html>"
                            );

                            // Notify all Admins and MasterAdmins about new doctor registration
                            await NotifyAdminsOfNewDoctorAsync(user, createdDoctor);
                        }
                        else if (Input.UserType == "Patient")
                        {
                            var patient = new Patient
                            {
                                UserId = user.Id,
                                BloodGroup = Input.BloodGroup,
                                Allergies = Input.Allergies,
                                MedicalHistory = Input.MedicalHistory,
                                EmergencyContactName = Input.EmergencyContactName,
                                EmergencyContactPhone = Input.EmergencyContactPhone,
                                InsuranceProvider = Input.InsuranceProvider,
                                InsuranceNumber = Input.InsuranceNumber,
                                CreatedAt = DateTime.UtcNow
                            };

                            var createdPatient = await _patientService.CreatePatientAsync(patient);

                            // Add PatientId claim
                            claims.Add(new Claim("PatientId", createdPatient.PatientId.ToString()));
                            claims.Add(new Claim(ClaimTypes.DateOfBirth, (Input.DateOfBirth ?? DateTime.Now).ToString("yyyy-MM-dd")));
                            claims.Add(new Claim("EmailConfirmed", user.EmailConfirmed.ToString()));

                            // Patients get immediate permissions
                            claims.Add(new Claim("Permission", "BookAppointments"));
                            claims.Add(new Claim("Permission", "ViewOwnRecords"));
                            claims.Add(new Claim("Permission", "ManageProfile"));
                            claims.Add(new Claim("Permission", "ManagePreferences"));
                            claims.Add(new Claim("Permission", "WriteReviews"));

                            // Send welcome email to patient
                            await _emailService.SendWelcomeEmailAsync(
                                user.Email,
                                user.FirstName,
                                user.LastName,
                                "Patient"
                            );
                        }

                        // Add all claims to user
                        await _userManager.AddClaimsAsync(user, claims);

                        // Generate email confirmation token
                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        var callbackUrl = Url.Page("/Account/ConfirmEmail",pageHandler: null, values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },protocol: Request.Scheme);

                        // Sign in logic based on UserType
                        if (Input.UserType == "Patient")
                        {
                            // Patients can sign in immediately
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            // Doctors cannot sign in until approved
                            return RedirectToPage("RegisterConfirmation", new
                            {
                                email = Input.Email,
                                returnUrl = returnUrl,
                                userType = "Doctor",
                                pendingApproval = true
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating doctor/patient record for user {Email}", Input.Email);

                        // Rollback user creation
                        await _userManager.DeleteAsync(user);

                        ModelState.AddModelError(string.Empty, "An error occurred while creating your profile. Please try again.");
                        return Page();
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private async Task NotifyAdminsOfNewDoctorAsync(ApplicationUser doctorUser, Doctor doctor)
        {
            try
            {
                // Get all users with Admin or MasterAdmin role
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                var masterAdminUsers = await _userManager.GetUsersInRoleAsync("MasterAdmin");

                var allAdmins = adminUsers.Concat(masterAdminUsers).Distinct();

                foreach (var admin in allAdmins)
                {
                    await _emailService.SendAdminNotificationAsync(
                        admin.Email,
                        $"Dr. {doctorUser.FirstName} {doctorUser.LastName}",
                        doctorUser.Email,
                        doctorUser.PhoneNumber,
                        doctor.Specialization,
                        doctor.LicenseNumber,
                        doctor.YearsOfExperience,
                        doctor.DoctorId
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to notify admins of new doctor registration");
                // Don't throw - registration should succeed even if notifications fail
            }
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}