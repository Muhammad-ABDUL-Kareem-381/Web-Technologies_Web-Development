using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Doctor
{
    // ViewModel for displaying doctor in card format (search results, featured doctors, etc.)
    // Used in: Search results, homepage, doctor listings, and maybe in partials for other views
    // ViewModel for individual doctor card
    public class DoctorCardViewModel
    {
        public int DoctorId { get; set; }

        [Required]
        [Display(Name = "Doctor Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Specialization")]
        public string Specialization { get; set; } = string.Empty;

        [Display(Name = "Qualification")]
        public string? Qualification { get; set; }

        [Display(Name = "Years of Experience")]
        public int YearsOfExperience { get; set; }

        [Display(Name = "Consultation Fee")]
        [DataType(DataType.Currency)]
        public decimal ConsultationFee { get; set; }

        [Display(Name = "Rating")]
        [Range(0, 5)]
        public decimal Rating { get; set; }

        [Display(Name = "Total Reviews")]
        public int TotalReviews { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "Profile Image")]
        [Url]
        public string? ProfileImageUrl { get; set; }

        [Display(Name = "Available for Booking")]
        public bool IsAvailableForBooking { get; set; }

        // Short biography (truncated)
        [Display(Name = "About")]
        public string? ShortBio { get; set; }

        // Email (optional - for contact)
        [EmailAddress]
        public string? Email { get; set; }

        // Phone number (optional - for contact)
        [Phone]
        public string? PhoneNumber { get; set; }

        // Formatted consultation fee with currency
        public string FormattedFee => $"${ConsultationFee:N2}";

        // Experience display text
        public string ExperienceText => YearsOfExperience == 1? "1 year" : $"{YearsOfExperience} years";

        // Rating display with stars
        public string RatingStars
        {
            get
            {
                var fullStars = (int)Math.Floor(Rating);
                var hasHalfStar = Rating - fullStars >= 0.5m;
                var emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

                var stars = new string('★', fullStars);
                if (hasHalfStar)
                {
                    stars += "⯨";
                }
                stars += new string('☆', emptyStars);

                return stars;
            }
        }

        // Rating display with icon classes (for Font Awesome)
        public string RatingIconClasses
        {
            get
            {
                var fullStars = (int)Math.Floor(Rating);
                var hasHalfStar = Rating - fullStars >= 0.5m;
                var emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

                var classes = "";
                for (int i = 0; i < fullStars; i++)
                {
                    classes += "<i class='fas fa-star text-warning'></i>";
                }
                if (hasHalfStar)
                {
                    classes += "<i class='fas fa-star-half-alt text-warning'></i>";
                }
                for (int i = 0; i < emptyStars; i++)
                {
                    classes += "<i class='far fa-star text-warning'></i>";
                }
                return classes;
            }
        }

        // Review count text
        public string ReviewCountText => TotalReviews switch
        {
            0 => "No reviews yet",
            1 => "1 review",
            _ => $"{TotalReviews} reviews"
        };

        // Availability badge class (Bootstrap)
        public string AvailabilityBadgeClass => IsAvailableForBooking? "badge bg-success" : "badge bg-secondary";

        // Availability badge text
        public string AvailabilityBadgeText => IsAvailableForBooking? "Available" : "Not Available";

        // Profile image URL or default
        public string ProfileImageOrDefault => string.IsNullOrEmpty(ProfileImageUrl) ? "/images/default-doctor-avatar.png" : ProfileImageUrl;

        // Card action URL (view profile)
        public string ProfileUrl => $"/Doctor/Profile/{DoctorId}";

        // Booking URL
        public string BookingUrl => $"/Appointment/Book?doctorId={DoctorId}";

        // Whether doctor has high rating (4.0+)
        public bool IsHighlyRated => Rating >= 4.0m;

        // Whether doctor is experienced (10+ years)
        public bool IsExperienced => YearsOfExperience >= 10;

        // CSS class for card border based on rating
        public string CardBorderClass
        {
            get
            {
                if (Rating >= 4.5m)
                {
                    return "border-success border-2";
                }
                if (Rating >= 4.0m)
                {
                    return "border-primary border-2";
                }
                return "border-light";
            }
        }

        // Badge for highly rated doctors
        public string? SpecialBadge
        {
            get
            {
                if (Rating >= 4.8m && TotalReviews >= 50)
                {
                    return "Top Rated";
                }
                if (YearsOfExperience >= 20)
                {
                    return "Expert";
                }
                if (TotalReviews >= 100)
                {
                    return "Popular";
                }
                return null;
            }
        }

        // CSS class for special badge
        public string SpecialBadgeClass
        {
            get
            {
                if (Rating >= 4.8m && TotalReviews >= 50)
                {
                    return "badge bg-warning text-dark";
                }
                if (YearsOfExperience >= 20)
                {
                    return "badge bg-info";
                }
                if (TotalReviews >= 100)
                {
                    return "badge bg-primary";
                }
                return "badge bg-secondary";
            }
        }
    }
}