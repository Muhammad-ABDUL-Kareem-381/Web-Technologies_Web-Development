namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    // Notification summary
    public class NotificationSummaryViewModel
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
    }
}
