namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // Top performing doctor
    public class TopDoctorViewModel
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int TotalReviews { get; set; }
        public int TotalAppointments { get; set; }
        public decimal TotalRevenue { get; set; }
        public string RatingStars => new string('★', (int)Math.Floor(Rating)) + new string('☆', 5 - (int)Math.Floor(Rating));
    }
}
