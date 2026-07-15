namespace MediBookClinic.Models.ViewModels.Patient.DashBoard
{
    // Recommended doctor for patient
    public class RecommendedDoctorViewModel
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public decimal Rating { get; set; }
        public int TotalReviews { get; set; }
        public decimal ConsultationFee { get; set; }
        public string? City { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal MatchScore { get; set; }
        public string? ReasonForRecommendation { get; set; }

        public string ProfileImageOrDefault => string.IsNullOrEmpty(ProfileImageUrl) ? "/images/default-doctor-avatar.png" : ProfileImageUrl;
        public string RatingStars => new string('★', (int)Math.Floor(Rating)) + new string('☆', 5 - (int)Math.Floor(Rating));
        public string MatchScorePercentage => $"{MatchScore:N0}%";
    }
}
