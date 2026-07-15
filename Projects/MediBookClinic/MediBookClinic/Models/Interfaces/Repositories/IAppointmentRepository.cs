using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Repository
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(int appointmentId);
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Appointment>> GetByDateRangeAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<bool> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(int appointmentId);
        Task<IEnumerable<Appointment>> GetUpcomingAsync(int doctorId, int days);
        Task<IEnumerable<Appointment>> GetPastAsync(int patientId);
        Task<bool> IsSlotAvailableAsync(int doctorId, DateTime appointmentDate, TimeSpan appointmentTime);
        Task<int> GetTodayCountAsync(int doctorId);
        Task<IEnumerable<Appointment>> GetByStatusAsync(string status);
    }
}