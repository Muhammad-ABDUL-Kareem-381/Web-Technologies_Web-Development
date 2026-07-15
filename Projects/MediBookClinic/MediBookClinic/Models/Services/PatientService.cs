using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Interfaces.Service;

namespace MediBookClinic.Models.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientPreferenceRepository _preferenceRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientRepository patientRepository, IPatientPreferenceRepository preferenceRepository,
            IAppointmentRepository appointmentRepository, ILogger<PatientService> logger)
        {
            _patientRepository = patientRepository;
            _preferenceRepository = preferenceRepository;
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            try
            {
                patient.CreatedAt = DateTime.UtcNow;
                patient.UpdatedAt = DateTime.UtcNow;

                var createdPatient = await _patientRepository.CreateAsync(patient);
                _logger.LogInformation("Patient created successfully with ID: {PatientId}", createdPatient.PatientId);

                // Create default preferences
                var defaultPreferences = new PatientPreference
                {
                    PatientId = createdPatient.PatientId,
                    PreferredLanguage = "en-US",
                    UpdatedAt = DateTime.UtcNow
                };
                await _preferenceRepository.CreateAsync(defaultPreferences);

                return createdPatient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating patient for UserId: {UserId}", patient.UserId);
                throw;
            }
        }

        public async Task<Patient?> GetPatientByIdAsync(int patientId)
        {
            return await _patientRepository.GetByIdAsync(patientId);
        }

        public async Task<Patient?> GetPatientByUserIdAsync(string userId)
        {
            return await _patientRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllAsync();
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            try
            {
                patient.UpdatedAt = DateTime.UtcNow;
                return await _patientRepository.UpdateAsync(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating patient with ID: {PatientId}", patient.PatientId);
                throw;
            }
        }

        public async Task<bool> DeletePatientAsync(int patientId)
        {
            try
            {
                return await _patientRepository.DeleteAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting patient with ID: {PatientId}", patientId);
                throw;
            }
        }

        public async Task<PatientPreference?> GetPatientPreferencesAsync(int patientId)
        {
            return await _preferenceRepository.GetByPatientIdAsync(patientId);
        }

        public async Task<bool> UpdatePatientPreferencesAsync(PatientPreference preferences)
        {
            preferences.UpdatedAt = DateTime.UtcNow;
            return await _preferenceRepository.UpdateAsync(preferences);
        }

        public async Task<int> GetPatientTotalAppointmentsAsync(int patientId)
        {
            var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);
            return appointments.Count();
        }

        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm)
        {
            return await _patientRepository.SearchAsync(searchTerm);
        }
    }
}