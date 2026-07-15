namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // Recent appointment
    public class RecentAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string FormattedDate => AppointmentDate.ToString("MMM dd, yyyy");
        public string FormattedTime => AppointmentTime.ToString(@"hh\:mm tt");
        public string StatusBadgeClass => Status switch
        {
            "Completed" => "badge bg-success",
            "Confirmed" => "badge bg-primary",
            "Pending" => "badge bg-warning",
            "Cancelled" => "badge bg-danger",
            "InProgress" => "badge bg-info",
            "NoShow" => "badge bg-secondary",
            _ => "badge bg-light"
        };
    }
}
