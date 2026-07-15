namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // Recent user registration
    public class RecentUserViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string FormattedDate => CreatedAt.ToString("MMM dd, yyyy");
        public string TimeAgo
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
                return FormattedDate;
            }
        }
        public string RoleBadgeClass => Role switch
        {
            "Doctor" => "badge bg-primary",
            "Patient" => "badge bg-success",
            "Admin" => "badge bg-warning",
            "MasterAdmin" => "badge bg-danger",
            _ => "badge bg-secondary"
        };
    }
}
