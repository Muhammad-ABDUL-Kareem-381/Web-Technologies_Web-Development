using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Notification
{
    // ViewModel for marking notification as read
    public class MarkNotificationReadViewModel
    {
        [Required]
        public int NotificationId { get; set; }

        public bool MarkAsRead { get; set; } = true;
    }
}
