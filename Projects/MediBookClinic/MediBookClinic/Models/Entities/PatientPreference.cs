namespace MediBookClinic.Models.Entities
{
    public class PatientPreference
    {
        public int PreferenceId { get; set; }
        public int PatientId { get; set; }
        public string? PreferredSpecialization { get; set; }
        public int? PreferredDayOfWeek { get; set; }
        public string? PreferredTimeOfDay { get; set; }
        public int? MaxDistance { get; set; }
        public int? MaxWaitingTime { get; set; }
        public string? PreferredLanguage { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
