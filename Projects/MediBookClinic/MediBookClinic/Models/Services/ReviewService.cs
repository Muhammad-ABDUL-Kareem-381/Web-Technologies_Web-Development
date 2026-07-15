using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Interfaces.Service;

namespace MediBookClinic.Models.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(IReviewRepository reviewRepository,IAppointmentRepository appointmentRepository,ILogger<ReviewService> logger)
        {
            _reviewRepository = reviewRepository;
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<AppointmentReview> CreateReviewAsync(AppointmentReview review)
        {
            // Validate appointment exists and is completed
            var appointment = await _appointmentRepository.GetByIdAsync(review.AppointmentId);
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found.");
            }
            if (appointment.Status != "Completed")
            {
                throw new InvalidOperationException("Can only review completed appointments.");
            }
            // Check if review already exists
            var existingReview = await _reviewRepository.GetByAppointmentIdAsync(review.AppointmentId);
            if (existingReview != null)
            {
                throw new InvalidOperationException("This appointment has already been reviewed.");
            }
            // Validate rating
            if (review.Rating < 1 || review.Rating > 5)
            {
                throw new InvalidOperationException("Rating must be between 1 and 5.");
            }
            review.CreatedAt = DateTime.UtcNow;
            return await _reviewRepository.CreateAsync(review);
        }

        public async Task<AppointmentReview?> GetReviewByIdAsync(int reviewId)
        {
            return await _reviewRepository.GetByIdAsync(reviewId);
        }

        public async Task<IEnumerable<AppointmentReview>> GetDoctorReviewsAsync(int doctorId)
        {
            return await _reviewRepository.GetByDoctorIdAsync(doctorId);
        }

        public async Task<AppointmentReview?> GetReviewByAppointmentIdAsync(int appointmentId)
        {
            return await _reviewRepository.GetByAppointmentIdAsync(appointmentId);
        }

        public async Task<bool> UpdateReviewAsync(AppointmentReview review)
        {
            // Validate rating
            if (review.Rating < 1 || review.Rating > 5)
            {
                throw new InvalidOperationException("Rating must be between 1 and 5.");
            }
            return await _reviewRepository.UpdateAsync(review);
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            return await _reviewRepository.DeleteAsync(reviewId);
        }

        public async Task<decimal> CalculateDoctorAverageRatingAsync(int doctorId)
        {
            return await _reviewRepository.GetAverageRatingAsync(doctorId);
        }

        public async Task<bool> CanPatientReviewAppointmentAsync(int appointmentId, int patientId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null || appointment.PatientId != patientId)
            {
                return false;
            }
            if (appointment.Status != "Completed")
            {
                return false;
            }
            var existingReview = await _reviewRepository.GetByAppointmentIdAsync(appointmentId);
            return existingReview == null;
        }

        public async Task<Dictionary<int, int>> GetDoctorRatingDistributionAsync(int doctorId)
        {
            return await _reviewRepository.GetRatingDistributionAsync(doctorId);
        }
    }
}