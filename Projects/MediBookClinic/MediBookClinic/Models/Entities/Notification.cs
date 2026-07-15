namespace MediBookClinic.Models.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
        public string? Channel { get; set; }
        public bool IsRead { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentAt { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
