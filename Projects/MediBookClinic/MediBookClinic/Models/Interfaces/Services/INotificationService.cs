using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Service
{
    public interface INotificationService
    {
        // Return Notification Methods
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task<bool> MarkAsReadAsync(int notificationId);
        Task<bool> MarkAllAsReadAsync(string userId);
        Task<bool> DeleteNotificationAsync(int notificationId);
        // Non-Return Notification Methods
        Task SendAppointmentConfirmationAsync(int appointmentId);
        Task SendAppointmentReminderAsync(int appointmentId);
        Task SendAppointmentCancellationAsync(int appointmentId);
        Task SendAppointmentRescheduledAsync(int appointmentId);
    }
}