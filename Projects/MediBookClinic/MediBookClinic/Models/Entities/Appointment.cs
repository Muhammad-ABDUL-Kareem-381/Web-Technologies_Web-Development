namespace MediBookClinic.Models.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Status { get; set; } // Pending, Confirmed, InProgress, Completed, Cancelled, NoShow
        public string? ReasonForVisit { get; set; }
        public string? Symptoms { get; set; }
        public string? Diagnosis { get; set; }
        public string? Prescription { get; set; }
        public string? Notes { get; set; }
        public string? CancellationReason { get; set; }
        public bool IsRecommended { get; set; }
        public decimal? RecommendationScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        // Navigation Properties
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
