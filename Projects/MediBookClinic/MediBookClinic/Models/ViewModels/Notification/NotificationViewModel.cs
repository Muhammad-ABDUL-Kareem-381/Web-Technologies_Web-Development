using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Notification
{
    // ViewModel for displaying a single notification
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        // Notification type (Info, Success, Warning, Error, Appointment, Review, etc.)
        [Required]
        public string Type { get; set; } = "Info";

        // Delivery channel (Email, SMS, InApp)
        [Required]
        public string Channel { get; set; } = "InApp";

        // Whether notification has been read
        public bool IsRead { get; set; }

        // Whether notification was sent successfully
        public bool IsSent { get; set; }

        // When notification was sent
        public DateTime? SentAt { get; set; }

        // Related entity type (Appointment, Review, User, etc.)
        public string? RelatedEntityType { get; set; }

        // Related entity ID
        public int? RelatedEntityId { get; set; }

        // When notification was created
        public DateTime CreatedAt { get; set; }

        // Time ago string (e.g., "5 minutes ago", "2 hours ago")
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
                    return $"{(int)timeSpan.TotalMinutes} minute{((int)timeSpan.TotalMinutes == 1 ? "" : "s")} ago";
                }
                if (timeSpan.TotalHours < 24)
                {
                    return $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours == 1 ? "" : "s")} ago";
                }
                if (timeSpan.TotalDays < 7)
                {
                    return $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays == 1 ? "" : "s")} ago";
                }
                if (timeSpan.TotalDays < 30)
                {
                    return $"{(int)(timeSpan.TotalDays / 7)} week{((int)(timeSpan.TotalDays / 7) == 1 ? "" : "s")} ago";
                }
                if (timeSpan.TotalDays < 365)
                {
                    return $"{(int)(timeSpan.TotalDays / 30)} month{((int)(timeSpan.TotalDays / 30) == 1 ? "" : "s")} ago";
                }
                return $"{(int)(timeSpan.TotalDays / 365)} year{((int)(timeSpan.TotalDays / 365) == 1 ? "" : "s")} ago";
            }
        }

        // Bootstrap CSS class for notification type
        public string NotificationClass
        {
            get
            {
                return Type.ToLower() switch
                {
                    "success" => "alert-success",
                    "warning" => "alert-warning",
                    "error" => "alert-danger",
                    "info" => "alert-info",
                    "appointment" => "alert-primary",
                    "review" => "alert-info",
                    _ => "alert-secondary"
                };
            }
        }

        // Icon class for notification type (Font Awesome)
        public string IconClass
        {
            get
            {
                return Type.ToLower() switch
                {
                    "success" => "fas fa-check-circle",
                    "warning" => "fas fa-exclamation-triangle",
                    "error" => "fas fa-times-circle",
                    "info" => "fas fa-info-circle",
                    "appointment" => "fas fa-calendar-check",
                    "review" => "fas fa-star",
                    "message" => "fas fa-envelope",
                    "reminder" => "fas fa-bell",
                    _ => "fas fa-bell"
                };
            }
        }

        // Link to related entity (if applicable)
        public string? ActionUrl
        {
            get
            {
                if (string.IsNullOrEmpty(RelatedEntityType) || !RelatedEntityId.HasValue)
                {
                    return null;
                }
                return RelatedEntityType.ToLower() switch
                {
                    "appointment" => $"/Appointment/Details/{RelatedEntityId}",
                    "review" => $"/Review/Details/{RelatedEntityId}",
                    "doctor" => $"/Doctor/Profile/{RelatedEntityId}",
                    "patient" => $"/Patient/Profile/{RelatedEntityId}",
                    _ => null
                };
            }
        }

        // Action button text
        public string? ActionText
        {
            get
            {
                if (string.IsNullOrEmpty(RelatedEntityType))
                {
                    return null;
                }
                return RelatedEntityType.ToLower() switch
                {
                    "appointment" => "View Appointment",
                    "review" => "View Review",
                    "doctor" => "View Profile",
                    "patient" => "View Profile",
                    _ => "View Details"
                };
            }
        }
    }
}