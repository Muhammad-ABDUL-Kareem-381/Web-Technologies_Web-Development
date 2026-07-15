
using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Service
{
    public interface IPatientService
    {
        Task<Patient> CreatePatientAsync(Patient patient);
        Task<Patient?> GetPatientByIdAsync(int patientId);
        Task<Patient?> GetPatientByUserIdAsync(string userId);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int patientId);
        Task<PatientPreference?> GetPatientPreferencesAsync(int patientId);
        Task<bool> UpdatePatientPreferencesAsync(PatientPreference preferences);
        Task<int> GetPatientTotalAppointmentsAsync(int patientId);
        Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm);
    }
}