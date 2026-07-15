using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Service
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDateRangeAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<bool> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentStatusAsync(int appointmentId, string status);
        Task<bool> CancelAppointmentAsync(int appointmentId, string cancelledBy, string? cancellationReason = null);
        Task<bool> DeleteAppointmentAsync(int appointmentId);
        Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(int doctorId, int days = 7);
        Task<IEnumerable<Appointment>> GetPastAppointmentsAsync(int patientId);
        Task<bool> IsSlotAvailableAsync(int doctorId, DateTime appointmentDate, TimeSpan appointmentTime);
        Task<IEnumerable<TimeSpan>> GetAvailableSlotsAsync(int doctorId, DateTime date);
        Task<int> GetTodayAppointmentCountAsync(int doctorId);
    }
}