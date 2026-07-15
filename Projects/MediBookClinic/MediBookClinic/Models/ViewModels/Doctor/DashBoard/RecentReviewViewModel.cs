namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    // Recent review summary
    public class RecentReviewViewModel
    {
        public int ReviewId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAnonymous { get; set; }
        public string DisplayName => IsAnonymous ? "Anonymous Patient" : PatientName;
        public string FormattedDate => CreatedAt.ToString("MMM dd, yyyy");
        public string RatingStars => new string('★', Rating) + new string('☆', 5 - Rating);
    }
}
