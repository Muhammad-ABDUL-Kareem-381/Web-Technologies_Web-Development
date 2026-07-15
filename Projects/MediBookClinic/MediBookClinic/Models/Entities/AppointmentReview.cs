namespace MediBookClinic.Models.Entities
{
    public class AppointmentReview
    {
        public int ReviewId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int Rating { get; set; } // 1-5
        public string? Review { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}