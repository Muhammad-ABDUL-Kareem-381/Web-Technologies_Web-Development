using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Doctor
{
    // ViewModel for changing profile image
    public class ChangeProfileImageViewModel
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; } = null!;

        public string? CurrentImageUrl { get; set; }
    }
}
