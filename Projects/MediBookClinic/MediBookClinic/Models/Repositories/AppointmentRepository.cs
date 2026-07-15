using Dapper;
using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;

namespace MediBookClinic.Models.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DapperContext _context;

        public AppointmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            var sql = @"INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, AppointmentTime, 
                      EndTime, Status, ReasonForVisit, Symptoms, Notes, IsRecommended, RecommendationScore, CreatedAt, UpdatedAt)
                      VALUES (@PatientId, @DoctorId, @AppointmentDate, @AppointmentTime, @EndTime, @Status, @ReasonForVisit, @Symptoms, @Notes, @IsRecommended, 
                      @RecommendationScore, @CreatedAt, @UpdatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, appointment);
            appointment.AppointmentId = id;
            return appointment;
        }

        public async Task<Appointment?> GetByIdAsync(int appointmentId)
        {
            var sql = "SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Appointment>(sql, new { AppointmentId = appointmentId });
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            var sql = "SELECT * FROM Appointments WHERE DoctorId = @DoctorId ORDER BY AppointmentDate DESC, AppointmentTime DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Appointment>(sql, new { DoctorId = doctorId });
        }

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId)
        {
            var sql = "SELECT * FROM Appointments WHERE PatientId = @PatientId ORDER BY AppointmentDate DESC, AppointmentTime DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Appointment>(sql, new { PatientId = patientId });
        }

        public async Task<IEnumerable<Appointment>> GetByDateRangeAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            var sql = @"SELECT * FROM Appointments WHERE DoctorId = @DoctorId AND AppointmentDate BETWEEN @StartDate AND @EndDate
                       ORDER BY AppointmentDate, AppointmentTime";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Appointment>(sql, new { DoctorId = doctorId, StartDate = startDate, EndDate = endDate });
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            var sql = @"UPDATE Appointments SET AppointmentDate = @AppointmentDate, AppointmentTime = @AppointmentTime, EndTime = @EndTime,
                      Status = @Status, ReasonForVisit = @ReasonForVisit, Symptoms = @Symptoms, Diagnosis = @Diagnosis, Prescription = @Prescription,
                      Notes = @Notes, CancellationReason = @CancellationReason, IsRecommended = @IsRecommended, RecommendationScore = @RecommendationScore,
                      UpdatedAt = @UpdatedAt, CompletedAt = @CompletedAt, CancelledAt = @CancelledAt WHERE AppointmentId = @AppointmentId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, appointment);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int appointmentId)
        {
            var sql = "DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { AppointmentId = appointmentId });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingAsync(int doctorId, int days)
        {
            var sql = @"SELECT * FROM Appointments WHERE DoctorId = @DoctorId AND AppointmentDate >= CAST(GETDATE() AS DATE)
                      AND AppointmentDate <= DATEADD(DAY, @Days, CAST(GETDATE() AS DATE)) AND Status IN ('Pending', 'Confirmed')
                      ORDER BY AppointmentDate, AppointmentTime";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Appointment>(sql, new { DoctorId = doctorId, Days = days });
        }

        public async Task<IEnumerable<Appointment>> GetPastAsync(int patientId)
        {
            var sql = @"SELECT * FROM Appointments WHERE PatientId = @PatientId AND AppointmentDate < CAST(GETDATE() AS DATE)
                      ORDER BY AppointmentDate DESC, AppointmentTime DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Appointment>(sql, new { PatientId = patientId });
        }

        public async Task<bool> IsSlotAvailableAsync(int doctorId, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            var sql = @"SELECT COUNT(1) FROM Appointments WHERE DoctorId = @DoctorId AND AppointmentDate = @AppointmentDate 
                      AND AppointmentTime = @AppointmentTime AND Status NOT IN ('Cancelled', 'NoShow')";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(sql, new
            {
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointmentTime
            });
            return count == 0;
        }

        public async Task<int> GetTodayCountAsync(int doctorId)
        {
            var sql = @"SELECT COUNT(*) FROM Appointments WHERE DoctorId = @DoctorId AND AppointmentDate = CAST(GETDATE() AS DATE)
                      AND Status IN ('Pending', 'Confirmed', 'Completed')";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, new { DoctorId = doctorId });
        }

        public async Task<IEnumerable<Appointment>> GetByStatusAsync(string status)
        {
            var sql = "SELECT * FROM Appointments WHERE Status = @Status ORDER BY AppointmentDate DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Appointment>(sql, new { Status = status });
        }
    }
}
