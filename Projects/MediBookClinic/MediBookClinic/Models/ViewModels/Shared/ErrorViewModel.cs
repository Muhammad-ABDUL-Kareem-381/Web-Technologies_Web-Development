namespace MediBookClinic.Models.ViewModels.Shared
{
    // ViewModel for displaying error pages
    public class ErrorViewModel
    {
        // Request ID for tracking errors
        public string? RequestId { get; set; }

        // Whether to show the request ID>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // HTTP status code (404, 500, etc.)
        public int StatusCode { get; set; }

        // Error title/message
        public string? Title { get; set; }

        // Detailed error message
        public string? Message { get; set; }

        // Whether this is a fatal error
        public bool IsFatal { get; set; }

        // URL to redirect user after error (optional)
        public string? RedirectUrl { get; set; }

        // Text for redirect button (optional)
        public string? RedirectText { get; set; }

        // Default error view model
        public static ErrorViewModel Default => new ErrorViewModel
        {
            StatusCode = 500,
            Title = "An Error Occurred",
            Message = "Something went wrong. Please try again later.",
            IsFatal = false
        };

        // 404 Not Found error
        public static ErrorViewModel NotFound => new ErrorViewModel
        {
            StatusCode = 404,
            Title = "Page Not Found",
            Message = "The page you're looking for doesn't exist.",
            IsFatal = false,
            RedirectUrl = "/",
            RedirectText = "Go to Home"
        };

        // 403 Forbidden/Access Denied error
        public static ErrorViewModel AccessDenied => new ErrorViewModel
        {
            StatusCode = 403,
            Title = "Access Denied",
            Message = "You don't have permission to access this resource.",
            IsFatal = false,
            RedirectUrl = "/",
            RedirectText = "Go to Home"
        };

        // 401 Unauthorized error
        public static ErrorViewModel Unauthorized => new ErrorViewModel
        {
            StatusCode = 401,
            Title = "Unauthorized",
            Message = "You need to log in to access this page.",
            IsFatal = false,
            RedirectUrl = "/Identity/Account/Login",
            RedirectText = "Go to Login"
        };
    }
}