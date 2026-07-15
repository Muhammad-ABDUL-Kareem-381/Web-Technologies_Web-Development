using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Repository
{
    public interface IPatientPreferenceRepository
    {
        Task<PatientPreference> CreateAsync(PatientPreference preference);
        Task<PatientPreference?> GetByPatientIdAsync(int patientId);
        Task<bool> UpdateAsync(PatientPreference preference);
        Task<bool> DeleteAsync(int patientId);
    }
}