namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    // Appointment status distribution for pie chart
    public class AppointmentStatusChartData
    {
        public int Completed { get; set; }
        public int Pending { get; set; }
        public int Confirmed { get; set; }
        public int Cancelled { get; set; }
        public int NoShow { get; set; }

        public int Total => Completed + Pending + Confirmed + Cancelled + NoShow;

        public List<ChartDataPoint> ToChartData() => new List<ChartDataPoint>
        {
            new ChartDataPoint { Label = "Completed", Value = Completed, Color = "#28a745" },
            new ChartDataPoint { Label = "Pending", Value = Pending, Color = "#ffc107" },
            new ChartDataPoint { Label = "Confirmed", Value = Confirmed, Color = "#17a2b8" },
            new ChartDataPoint { Label = "Cancelled", Value = Cancelled, Color = "#dc3545" },
            new ChartDataPoint { Label = "No Show", Value = NoShow, Color = "#6c757d" }
        };
    }
}
