using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Interfaces.Service;

namespace MediBookClinic.Models.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(IAnalyticsRepository analyticsRepository,IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository,ILogger<AnalyticsService> logger)
        {
            _analyticsRepository = analyticsRepository;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _logger = logger;
        }

        public async Task<AppointmentAnalytics> RecordAppointmentAsync(int doctorId, DateTime appointmentDate)
        {
            var analytics = new AppointmentAnalytics
            {
                RecordDate = appointmentDate.Date,
                TotalAppointments = 0,
                CompletedAppointments = 0,
                CancelledAppointments = 0,
                NoShowAppointments = 0,
                Revenue = 0,
                CreatedAt = DateTime.UtcNow
            };

            // Get all appointments for this date
            var appointments = await _appointmentRepository.GetByDateRangeAsync(
                doctorId,
                appointmentDate.Date,
                appointmentDate.Date);

            analytics.TotalAppointments = appointments.Count();
            analytics.CompletedAppointments = appointments.Count(a => a.Status == "Completed");
            analytics.CancelledAppointments = appointments.Count(a => a.Status == "Cancelled");
            analytics.NoShowAppointments = appointments.Count(a => a.Status == "NoShow");

            // Calculate revenue
            var doctor = await _doctorRepository.GetByIdAsync(doctorId);
            if (doctor != null)
            {
                analytics.Revenue = appointments
                    .Where(a => a.Status == "Completed")
                    .Count() * doctor.ConsultationFee;
            }

            return await _analyticsRepository.CreateAsync(analytics);
        }

        public async Task<IEnumerable<AppointmentAnalytics>> GetDoctorAnalyticsAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            return await _analyticsRepository.GetByDoctorIdAsync(doctorId, startDate, endDate);
        }

        public async Task<Dictionary<string, int>> GetAppointmentsByStatusAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            return await _analyticsRepository.GetAppointmentsByStatusAsync(doctorId, startDate, endDate);
        }

        public async Task<Dictionary<string, int>> GetMonthlyAppointmentTrendsAsync(int doctorId, int months = 6)
        {
            return await _analyticsRepository.GetMonthlyTrendsAsync(doctorId, months);
        }

        public async Task<decimal> GetDoctorRevenueAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            return await _analyticsRepository.GetTotalRevenueAsync(doctorId, startDate, endDate);
        }

        public async Task<int> GetTotalPatientsServedAsync(int doctorId)
        {
            return await _analyticsRepository.GetUniquePatientsCountAsync(doctorId);
        }

        public async Task<Dictionary<int, int>> GetPeakAppointmentHoursAsync(int doctorId)
        {
            return await _analyticsRepository.GetPeakHoursAsync(doctorId);
        }

        public async Task<Dictionary<string, object>> GetDashboardStatsAsync(int doctorId)
        {
            var today = DateTime.Today;
            var weekStart = today.AddDays(-(int)today.DayOfWeek);
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var stats = new Dictionary<string, object>
            {
                ["TodayAppointments"] = await _appointmentRepository.GetTodayCountAsync(doctorId),
                ["WeeklyRevenue"] = await _analyticsRepository.GetTotalRevenueAsync(doctorId, weekStart, today),
                ["MonthlyRevenue"] = await _analyticsRepository.GetTotalRevenueAsync(doctorId, monthStart, today),
                ["TotalPatients"] = await _analyticsRepository.GetUniquePatientsCountAsync(doctorId),
                ["AppointmentsByStatus"] = await _analyticsRepository.GetAppointmentsByStatusAsync(doctorId, monthStart, today),
                ["MonthlyTrends"] = await _analyticsRepository.GetMonthlyTrendsAsync(doctorId, 6),
                ["PeakHours"] = await _analyticsRepository.GetPeakHoursAsync(doctorId)
            };

            return stats;
        }
    }
}