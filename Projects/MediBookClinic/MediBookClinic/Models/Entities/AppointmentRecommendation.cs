namespace MediBookClinic.Models.Entities
{
    public class AppointmentRecommendation
    {
        public int RecommendationId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public decimal MatchScore { get; set; }
        public string? ReasonForRecommendation { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
