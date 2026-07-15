using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Repository
{
    public interface IPatientRepository
    {
        Task<Patient> CreateAsync(Patient patient);
        Task<Patient?> GetByIdAsync(int patientId);
        Task<Patient?> GetByUserIdAsync(string userId);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<bool> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int patientId);
        Task<IEnumerable<Patient>> SearchAsync(string searchTerm);
        Task<bool> ExistsAsync(int patientId);
    }
}