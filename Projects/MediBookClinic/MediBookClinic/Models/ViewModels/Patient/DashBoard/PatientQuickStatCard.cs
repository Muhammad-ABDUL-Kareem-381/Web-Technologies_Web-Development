namespace MediBookClinic.Models.ViewModels.Patient.DashBoard
{
    // Patient quick stat card
    public class PatientQuickStatCard
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? ActionText { get; set; }
        public string? ActionUrl { get; set; }
    }
}
