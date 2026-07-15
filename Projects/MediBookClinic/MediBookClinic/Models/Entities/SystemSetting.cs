namespace MediBookClinic.Models.Entities
{
    public class SystemSetting
    {
        public int SettingId { get; set; }
        public string? SettingKey { get; set; }
        public string? SettingValue { get; set; }
        public string? Description { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
