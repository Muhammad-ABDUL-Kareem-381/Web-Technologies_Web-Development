namespace MediBookClinic.Models.Entities
{
    public class AppointmentAnalytics
    {
        public int AnalyticsId { get; set; }
        public DateTime RecordDate { get; set; }
        public int TotalAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public int CancelledAppointments { get; set; }
        public int NoShowAppointments { get; set; }
        public decimal? AverageWaitingTime { get; set; } 
        public decimal? AverageRating { get; set; } 
        public decimal? Revenue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}