namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    // Upcoming appointment summary
    public class UpcomingAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string? PatientProfileImage { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string ReasonForVisit { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string FormattedDate => AppointmentDate.ToString("MMM dd, yyyy");
        public string FormattedTime => AppointmentTime.ToString(@"hh\:mm");
        public string FormattedDateTime => $"{FormattedDate} at {FormattedTime}";
        public string TimeUntil
        {
            get
            {
                var appointmentDateTime = AppointmentDate.Add(AppointmentTime);
                var timeSpan = appointmentDateTime - DateTime.Now;

                if (timeSpan.TotalMinutes < 60)
                {
                    return $"In {(int)timeSpan.TotalMinutes} minutes";
                }
                if (timeSpan.TotalHours < 24)
                {
                    return $"In {(int)timeSpan.TotalHours} hours";
                }
                return $"In {(int)timeSpan.TotalDays} days";
            }
        }
        public bool IsToday => AppointmentDate.Date == DateTime.Today;
        public bool IsUrgent => (AppointmentDate.Add(AppointmentTime) - DateTime.Now).TotalHours < 1;
    }
}
