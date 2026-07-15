namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    // Peak hours data for bar chart
    public class PeakHourData
    {
        public int Hour { get; set; }
        public int AppointmentCount { get; set; }
        public string TimeRange => $"{Hour:00}:00 - {(Hour + 1):00}:00";
    }
}
