using Dapper;
using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;

namespace MediBookClinic.Models.Repositories
{
    public class SpecialDateRepository : ISpecialDateRepository
    {
        private readonly DapperContext _context;

        public SpecialDateRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<DoctorSpecialDate> CreateAsync(DoctorSpecialDate specialDate)
        {
            var sql = @"INSERT INTO DoctorSpecialDates (DoctorId, SpecialDate, IsAvailable, StartTime, EndTime, Reason, CreatedAt)
                VALUES (@DoctorId, @SpecialDate, @IsAvailable, @StartTime, @EndTime, @Reason, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, specialDate);
            specialDate.SpecialDateId = id;
            return specialDate;
        }

        public async Task<IEnumerable<DoctorSpecialDate>> GetByDoctorIdAsync(int doctorId)
        {
            var sql = "SELECT * FROM DoctorSpecialDates WHERE DoctorId = @DoctorId ORDER BY SpecialDate";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DoctorSpecialDate>(sql, new { DoctorId = doctorId });
        }

        public async Task<DoctorSpecialDate?> GetByDateAsync(int doctorId, DateTime date)
        {
            var sql = "SELECT * FROM DoctorSpecialDates WHERE DoctorId = @DoctorId AND SpecialDate = @Date";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<DoctorSpecialDate>(sql, new { DoctorId = doctorId, Date = date.Date });
        }

        public async Task<bool> UpdateAsync(DoctorSpecialDate specialDate)
        {
            var sql = @"UPDATE DoctorSpecialDates SET IsAvailable = @IsAvailable, StartTime = @StartTime, EndTime = @EndTime,
                    Reason = @Reason WHERE SpecialDateId = @SpecialDateId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, specialDate);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int specialDateId)
        {
            var sql = "DELETE FROM DoctorSpecialDates WHERE SpecialDateId = @SpecialDateId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { SpecialDateId = specialDateId });
            return affectedRows > 0;
        }
    }
}
