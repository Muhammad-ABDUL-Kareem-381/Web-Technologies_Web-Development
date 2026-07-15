using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Doctor.Profile
{
    // ViewModel for editing doctor profile (simplified)
    public class EditDoctorProfileViewModel
    {
        [Required]
        public int DoctorId { get; set; }

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
        [StringLength(100)]
        [Display(Name = "Specialization")]
        public string Specialization { get; set; } = string.Empty;

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

        [Display(Name = "Preferred Language")]
        public string PreferredLanguage { get; set; } = "en-US";

        [Display(Name = "Preferred Theme")]
        public string PreferredTheme { get; set; } = "light";

        // For dropdowns
        public List<SelectListItem>? SpecializationList { get; set; }
        public List<SelectListItem>? GenderList { get; set; }
        public List<SelectListItem>? LanguageList { get; set; }
        public List<SelectListItem>? ThemeList { get; set; }
    }
}
