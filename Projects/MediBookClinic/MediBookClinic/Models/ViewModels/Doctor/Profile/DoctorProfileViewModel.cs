using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediBookClinic.Models.ViewModels.Doctor.Profile
{
    // ViewModel for displaying and maybe editing doctor profile information
    public class DoctorProfileViewModel
    {
        public int DoctorId { get; set; }

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
        [StringLength(100)]
        [Display(Name = "Specialization")]
        public string Specialization { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; } = string.Empty;

        [Required]
        [Range(0, 100)]
        [Display(Name = "Years of Experience")]
        public int YearsOfExperience { get; set; }

        [StringLength(200)]
        [Display(Name = "Qualification")]
        public string? Qualification { get; set; }

        [StringLength(1000)]
        [Display(Name = "Biography")]
        [DataType(DataType.MultilineText)]
        public string? Biography { get; set; }

        [Required]
        [Range(0, 100000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Consultation Fee")]
        public decimal ConsultationFee { get; set; }

        [Display(Name = "Rating")]
        [Range(0, 5)]
        public decimal Rating { get; set; }

        [Display(Name = "Total Reviews")]
        public int TotalReviews { get; set; }

        [Display(Name = "Available for Booking")]
        public bool IsAvailableForBooking { get; set; }

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

        [Display(Name = "Total Patients Served")]
        public int TotalPatientsServed { get; set; }

        [Display(Name = "Average Session Duration")]
        public int? AverageSessionDuration { get; set; } // in minutes

        [Display(Name = "Total Revenue")]
        [DataType(DataType.Currency)]
        public decimal? TotalRevenue { get; set; }

        [Display(Name = "Member Since")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        // Full name    
        public string FullName => $"Dr. {FirstName} {LastName}";

        // Profile image or default    
        public string ProfileImageOrDefault => string.IsNullOrEmpty(ProfileImageUrl) ? "/images/default-doctor-avatar.png" : ProfileImageUrl;

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
 
        // Years of experience display    
        public string ExperienceText => YearsOfExperience == 1 ? "1 year" : $"{YearsOfExperience} years";

        // Rating stars HTML    
        public string RatingStarsHtml
        {
            get
            {
                var fullStars = (int)Math.Floor(Rating);
                var hasHalfStar = Rating - fullStars >= 0.5m;
                var emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

                var html = "";
                for (int i = 0; i < fullStars; i++)
                {
                    html += "<i class='fas fa-star text-warning'></i>";
                }
                if (hasHalfStar)
                {
                    html += "<i class='fas fa-star-half-alt text-warning'></i>";
                }
                for (int i = 0; i < emptyStars; i++)
                {
                    html += "<i class='far fa-star text-warning'></i>";
                }
                return html;
            }
        }

        // Review count text    
        public string ReviewCountText => TotalReviews switch
        {
            0 => "No reviews yet",
            1 => "1 review",
            _ => $"{TotalReviews} reviews"
        };

        // Availability badge
        public string AvailabilityBadge => IsAvailableForBooking ? "<span class='badge bg-success'>Available</span>" : "<span class='badge bg-secondary'>Not Available</span>";

        // Account status badge    
        public string AccountStatusBadge => IsActive ? "<span class='badge bg-success'>Active</span>" : "<span class='badge bg-warning'>Pending Approval</span>";

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

        // Whether profile is complete (all fields filled)
        public bool IsProfileComplete
        {
            get
            {
                return !string.IsNullOrEmpty(Specialization) && !string.IsNullOrEmpty(LicenseNumber) && !string.IsNullOrEmpty(Qualification) && !string.IsNullOrEmpty(Biography) && !string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Address) && ConsultationFee > 0;
            }
        }
    
        // Profile completion percentage
        public int ProfileCompletionPercentage
        {
            get
            {
                int total = 10;
                int completed = 0;

                if (!string.IsNullOrEmpty(FirstName))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(LastName))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(Specialization))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(LicenseNumber))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(Qualification))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(Biography))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(City))
                {
                    completed++;
                }
                if (!string.IsNullOrEmpty(Address))
                {
                    completed++;
                }
                if (ConsultationFee > 0)
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

        // Fields For Edit Form (Dropdowns)
        public List<SelectListItem>? SpecializationList { get; set; }
        public List<SelectListItem>? GenderList { get; set; }
        public List<SelectListItem>? LanguageList { get; set; }
        public List<SelectListItem>? ThemeList { get; set; }
    }
}