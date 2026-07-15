namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    // Recent appointment summary
    public class RecentAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string FormattedDate => AppointmentDate.ToString("MMM dd, yyyy");
        public string TimeAgo
        {
            get
            {
                var timeSpan = DateTime.Now - AppointmentDate;
                if (timeSpan.TotalHours < 24) return "Today";
                if (timeSpan.TotalDays < 7) return $"{(int)timeSpan.TotalDays} days ago";
                return FormattedDate;
            }
        }
    }
}
