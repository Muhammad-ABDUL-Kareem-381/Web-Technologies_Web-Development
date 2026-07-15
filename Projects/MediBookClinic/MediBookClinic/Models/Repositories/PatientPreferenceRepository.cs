using Dapper;
using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;

namespace MediBookClinic.Models.Repositories
{
    public class PatientPreferenceRepository : IPatientPreferenceRepository
    {
        private readonly DapperContext _context;

        public PatientPreferenceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<PatientPreference> CreateAsync(PatientPreference preference)
        {
            var sql = @"INSERT INTO PatientPreferences (PatientId, PreferredSpecialization, PreferredDayOfWeek, PreferredTimeOfDay, MaxDistance, MaxWaitingTime, PreferredLanguage, UpdatedAt)
                      VALUES (@PatientId, @PreferredSpecialization, @PreferredDayOfWeek, @PreferredTimeOfDay, @MaxDistance, @MaxWaitingTime, @PreferredLanguage, @UpdatedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, preference);
            preference.PreferenceId = id;
            return preference;
        }

        public async Task<PatientPreference?> GetByPatientIdAsync(int patientId)
        {
            var sql = "SELECT * FROM PatientPreferences WHERE PatientId = @PatientId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<PatientPreference>(sql, new { PatientId = patientId });
        }

        public async Task<bool> UpdateAsync(PatientPreference preference)
        {
            var sql = @"UPDATE PatientPreferences SET PreferredSpecialization = @PreferredSpecialization, PreferredDayOfWeek = @PreferredDayOfWeek,
                      PreferredTimeOfDay = @PreferredTimeOfDay, MaxDistance = @MaxDistance, MaxWaitingTime = @MaxWaitingTime,
                      PreferredLanguage = @PreferredLanguage, UpdatedAt = @UpdatedAt WHERE PreferenceId = @PreferenceId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, preference);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int patientId)
        {
            var sql = "DELETE FROM PatientPreferences WHERE PatientId = @PatientId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { PatientId = patientId });
            return affectedRows > 0;
        }
    }
}
