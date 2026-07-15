namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // Monthly growth data for charts
    public class MonthlyGrowthData
    {
        public string Month { get; set; } = string.Empty;
        public int Doctors { get; set; }
        public int Patients { get; set; }
        public int TotalUsers { get; set; }
    }
}
