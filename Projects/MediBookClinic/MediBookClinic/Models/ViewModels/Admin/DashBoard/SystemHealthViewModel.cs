namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // System health metrics
    public class SystemHealthViewModel
    {
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public double DiskUsage { get; set; }
        public int ActiveConnections { get; set; }
        public DateTime LastBackup { get; set; }
        public bool DatabaseHealthy { get; set; } = true;
        public bool EmailServiceHealthy { get; set; } = true;
        public bool SmsServiceHealthy { get; set; } = true;
        public string OverallHealth => DatabaseHealthy && EmailServiceHealthy ? "Healthy" : "Issues Detected";
        public string HealthBadgeClass => OverallHealth == "Healthy" ? "badge bg-success" : "badge bg-warning";
    }
}
