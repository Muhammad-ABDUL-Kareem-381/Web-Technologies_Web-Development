namespace MediBookClinic.Models.ViewModels.Shared
{
    // Generic pagination view model for lists
    public class PaginatedViewModel<T> where T : class
    {
        // Current page number (1-based)
        public int PageNumber { get; set; } = 1;

        // Number of items per page
        public int PageSize { get; set; } = 10;

        // Total number of items across all pages
        public int TotalItems { get; set; }

        // Total number of pages
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        // Whether there is a previous page
        public bool HasPreviousPage => PageNumber > 1;

        // Whether there is a next page
        public bool HasNextPage => PageNumber < TotalPages;

        // The items for the current page
        public List<T> Items { get; set; } = new List<T>();

        // Starting item number for current page
        public int StartItem => TotalItems == 0 ? 0 : (PageNumber - 1) * PageSize + 1;

        // Ending item number for current page
        public int EndItem => Math.Min(PageNumber * PageSize, TotalItems);

        // Search query (if applicable)
        public string? SearchQuery { get; set; }

        // Sort field
        public string? SortBy { get; set; }

        // Sort direction (asc/desc)
        public string SortDirection { get; set; } = "asc";

        // Filter parameters (for advanced filtering)
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

        // Create paginated result
        public static PaginatedViewModel<T> Create(IEnumerable<T> source,int pageNumber,int pageSize,int totalItems)
        {
            return new PaginatedViewModel<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = source.ToList()
            };
        }

        // Create paginated result with full query parameters
        public static PaginatedViewModel<T> Create(IEnumerable<T> source,int pageNumber,int pageSize,int totalItems,string? searchQuery = null,string? sortBy = null,string? sortDirection = "asc",Dictionary<string, string>? filters = null)
        {
            return new PaginatedViewModel<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = source.ToList(),
                SearchQuery = searchQuery,
                SortBy = sortBy,
                SortDirection = sortDirection ?? "asc",
                Filters = filters ?? new Dictionary<string, string>()
            };
        }
    }
}