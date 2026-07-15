using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Notification
{
    // ViewModel for deleting notification
    public class DeleteNotificationViewModel
    {
        [Required]
        public int NotificationId { get; set; }
    }
}
