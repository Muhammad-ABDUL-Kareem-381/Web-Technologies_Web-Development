using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Interfaces.Service;

namespace MediBookClinic.Models.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationRepository notificationRepository,IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository,IPatientRepository patientRepository,ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _logger = logger;
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            notification.IsRead = false;
            notification.IsSent = false;
            notification.CreatedAt = DateTime.UtcNow;

            if (string.IsNullOrEmpty(notification.Channel))
            {
                notification.Channel = "InApp";
            }

            return await _notificationRepository.CreateAsync(notification);
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId)
        {
            return await _notificationRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(string userId)
        {
            return await _notificationRepository.GetUnreadAsync(userId);
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _notificationRepository.GetUnreadCountAsync(userId);
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            return await _notificationRepository.MarkAsReadAsync(notificationId);
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            return await _notificationRepository.MarkAllAsReadAsync(userId);
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            return await _notificationRepository.DeleteAsync(notificationId);
        }

        public async Task SendAppointmentConfirmationAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return;
            }
            var doctor = await _doctorRepository.GetByIdAsync(appointment.DoctorId);
            var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);

            if (doctor != null && patient != null)
            {
                // Send to patient
                await CreateNotificationAsync(new Notification
                {
                    UserId = patient.UserId,
                    Type = "AppointmentConfirmation",
                    Title = "Appointment Confirmed",
                    Message = $"Your appointment on {appointment.AppointmentDate:MMM dd, yyyy} at {appointment.AppointmentTime} has been confirmed.",
                    Channel = "Email",
                    RelatedEntityType = "Appointment",
                    RelatedEntityId = appointmentId
                });

                // Send to doctor
                await CreateNotificationAsync(new Notification
                {
                    UserId = doctor.UserId,
                    Type = "NewAppointment",
                    Title = "New Appointment",
                    Message = $"New appointment scheduled on {appointment.AppointmentDate:MMM dd, yyyy} at {appointment.AppointmentTime}.",
                    Channel = "InApp",
                    RelatedEntityType = "Appointment",
                    RelatedEntityId = appointmentId
                });
            }
        }

        public async Task SendAppointmentReminderAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return;
            }
            var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);
            if (patient != null)
            {
                await CreateNotificationAsync(new Notification
                {
                    UserId = patient.UserId,
                    Type = "AppointmentReminder",
                    Title = "Appointment Reminder",
                    Message = $"Reminder: You have an appointment tomorrow at {appointment.AppointmentTime}.",
                    Channel = "SMS",
                    RelatedEntityType = "Appointment",
                    RelatedEntityId = appointmentId
                });
            }
        }

        public async Task SendAppointmentCancellationAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return;
            }
            var doctor = await _doctorRepository.GetByIdAsync(appointment.DoctorId);
            var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);

            if (doctor != null && patient != null)
            {
                // Notify the affected party
                var targetUserId = patient.UserId;
                var message = $"Your appointment on {appointment.AppointmentDate:MMM dd, yyyy} has been cancelled.";

                await CreateNotificationAsync(new Notification
                {
                    UserId = targetUserId,
                    Type = "AppointmentCancellation",
                    Title = "Appointment Cancelled",
                    Message = message,
                    Channel = "Email",
                    RelatedEntityType = "Appointment",
                    RelatedEntityId = appointmentId
                });
            }
        }

        public async Task SendAppointmentRescheduledAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return;
            }
            var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);
            if (patient != null)
            {
                await CreateNotificationAsync(new Notification
                {
                    UserId = patient.UserId,
                    Type = "AppointmentRescheduled",
                    Title = "Appointment Rescheduled",
                    Message = $"Your appointment has been rescheduled to {appointment.AppointmentDate:MMM dd, yyyy} at {appointment.AppointmentTime}.",
                    Channel = "Email",
                    RelatedEntityType = "Appointment",
                    RelatedEntityId = appointmentId
                });
            }
        }
    }
}