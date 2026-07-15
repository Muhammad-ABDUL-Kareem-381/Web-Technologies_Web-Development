namespace MediBookClinic.Models.ViewModels.Patient.DashBoard
{
    // Patient notification
    public class PatientNotificationViewModel
    {
        public int NotificationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string TimeAgo
        {
            get
            {
                var timeSpan = DateTime.UtcNow - CreatedAt;
                if (timeSpan.TotalMinutes < 1)
                {
                    return "Just now";
                }
                if (timeSpan.TotalMinutes < 60)
                {
                    return $"{(int)timeSpan.TotalMinutes}m ago";
                }
                if (timeSpan.TotalHours < 24)
                {
                    return $"{(int)timeSpan.TotalHours}h ago";
                }
                return $"{(int)timeSpan.TotalDays}d ago";
            }
        }
        public string IconClass => Type.ToLower() switch
        {
            "appointment" => "fas fa-calendar-check text-primary",
            "reminder" => "fas fa-bell text-warning",
            "success" => "fas fa-check-circle text-success",
            "info" => "fas fa-info-circle text-info",
            _ => "fas fa-bell text-secondary"
        };
    }
}
