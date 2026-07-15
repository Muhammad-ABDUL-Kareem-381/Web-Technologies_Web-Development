namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // Admin quick stat card
    public class AdminQuickStatCard
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? Trend { get; set; }
        public string? ActionUrl { get; set; }
    }
}
