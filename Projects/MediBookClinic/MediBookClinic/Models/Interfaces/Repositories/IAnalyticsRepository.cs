using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Repository
{
    public interface IAnalyticsRepository
    {
        Task<AppointmentAnalytics> CreateAsync(AppointmentAnalytics analytics);
        Task<IEnumerable<AppointmentAnalytics>> GetByDoctorIdAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<Dictionary<string, int>> GetAppointmentsByStatusAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<Dictionary<string, int>> GetMonthlyTrendsAsync(int doctorId, int months);
        Task<decimal> GetTotalRevenueAsync(int doctorId, DateTime startDate, DateTime endDate);
        Task<int> GetUniquePatientsCountAsync(int doctorId);
        Task<Dictionary<int, int>> GetPeakHoursAsync(int doctorId);
    }
}