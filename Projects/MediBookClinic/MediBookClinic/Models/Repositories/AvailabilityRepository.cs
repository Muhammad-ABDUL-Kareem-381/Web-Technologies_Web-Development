using Dapper;
using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;

namespace MediBookClinic.Models.Repositories
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly DapperContext _context;

        public AvailabilityRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<DoctorAvailability> CreateAsync(DoctorAvailability availability)
        {
            var sql = @"INSERT INTO DoctorAvailability (DoctorId, DayOfWeek, StartTime, EndTime, SlotDuration, IsActive, CreatedAt)
                      VALUES (@DoctorId, @DayOfWeek, @StartTime, @EndTime, @SlotDuration, @IsActive, @CreatedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, availability);
            availability.AvailabilityId = id;
            return availability;
        }

        public async Task<IEnumerable<DoctorAvailability>> GetByDoctorIdAsync(int doctorId)
        {
            var sql = "SELECT * FROM DoctorAvailability WHERE DoctorId = @DoctorId ORDER BY DayOfWeek";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DoctorAvailability>(sql, new { DoctorId = doctorId });
        }

        public async Task<DoctorAvailability?> GetByDayAsync(int doctorId, string dayOfWeek)
        {
            int dayOfWeekInt = dayOfWeek switch
            {
                "Sunday" => 0,
                "Monday" => 1,
                "Tuesday" => 2,
                "Wednesday" => 3,
                "Thursday" => 4,
                "Friday" => 5,
                "Saturday" => 6,
                _ => -1
            };
            var sql = "SELECT * FROM DoctorAvailability WHERE DoctorId = @DoctorId AND DayOfWeek = @DayOfWeek";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<DoctorAvailability>(sql, new { DoctorId = doctorId, DayOfWeek = dayOfWeekInt });
        }

        public async Task<bool> UpdateAsync(DoctorAvailability availability)
        {
            var sql = @"UPDATE DoctorAvailability SET StartTime = @StartTime, EndTime = @EndTime, SlotDuration = @SlotDuration,
                      IsActive = @IsActive WHERE AvailabilityId = @AvailabilityId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, availability);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int availabilityId)
        {
            var sql = "DELETE FROM DoctorAvailability WHERE AvailabilityId = @AvailabilityId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { AvailabilityId = availabilityId });
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAllByDoctorIdAsync(int doctorId)
        {
            var sql = "DELETE FROM DoctorAvailability WHERE DoctorId = @DoctorId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { DoctorId = doctorId });
            return true;
        }
    }
}
