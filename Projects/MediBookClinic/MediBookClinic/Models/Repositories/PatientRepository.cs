using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;
using Dapper;


namespace MediBookClinic.Models.Repositories
{

    public class PatientRepository : IPatientRepository
    {
        private readonly DapperContext _context;

        public PatientRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            var sql = @"INSERT INTO Patients (UserId, BloodGroup, Allergies, MedicalHistory, EmergencyContactName, EmergencyContactPhone, InsuranceProvider, 
                      InsuranceNumber, CreatedAt, UpdatedAt) VALUES (@UserId, @BloodGroup, @Allergies, @MedicalHistory, 
                      @EmergencyContactName, @EmergencyContactPhone, @InsuranceProvider, @InsuranceNumber, @CreatedAt, @UpdatedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, patient);
            patient.PatientId = id;
            return patient;
        }

        public async Task<Patient?> GetByIdAsync(int patientId)
        {
            var sql = "SELECT * FROM Patients WHERE PatientId = @PatientId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Patient>(sql, new { PatientId = patientId });
        }

        public async Task<Patient?> GetByUserIdAsync(string userId)
        {
            var sql = "SELECT * FROM Patients WHERE UserId = @UserId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Patient>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            var sql = "SELECT * FROM Patients ORDER BY CreatedAt DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Patient>(sql);
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            var sql = @"UPDATE Patients SET BloodGroup = @BloodGroup, Allergies = @Allergies, MedicalHistory = @MedicalHistory,
                      EmergencyContactName = @EmergencyContactName, EmergencyContactPhone = @EmergencyContactPhone, InsuranceProvider = @InsuranceProvider,
                      InsuranceNumber = @InsuranceNumber, UpdatedAt = @UpdatedAt WHERE PatientId = @PatientId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, patient);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int patientId)
        {
            var sql = "DELETE FROM Patients WHERE PatientId = @PatientId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { PatientId = patientId });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm)
        {
            var sql = @"SELECT p.* FROM Patients p INNER JOIN AspNetUsers u ON p.UserId = u.Id WHERE u.FirstName LIKE '%' + @SearchTerm + '%' OR 
                      u.LastName LIKE '%' + @SearchTerm + '%' OR u.Email LIKE '%' + @SearchTerm + '%' OR
                      u.PhoneNumber LIKE '%' + @SearchTerm + '%' ORDER BY p.CreatedAt DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Patient>(sql, new { SearchTerm = searchTerm });
        }

        public async Task<bool> ExistsAsync(int patientId)
        {
            var sql = "SELECT COUNT(1) FROM Patients WHERE PatientId = @PatientId";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(sql, new { PatientId = patientId });
            return count > 0;
        }
    }
}
