using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Repository

{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateAsync(Doctor doctor);
        Task<Doctor?> GetByIdAsync(int doctorId);
        Task<Doctor?> GetByUserIdAsync(string userId);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization);
        Task<IEnumerable<Doctor>> SearchAsync(string searchTerm, string? specialization = null, string? city = null);
        Task<bool> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(int doctorId);
        Task<IEnumerable<Doctor>> GetTopRatedAsync(int count);
        Task<bool> ExistsAsync(int doctorId);
    }
}