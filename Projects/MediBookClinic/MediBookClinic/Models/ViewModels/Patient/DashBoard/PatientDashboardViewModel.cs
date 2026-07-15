using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Patient.DashBoard
{
    // ViewModel for Patient Dashboard - Main overview page
    public class PatientDashboardViewModel
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public string? BloodGroup { get; set; }
        public int? Age { get; set; }

        // UPCOMING APPOINTMENTS
        public List<PatientUpcomingAppointmentViewModel> UpcomingAppointments { get; set; } = new List<PatientUpcomingAppointmentViewModel>();

        public int UpcomingAppointmentsCount => UpcomingAppointments.Count;

        // PAST APPOINTMENTS
        public List<PatientPastAppointmentViewModel> RecentAppointments { get; set; } = new List<PatientPastAppointmentViewModel>();

        // STATISTICS
        [Display(Name = "Total Appointments")]
        public int TotalAppointments { get; set; }

        [Display(Name = "Completed Appointments")]
        public int CompletedAppointments { get; set; }

        [Display(Name = "Cancelled Appointments")]
        public int CancelledAppointments { get; set; }

        [Display(Name = "Pending Appointments")]
        public int PendingAppointments { get; set; }

        [Display(Name = "Doctors Visited")]
        public int DoctorsVisited { get; set; }

        [Display(Name = "Total Spent")]
        [DataType(DataType.Currency)]
        public decimal TotalSpent { get; set; }

        [Display(Name = "Reviews Written")]
        public int ReviewsWritten { get; set; }

        // RECOMMENDED DOCTORS
        public List<RecommendedDoctorViewModel> RecommendedDoctors { get; set; } = new List<RecommendedDoctorViewModel>();

        // HEALTH SUMMARY
        public HealthSummaryViewModel? HealthSummary { get; set; }

        // NOTIFICATIONS
        public int UnreadNotificationsCount { get; set; }
        public List<PatientNotificationViewModel> RecentNotifications { get; set; } = new List<PatientNotificationViewModel>();

        // RECENT REVIEWS (To Write)
        public List<PendingReviewViewModel> PendingReviews { get; set; } = new List<PendingReviewViewModel>();

        // Has upcoming appointments    
        public bool HasUpcomingAppointments => UpcomingAppointments.Any();

        // Next appointment    
        public PatientUpcomingAppointmentViewModel? NextAppointment => UpcomingAppointments.FirstOrDefault();

        // Has next appointment    
        public bool HasNextAppointment => NextAppointment != null;

        // Profile image or default    
        public string ProfileImageOrDefault => string.IsNullOrEmpty(ProfileImageUrl) ? "/images/default-patient-avatar.png" : ProfileImageUrl;

        // Welcome message    
        public string WelcomeMessage
        {
            get
            {
                var hour = DateTime.Now.Hour;
                var greeting = hour < 12 ? "Good Morning" : hour < 18 ? "Good Afternoon" : "Good Evening";
                return $"{greeting}, {PatientName.Split(' ').FirstOrDefault()}!";
            }
        }
    
        // Completion rate
        public double CompletionRate => TotalAppointments == 0 ? 0 : Math.Round((double)CompletedAppointments / TotalAppointments * 100, 1);

        // Has pending reviews to write    
        public bool HasPendingReviews => PendingReviews.Any();

        // Quick stats for cards    
        public List<PatientQuickStatCard> QuickStats => new List<PatientQuickStatCard>
        {
            new PatientQuickStatCard
            {
                Title = "Upcoming",
                Value = UpcomingAppointmentsCount.ToString(),
                Icon = "fas fa-calendar-check",
                Color = "primary",
                ActionText = "View All",
                ActionUrl = "/Appointment/MyAppointments"
            },
            new PatientQuickStatCard
            {
                Title = "Completed",
                Value = CompletedAppointments.ToString(),
                Icon = "fas fa-check-circle",
                Color = "success",
                ActionText = "View History",
                ActionUrl = "/Appointment/History"
            },
            new PatientQuickStatCard
            {
                Title = "Doctors Visited",
                Value = DoctorsVisited.ToString(),
                Icon = "fas fa-user-md",
                Color = "info",
                ActionText = "Find More",
                ActionUrl = "/Patient/SearchDoctors"
            },
            new PatientQuickStatCard
            {
                Title = "Total Spent",
                Value = $"${TotalSpent:N0}",
                Icon = "fas fa-dollar-sign",
                Color = "warning",
                ActionText = "View Details",
                ActionUrl = "/Patient/PaymentHistory"
            }
        };
    }
}
