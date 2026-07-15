namespace MediBookClinic.Models.ViewModels.Notification
{
    // ViewModel for notification list with filters
    public class NotificationListViewModel
    {
        // List of notifications
        public List<NotificationViewModel> Notifications { get; set; } = new List<NotificationViewModel>();

        // Total unread count
        public int UnreadCount { get; set; }

        // Filter: Show only unread
        public bool ShowOnlyUnread { get; set; }

        // Filter: Notification type
        public string? FilterType { get; set; }

        // Filter: Date range start
        public DateTime? FromDate { get; set; }

        // Filter: Date range end
        public DateTime? ToDate { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
