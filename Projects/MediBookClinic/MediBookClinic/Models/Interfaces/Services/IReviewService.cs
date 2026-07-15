using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Service
{
    public interface IReviewService
    {
        Task<AppointmentReview> CreateReviewAsync(AppointmentReview review);
        Task<AppointmentReview?> GetReviewByIdAsync(int reviewId);
        Task<IEnumerable<AppointmentReview>> GetDoctorReviewsAsync(int doctorId);
        Task<AppointmentReview?> GetReviewByAppointmentIdAsync(int appointmentId);
        Task<bool> UpdateReviewAsync(AppointmentReview review);
        Task<bool> DeleteReviewAsync(int reviewId);
        Task<decimal> CalculateDoctorAverageRatingAsync(int doctorId);
        Task<bool> CanPatientReviewAppointmentAsync(int appointmentId, int patientId);
        Task<Dictionary<int, int>> GetDoctorRatingDistributionAsync(int doctorId);
    }
}