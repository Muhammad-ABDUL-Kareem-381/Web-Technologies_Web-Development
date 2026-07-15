using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Doctor
{
    // ViewModel for displaying doctor in list format (search results, featured doctors, etc.)
    // Used in: Search results, homepage, doctor listings, and maybe in partials for other views
    // ViewModel for doctor list with pagination
    public class DoctorListViewModel
    {
        // List of doctor cards
        public List<DoctorCardViewModel> Doctors { get; set; } = new List<DoctorCardViewModel>();

        // Search query
        [Display(Name = "Search")]
        public string? SearchQuery { get; set; }

        // Specialization
        [Display(Name = "Specialization")]
        public string? Specialization { get; set; }

        // City
        [Display(Name = "City")]
        public string? City { get; set; }

        // Minimum rating
        [Display(Name = "Minimum Rating")]
        [Range(0, 5)]
        public decimal? MinRating { get; set; }

        // Maximum consultation fee
        [Display(Name = "Max Fee")]
        [DataType(DataType.Currency)]
        public decimal? MaxFee { get; set; }

        // Available only
        [Display(Name = "Available Only")]
        public bool AvailableOnly { get; set; } = true;

        // Sort by
        [Display(Name = "Sort By")]
        public string SortBy { get; set; } = "rating_desc";

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        // Available specializations for filter dropdown
        public List<string> AvailableSpecializations { get; set; } = new List<string>();

        // Available cities for filter dropdown
        public List<string> AvailableCities { get; set; } = new List<string>();

        // Results count text
        public string ResultsText => TotalItems switch
        {
            0 => "No doctors found",
            1 => "1 doctor found",
            _ => $"{TotalItems} doctors found"
        };
    }
}
