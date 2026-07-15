using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Service
{
    public interface IAnalyticsService
    {
        Task<AppointmentAnalytics> RecordAppointmentAsync(int doctorId, DateTime appointmentDate);
        Task<IEnumerable<AppointmentAnalytics>> GetDoctorAnalyticsAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<Dictionary<string, int>> GetAppointmentsByStatusAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<Dictionary<string, int>> GetMonthlyAppointmentTrendsAsync(int doctorId, int months = 6);
        Task<decimal> GetDoctorRevenueAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<int> GetTotalPatientsServedAsync(int doctorId);
        Task<Dictionary<int, int>> GetPeakAppointmentHoursAsync(int doctorId);
        Task<Dictionary<string, object>> GetDashboardStatsAsync(int doctorId);
    }
}