namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    public class MonthlyTrendData
    {
        public string Month { get; set; } = string.Empty;
        public int TotalAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public int CancelledAppointments { get; set; }
    }
}
