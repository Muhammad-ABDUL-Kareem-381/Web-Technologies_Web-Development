namespace MediBookClinic.Models.Entities
{
    public class DoctorAvailability
    {
        public int AvailabilityId { get; set; }
        public int DoctorId { get; set; }
        public int DayOfWeek { get; set; } // 0=Sunday, 6=Saturday
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDuration { get; set; } // in minutes
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
