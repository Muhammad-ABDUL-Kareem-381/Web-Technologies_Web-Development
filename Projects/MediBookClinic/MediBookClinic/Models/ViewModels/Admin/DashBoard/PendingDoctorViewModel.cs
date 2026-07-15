namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // Pending doctor awaiting approval
    public class PendingDoctorViewModel
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public string? Qualification { get; set; }
        public decimal ConsultationFee { get; set; }
        public DateTime CreatedAt { get; set; }

        public string RegisteredAgo
        {
            get
            {
                var timeSpan = DateTime.UtcNow - CreatedAt;
                if (timeSpan.TotalHours < 24)
                {
                    return "Today";
                }
                if (timeSpan.TotalDays < 7)
                {
                    return $"{(int)timeSpan.TotalDays} days ago";
                }
                return CreatedAt.ToString("MMM dd, yyyy");
            }
        }
        public string FormattedDate => CreatedAt.ToString("MMM dd, yyyy");
    }
}
