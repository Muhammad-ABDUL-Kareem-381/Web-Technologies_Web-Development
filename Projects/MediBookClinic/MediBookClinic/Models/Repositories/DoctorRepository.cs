using MediBookClinic.Data;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Entities;
using Dapper;

namespace MediBookClinic.Models.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DapperContext _context;

        public DoctorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            var sql = @"INSERT INTO Doctors (UserId, Specialization, LicenseNumber, YearsOfExperience, Qualification, Biography, ConsultationFee, Rating, TotalReviews, IsAvailableForBooking, CreatedAt, UpdatedAt) VALUES (@UserId, @Specialization, @LicenseNumber, @YearsOfExperience, @Qualification, @Biography, @ConsultationFee, @Rating, @TotalReviews, @IsAvailableForBooking, @CreatedAt, @UpdatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, doctor);
            doctor.DoctorId = id;
            return doctor;
        }

        public async Task<Doctor?> GetByIdAsync(int doctorId)
        {
            var sql = "SELECT * FROM Doctors WHERE DoctorId = @DoctorId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Doctor>(sql, new { DoctorId = doctorId });
        }

        public async Task<Doctor?> GetByUserIdAsync(string userId)
        {
            var sql = "SELECT * FROM Doctors WHERE UserId = @UserId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Doctor>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            var sql = "SELECT * FROM Doctors ORDER BY CreatedAt DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Doctor>(sql);
        }

        public async Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization)
        {
            var sql = "SELECT * FROM Doctors WHERE Specialization = @Specialization AND IsAvailableForBooking = 1 ORDER BY Rating DESC, YearsOfExperience DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Doctor>(sql, new { Specialization = specialization });
        }

        public async Task<IEnumerable<Doctor>> SearchAsync(string searchTerm, string? specialization = null, string? city = null)
        {
            var sql = @"SELECT d.* FROM Doctors d INNER JOIN AspNetUsers u ON d.UserId = u.Id WHERE d.IsAvailableForBooking = 1 AND (@SearchTerm IS NULL OR u.FirstName LIKE '%' + @SearchTerm + '%' OR u.LastName LIKE '%' + @SearchTerm + '%' OR d.Specialization LIKE '%' + @SearchTerm + '%') AND (@Specialization IS NULL OR d.Specialization = @Specialization) AND (@City IS NULL OR u.City = @City) ORDER BY d.Rating DESC, d.YearsOfExperience DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Doctor>(sql, new { SearchTerm = searchTerm, Specialization = specialization, City = city });
        }

        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            var sql = @"UPDATE Doctors SET Specialization = @Specialization, LicenseNumber = @LicenseNumber, YearsOfExperience = @YearsOfExperience, Qualification = @Qualification, Biography = @Biography, ConsultationFee = @ConsultationFee, Rating = @Rating, TotalReviews = @TotalReviews, IsAvailableForBooking = @IsAvailableForBooking, UpdatedAt = @UpdatedAt WHERE DoctorId = @DoctorId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, doctor);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int doctorId)
        {
            var sql = "DELETE FROM Doctors WHERE DoctorId = @DoctorId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { DoctorId = doctorId });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Doctor>> GetTopRatedAsync(int count)
        {
            var sql = "SELECT TOP(@Count) * FROM Doctors WHERE IsAvailableForBooking = 1 AND TotalReviews > 0 ORDER BY Rating DESC, TotalReviews DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Doctor>(sql, new { Count = count });
        }

        public async Task<bool> ExistsAsync(int doctorId)
        {
            var sql = "SELECT COUNT(1) FROM Doctors WHERE DoctorId = @DoctorId";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(sql, new { DoctorId = doctorId });
            return count > 0;
        }
    }
}
