namespace MediBookClinic.Models.ViewModels.Patient.DashBoard
{
    // Patient past appointment
    public class PatientPastAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public bool HasReview { get; set; }

        public string FormattedDate => AppointmentDate.ToString("MMM dd, yyyy");
        public string TimeAgo
        {
            get
            {
                var timeSpan = DateTime.Now - AppointmentDate;
                if (timeSpan.TotalHours < 24)
                {
                    return "Today";
                }
                if (timeSpan.TotalDays < 7)
                {
                    return $"{(int)timeSpan.TotalDays} days ago";
                }
                if (timeSpan.TotalDays < 30)
                {
                    return $"{(int)(timeSpan.TotalDays / 7)} weeks ago";
                }
                return FormattedDate;
            }
        }
        public bool CanReview => Status == "Completed" && !HasReview;
    }
}
