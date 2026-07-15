namespace MediBookClinic.Models.ViewModels.Patient.DashBoard
{
    // Patient upcoming appointment
    public class PatientUpcomingAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? DoctorProfileImage { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ReasonForVisit { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public string DoctorImageOrDefault => string.IsNullOrEmpty(DoctorProfileImage) ? "/images/default-doctor-avatar.png" : DoctorProfileImage;
        public string FormattedDate => AppointmentDate.ToString("MMM dd, yyyy");
        public string FormattedTime => AppointmentTime.ToString(@"hh\:mm tt");
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
                if (timeSpan.TotalDays < 7)
                {
                    return $"In {(int)timeSpan.TotalDays} days";
                }
                return FormattedDate;
            }
        }
        public bool IsToday => AppointmentDate.Date == DateTime.Today;
        public bool IsTomorrow => AppointmentDate.Date == DateTime.Today.AddDays(1);
        public bool IsThisWeek => AppointmentDate <= DateTime.Today.AddDays(7);
        public bool CanCancel => (AppointmentDate.Add(AppointmentTime) - DateTime.Now).TotalHours > 6;
    }
}
