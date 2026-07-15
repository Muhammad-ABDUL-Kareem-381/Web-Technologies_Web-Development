namespace MediBookClinic.Models.Entities
{
    public class DoctorSpecialDate
    {
        public int SpecialDateId { get; set; }
        public int DoctorId { get; set; }
        public DateTime SpecialDate { get; set; }
        public bool IsAvailable { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
