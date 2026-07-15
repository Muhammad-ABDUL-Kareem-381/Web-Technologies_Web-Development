namespace MediBookClinic.Models.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string? UserId { get; set; }
        public string? Specialization { get; set; }
        public string? LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Qualification { get; set; }
        public string? Biography { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal Rating { get; set; }
        public int TotalReviews { get; set; }
        public bool IsAvailableForBooking { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        // Doctor Info from Identity table
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? FullName => $"Dr. {FirstName} {LastName}";
    }
}
