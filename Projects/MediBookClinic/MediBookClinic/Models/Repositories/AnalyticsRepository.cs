using Dapper;
using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;

namespace MediBookClinic.Models.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly DapperContext _context;

        public AnalyticsRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<AppointmentAnalytics> CreateAsync(AppointmentAnalytics analytics)
        {
            var sql = @"INSERT INTO AppointmentAnalytics (RecordDate, TotalAppointments, CompletedAppointments, 
                      CancelledAppointments, NoShowAppointments, AverageWaitingTime, AverageRating, Revenue, CreatedAt)
                      VALUES (@RecordDate, @TotalAppointments, @CompletedAppointments, @CancelledAppointments, @NoShowAppointments, @AverageWaitingTime, @AverageRating, 
                      @Revenue, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, analytics);
            analytics.AnalyticsId = id;
            return analytics;
        }

        public async Task<IEnumerable<AppointmentAnalytics>> GetByDoctorIdAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            var sql = @"SELECT CAST(a.AppointmentDate AS DATE) as RecordDate, COUNT(*) as TotalAppointments,
                      SUM(CASE WHEN a.Status = 'Completed' THEN 1 ELSE 0 END) as CompletedAppointments,
                      SUM(CASE WHEN a.Status = 'Cancelled' THEN 1 ELSE 0 END) as CancelledAppointments,
                      SUM(CASE WHEN a.Status = 'NoShow' THEN 1 ELSE 0 END) as NoShowAppointments,
                      AVG(CASE WHEN a.Status = 'Completed' THEN DATEDIFF(DAY, a.CreatedAt, a.CompletedAt) ELSE NULL END) as AverageWaitingTime,
                      (SELECT AVG(CAST(Rating AS DECIMAL(3,2))) FROM AppointmentReviews WHERE DoctorId = @DoctorId) as AverageRating,
                      SUM(CASE WHEN a.Status = 'Completed' THEN d.ConsultationFee ELSE 0 END) as Revenue FROM Appointments a
                      INNER JOIN Doctors d ON a.DoctorId = d.DoctorId WHERE a.DoctorId = @DoctorId AND a.AppointmentDate BETWEEN @StartDate AND @EndDate
                      GROUP BY CAST(a.AppointmentDate AS DATE) ORDER BY RecordDate";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<AppointmentAnalytics>(sql, new { DoctorId = doctorId, StartDate = startDate, EndDate = endDate });
        }

        public async Task<Dictionary<string, int>> GetAppointmentsByStatusAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT Status, COUNT(*) as Count FROM Appointments WHERE DoctorId = @DoctorId AND AppointmentDate BETWEEN @StartDate AND @EndDate GROUP BY Status";
            using var connection = _context.CreateConnection();
            var results = await connection.QueryAsync<(string Status, int Count)>(sql, new { DoctorId = doctorId, StartDate = startDate, EndDate = endDate });
            return results.ToDictionary(r => r.Status, r => r.Count);
        }

        public async Task<Dictionary<string, int>> GetMonthlyTrendsAsync(int doctorId, int months)
        {
            var sql = @"SELECT FORMAT(AppointmentDate, 'yyyy-MM') as Month, COUNT(*) as Count FROM Appointments WHERE DoctorId = @DoctorId
                      AND AppointmentDate >= DATEADD(MONTH, -@Months, GETDATE()) GROUP BY FORMAT(AppointmentDate, 'yyyy-MM') ORDER BY Month";
            using var connection = _context.CreateConnection();
            var results = await connection.QueryAsync<(string Month, int Count)>(sql, new { DoctorId = doctorId, Months = months });
            return results.ToDictionary(r => r.Month, r => r.Count);
        }

        public async Task<decimal> GetTotalRevenueAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            var sql = @"SELECT ISNULL(SUM(d.ConsultationFee), 0) FROM Appointments a INNER JOIN Doctors d ON a.DoctorId = d.DoctorId
                      WHERE a.DoctorId = @DoctorId AND a.AppointmentDate BETWEEN @StartDate AND @EndDate AND a.Status = 'Completed'";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<decimal>(sql, new { DoctorId = doctorId, StartDate = startDate, EndDate = endDate });
        }

        public async Task<int> GetUniquePatientsCountAsync(int doctorId)
        {
            var sql = "SELECT COUNT(DISTINCT PatientId) FROM Appointments WHERE DoctorId = @DoctorId";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, new { DoctorId = doctorId });
        }

        public async Task<Dictionary<int, int>> GetPeakHoursAsync(int doctorId)
        {
            var sql = @"SELECT DATEPART(HOUR, AppointmentTime) as Hour, COUNT(*) as Count FROM Appointments
                      WHERE DoctorId = @DoctorId GROUP BY DATEPART(HOUR, AppointmentTime) ORDER BY Hour";
            using var connection = _context.CreateConnection();
            var results = await connection.QueryAsync<(int Hour, int Count)>(sql, new { DoctorId = doctorId });
            return results.ToDictionary(r => r.Hour, r => r.Count);
        }
    }
}
