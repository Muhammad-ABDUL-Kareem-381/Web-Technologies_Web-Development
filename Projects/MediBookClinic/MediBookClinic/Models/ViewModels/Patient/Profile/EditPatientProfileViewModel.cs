using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Patient.Profile
{
    // ViewModel for editing patient profile (simplified)
    public class EditPatientProfileViewModel
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

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

        [Required]
        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Country")]
        public string? Country { get; set; }

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

        [Display(Name = "Preferred Language")]
        public string PreferredLanguage { get; set; } = "en-US";

        [Display(Name = "Preferred Theme")]
        public string PreferredTheme { get; set; } = "light";

        // For dropdowns
        public List<SelectListItem>? BloodGroupList { get; set; }
        public List<SelectListItem>? GenderList { get; set; }
        public List<SelectListItem>? LanguageList { get; set; }
        public List<SelectListItem>? ThemeList { get; set; }
    }
}
