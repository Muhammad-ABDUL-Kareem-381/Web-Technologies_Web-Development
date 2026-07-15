using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Repository
{
    public interface IReviewRepository
    {
        Task<AppointmentReview> CreateAsync(AppointmentReview review);
        Task<AppointmentReview?> GetByIdAsync(int reviewId);
        Task<IEnumerable<AppointmentReview>> GetByDoctorIdAsync(int doctorId);
        Task<AppointmentReview?> GetByAppointmentIdAsync(int appointmentId);
        Task<bool> UpdateAsync(AppointmentReview review);
        Task<bool> DeleteAsync(int reviewId);
        Task<decimal> GetAverageRatingAsync(int doctorId);
        Task<Dictionary<int, int>> GetRatingDistributionAsync(int doctorId);
    }
}