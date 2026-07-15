using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediBookClinic.Models.ViewModels.Patient.Profile
{
    // ViewModel for displaying and editing patient profile information
    public class PatientProfileViewModel
    {
        public int PatientId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [StringLength(200)]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [StringLength(100)]
        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Url]
        [Display(Name = "Profile Image")]
        public string? ProfileImageUrl { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Allergies")]
        [DataType(DataType.MultilineText)]
        public string? Allergies { get; set; }

        [StringLength(1000)]
        [Display(Name = "Medical History")]
        [DataType(DataType.MultilineText)]
        public string? MedicalHistory { get; set; }

        [StringLength(100)]
        [Display(Name = "Emergency Contact Name")]
        public string? EmergencyContactName { get; set; }

        [Phone]
        [Display(Name = "Emergency Contact Phone")]
        public string? EmergencyContactPhone { get; set; }

        [StringLength(100)]
        [Display(Name = "Insurance Provider")]
        public string? InsuranceProvider { get; set; }

        [StringLength(50)]
        [Display(Name = "Insurance Number")]
        public string? InsuranceNumber { get; set; }

        [Display(Name = "Account Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Preferred Language")]
        public string PreferredLanguage { get; set; } = "en-US";

        [Display(Name = "Preferred Theme")]
        public string PreferredTheme { get; set; } = "light";

        [Display(Name = "Total Appointments")]
        public int TotalAppointments { get; set; }

        [Display(Name = "Completed Appointments")]
        public int CompletedAppointments { get; set; }

        [Display(Name = "Cancelled Appointments")]
        public int CancelledAppointments { get; set; }

        [Display(Name = "No-Show Appointments")]
        public int NoShowAppointments { get; set; }

        [Display(Name = "Total Doctors Visited")]
        public int TotalDoctorsVisited { get; set; }

        [Display(Name = "Total Amount Spent")]
        [DataType(DataType.Currency)]
        public decimal? TotalAmountSpent { get; set; }

        [Display(Name = "Average Rating Given")]
        public decimal? AverageRatingGiven { get; set; }

        [Display(Name = "Total Reviews Written")]
        public int TotalReviewsWritten { get; set; }

        [Display(Name = "Member Since")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        // Full name    
        public string FullName => $"{FirstName} {LastName}";

        // Profile image or default    
        public string ProfileImageOrDefault => string.IsNullOrEmpty(ProfileImageUrl) ? "/images/default-patient-avatar.png" : ProfileImageUrl;
    
        // Age
        public int? Age
        {
            get
            {
                if (!DateOfBirth.HasValue)
                {
                    return null;
                }
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Value.Year;
                if (DateOfBirth.Value.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
    
        // Account status badge HTML
        public string AccountStatusBadge => IsActive ? "<span class='badge bg-success'>Active</span>" : "<span class='badge bg-danger'>Inactive</span>";
    
        // Completion rate percentage
        public double CompletionRate
        {
            get
            {
                if (TotalAppointments == 0)
                {
                    return 0;
                }
                return Math.Round((double)CompletedAppointments / TotalAppointments * 100, 1);
            }
        }
    
        // Cancellation rate percentage
        public double CancellationRate
        {
            get
            {
                if (TotalAppointments == 0)
                {
                    return 0;
                }
                return Math.Round((double)CancelledAppointments / TotalAppointments * 100, 1);
            }
        }

        // Member duration text    
        public string MemberSinceText
        {
            get
            {
                var duration = DateTime.UtcNow - CreatedAt;
                if (duration.TotalDays < 30)
                {
                    return "New member";
                }
                if (duration.TotalDays < 365)
                {
                    return $"{(int)(duration.TotalDays / 30)} months";
                }
                return $"{(int)(duration.TotalDays / 365)} years";
            }
        }
    
        // Whether profile is complete
        public bool IsProfileComplete
        {
            get
            {
                return !string.IsNullOrEmpty(BloodGroup) && !string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(EmergencyContactName) && !string.IsNullOrEmpty(EmergencyContactPhone) && DateOfBirth.HasValue;
            }
        }
    
        // Profile completion percentage
        public int ProfileCompletionPercentage
        {
            get
            {
                int total = 12;
                int completed = 0;

                if (!string.IsNullOrEmpty(FirstName))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(LastName))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(Email))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(PhoneNumber))
                {
                    completed++;
                }
                if (DateOfBirth.HasValue)
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(Gender))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(BloodGroup))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(Address))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(City))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(EmergencyContactName))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(EmergencyContactPhone))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(ProfileImageUrl))
                {
                    completed++;
                }
                return (int)Math.Round((double)completed / total * 100);
            }
        }
    
        // Has insurance information
        public bool HasInsurance => !string.IsNullOrEmpty(InsuranceProvider) && !string.IsNullOrEmpty(InsuranceNumber);

        // Has allergies recorded    
        public bool HasAllergies => !string.IsNullOrEmpty(Allergies);
    
        // Has medical history recorded
        public bool HasMedicalHistory => !string.IsNullOrEmpty(MedicalHistory);

        // Has emergency contact    
        public bool HasEmergencyContact => !string.IsNullOrEmpty(EmergencyContactName) && !string.IsNullOrEmpty(EmergencyContactPhone);

        // Reviews written text    
        public string ReviewsWrittenText => TotalReviewsWritten switch
        {
            0 => "No reviews written",
            1 => "1 review written",
            _ => $"{TotalReviewsWritten} reviews written"
        };
    
        // Upcoming appointments count
        public int UpcomingAppointmentsCount { get; set; }

        // Has upcoming appointments    
        public bool HasUpcomingAppointments => UpcomingAppointmentsCount > 0;

        //Fields For Edit Form (Dropdowns)
        public List<SelectListItem>? BloodGroupList { get; set; }
        public List<SelectListItem>? GenderList { get; set; }
        public List<SelectListItem>? LanguageList { get; set; }
        public List<SelectListItem>? ThemeList { get; set; }
    }
}