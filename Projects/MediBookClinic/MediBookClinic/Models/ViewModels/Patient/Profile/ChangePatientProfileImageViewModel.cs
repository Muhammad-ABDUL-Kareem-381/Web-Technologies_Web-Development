using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Patient.Profile
{
    // ViewModel for changing profile image
    public class ChangePatientProfileImageViewModel
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; } = null!;

        public string? CurrentImageUrl { get; set; }
    }
}
