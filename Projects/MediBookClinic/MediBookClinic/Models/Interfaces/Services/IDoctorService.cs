using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Service
{
    public interface IDoctorService
    {
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<Doctor?> GetDoctorByIdAsync(int doctorId);
        Task<Doctor?> GetDoctorByUserIdAsync(string userId);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(string specialization);
        Task<IEnumerable<Doctor>> SearchDoctorsAsync(string searchTerm, string? specialization = null, string? city = null);
        Task<bool> UpdateDoctorAsync(Doctor doctor);
        Task<bool> UpdateDoctorStatusAsync(int doctorId, string status);
        Task<bool> DeleteDoctorAsync(int doctorId);
        Task<IEnumerable<Doctor>> GetTopRatedDoctorsAsync(int count = 10);
        Task<decimal> GetDoctorAverageRatingAsync(int doctorId);
        Task<int> GetDoctorTotalAppointmentsAsync(int doctorId);
        Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime date, TimeSpan time);
        Task<IEnumerable<Doctor>> GetPendingDoctorsAsync();
        Task<bool> ApproveDoctorAsync(int doctorId, string approvedByUserId);
        Task<bool> RejectDoctorAsync(int doctorId, string reason, string rejectedByUserId);

    }
}