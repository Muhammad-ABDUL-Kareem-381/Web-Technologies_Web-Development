using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Doctor.Profile
{
    // ViewModel for toggling availability
    public class ToggleAvailabilityViewModel
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Display(Name = "Available for Booking")]
        public bool IsAvailableForBooking { get; set; }
    }
}
