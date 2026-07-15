using Dapper;
using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;

namespace MediBookClinic.Models.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DapperContext _context;

        public ReviewRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<AppointmentReview> CreateAsync(AppointmentReview review)
        {
            var sql = @"INSERT INTO AppointmentReviews (AppointmentId, PatientId, DoctorId, Rating, Review, IsAnonymous, CreatedAt)
                      VALUES (@AppointmentId, @PatientId, @DoctorId, @Rating, @Review, @IsAnonymous, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, review);
            review.ReviewId = id;
            await connection.ExecuteAsync("sp_UpdateDoctorRating", new { DoctorId = review.DoctorId }, commandType: System.Data.CommandType.StoredProcedure);
            return review;
        }

        public async Task<AppointmentReview?> GetByIdAsync(int reviewId)
        {
            var sql = "SELECT * FROM AppointmentReviews WHERE ReviewId = @ReviewId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<AppointmentReview>(sql, new { ReviewId = reviewId });
        }

        public async Task<IEnumerable<AppointmentReview>> GetByDoctorIdAsync(int doctorId)
        {
            var sql = "SELECT * FROM AppointmentReviews WHERE DoctorId = @DoctorId ORDER BY CreatedAt DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<AppointmentReview>(sql, new { DoctorId = doctorId });
        }

        public async Task<AppointmentReview?> GetByAppointmentIdAsync(int appointmentId)
        {
            var sql = "SELECT * FROM AppointmentReviews WHERE AppointmentId = @AppointmentId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<AppointmentReview>(sql, new { AppointmentId = appointmentId });
        }

        public async Task<bool> UpdateAsync(AppointmentReview review)
        {
            var sql = "UPDATE AppointmentReviews SET Rating = @Rating, Review = @Review, IsAnonymous = @IsAnonymous WHERE ReviewId = @ReviewId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, review);
            if (affectedRows > 0)
            {
                await connection.ExecuteAsync("sp_UpdateDoctorRating", new { DoctorId = review.DoctorId }, commandType: System.Data.CommandType.StoredProcedure);
            }
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int reviewId)
        {
            var review = await GetByIdAsync(reviewId);
            if (review == null)
            {
                return false;
            }
            var sql = "DELETE FROM AppointmentReviews WHERE ReviewId = @ReviewId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { ReviewId = reviewId });
            if (affectedRows > 0)
            {
                await connection.ExecuteAsync("sp_UpdateDoctorRating", new { DoctorId = review.DoctorId }, commandType: System.Data.CommandType.StoredProcedure);
            }
            return affectedRows > 0;
        }

        public async Task<decimal> GetAverageRatingAsync(int doctorId)
        {
            var sql = "SELECT ISNULL(AVG(CAST(Rating AS DECIMAL(3,2))), 0) FROM AppointmentReviews WHERE DoctorId = @DoctorId";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<decimal>(sql, new { DoctorId = doctorId });
        }

        public async Task<Dictionary<int, int>> GetRatingDistributionAsync(int doctorId)
        {
            var sql = "SELECT Rating, COUNT(*) as Count FROM AppointmentReviews WHERE DoctorId = @DoctorId GROUP BY Rating";
            using var connection = _context.CreateConnection();
            var results = await connection.QueryAsync<(int Rating, int Count)>(sql, new { DoctorId = doctorId });
            return results.ToDictionary(r => r.Rating, r => r.Count);
        }
    }
}
