namespace MediBookClinic.Models.ViewModels.Patient.DashBoard
{
    // Health summary widget
    public class HealthSummaryViewModel
    {
        public string? BloodGroup { get; set; }
        public string? Allergies { get; set; }
        public string? LastVisitDate { get; set; }
        public string? LastDiagnosis { get; set; }
        public int TotalVisits { get; set; }
        public bool HasAllergies => !string.IsNullOrEmpty(Allergies);
        public bool HasRecentVisit => !string.IsNullOrEmpty(LastVisitDate);
    }
}
