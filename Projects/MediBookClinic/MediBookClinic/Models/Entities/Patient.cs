namespace MediBookClinic.Models.Entities
{ 
    public class Patient
    {
        public int PatientId { get; set; }
        public string? UserId { get; set; }
        public string? BloodGroup { get; set; }
        public string? Allergies { get; set; }
        public string? MedicalHistory { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? InsuranceNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        // Patient Info from Identity tabel
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? FullName => $"{FirstName} {LastName}";
    }
}
