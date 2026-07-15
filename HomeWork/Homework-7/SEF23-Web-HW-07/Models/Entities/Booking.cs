namespace SEF23_Web_HW_07.Models.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string UserId { get; set; } = string.Empty; // Add this
        public int PackageId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public int NumberOfTravelers { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending"; // Add this
    }
}