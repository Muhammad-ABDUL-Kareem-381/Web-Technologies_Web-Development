namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    // Quick stat card data
    public class QuickStatCard
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? Change { get; set; }
    }
}
