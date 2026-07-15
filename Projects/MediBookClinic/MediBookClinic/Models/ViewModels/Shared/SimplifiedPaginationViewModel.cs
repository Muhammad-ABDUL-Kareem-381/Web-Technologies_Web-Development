namespace MediBookClinic.Models.ViewModels.Shared
{
    // Simple pagination metadata (when you don't need full generic model)
    public class SimplifiedPaginationViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int StartItem => TotalItems == 0 ? 0 : (PageNumber - 1) * PageSize + 1;
        public int EndItem => Math.Min(PageNumber * PageSize, TotalItems);
    }
}
