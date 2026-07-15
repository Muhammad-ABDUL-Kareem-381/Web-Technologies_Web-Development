namespace MediBookClinic.Models.ViewModels.Patient.DashBoard
{
    // Pending review (completed appointment without review)
    public class PendingReviewViewModel
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }

        public string FormattedDate => AppointmentDate.ToString("MMM dd, yyyy");
        public string TimeAgo
        {
            get
            {
                var timeSpan = DateTime.Now - AppointmentDate;
                if (timeSpan.TotalDays < 1)
                {
                    return "Today";
                }
                if (timeSpan.TotalDays < 7)
                {
                    return $"{(int)timeSpan.TotalDays} days ago";
                }
                return FormattedDate;
            }
        }
    }
}
