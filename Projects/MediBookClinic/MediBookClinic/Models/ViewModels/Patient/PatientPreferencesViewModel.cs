using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Patient
{
    // ViewModel for patient preferences
    public class PatientPreferencesViewModel
    {
        public int PreferenceId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [StringLength(100)]
        [Display(Name = "Preferred Specialization")]
        public string? PreferredSpecialization { get; set; }

        [Range(0, 6)]
        [Display(Name = "Preferred Day of Week")]
        public int? PreferredDayOfWeek { get; set; }

        [StringLength(20)]
        [Display(Name = "Preferred Time of Day")]
        public string? PreferredTimeOfDay { get; set; }

        [Range(0, 1000)]
        [Display(Name = "Maximum Distance (km)")]
        public int? MaxDistance { get; set; }

        [Range(0, 300)]
        [Display(Name = "Maximum Waiting Time (minutes)")]
        public int? MaxWaitingTime { get; set; }

        [StringLength(10)]
        [Display(Name = "Preferred Language")]
        public string? PreferredLanguage { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime? UpdatedAt { get; set; }

        // For dropdowns
        public List<SelectListItem>? SpecializationList { get; set; }
        public List<SelectListItem>? DayOfWeekList { get; set; }
        public List<SelectListItem>? TimeOfDayList { get; set; }
        public List<SelectListItem>? LanguageList { get; set; }
    }
}
