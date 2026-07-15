using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [Url]
        [MaxLength(500)]
        public string ProfileImageUrl { get; set; }

        [Required]
        [MaxLength(10)]
        public string PreferredLanguage { get; set; } = "en-US";

        [Required]
        [MaxLength(20)]
        public string PreferredTheme { get; set; } = "light";

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

}
