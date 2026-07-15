using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Doctor.DashBoard
{
    // ViewModel for Doctor Dashboard - Main overview page
    public class DoctorDashboardViewModel
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public decimal Rating { get; set; }
        public int TotalReviews { get; set; }
        public bool IsAvailableForBooking { get; set; }

        [Display(Name = "Today's Appointments")]
        public int TodayAppointments { get; set; }

        [Display(Name = "Completed Today")]
        public int TodayCompleted { get; set; }

        [Display(Name = "Pending Today")]
        public int TodayPending { get; set; }

        [Display(Name = "Cancelled Today")]
        public int TodayCancelled { get; set; }

        [Display(Name = "Today's Revenue")]
        [DataType(DataType.Currency)]
        public decimal TodayRevenue { get; set; }

        [Display(Name = "This Week's Appointments")]
        public int WeekAppointments { get; set; }

        [Display(Name = "Week Completed")]
        public int WeekCompleted { get; set; }

        [Display(Name = "Week Revenue")]
        [DataType(DataType.Currency)]
        public decimal WeekRevenue { get; set; }

        [Display(Name = "This Month's Appointments")]
        public int MonthAppointments { get; set; }

        [Display(Name = "Month Completed")]
        public int MonthCompleted { get; set; }

        [Display(Name = "Month Revenue")]
        [DataType(DataType.Currency)]
        public decimal MonthRevenue { get; set; }

        [Display(Name = "New Patients This Month")]
        public int NewPatientsThisMonth { get; set; }

        [Display(Name = "Total Appointments")]
        public int TotalAppointments { get; set; }

        [Display(Name = "Total Completed")]
        public int TotalCompleted { get; set; }

        [Display(Name = "Total Cancelled")]
        public int TotalCancelled { get; set; }

        [Display(Name = "Total No-Show")]
        public int TotalNoShow { get; set; }

        [Display(Name = "Total Patients Served")]
        public int TotalPatientsServed { get; set; }

        [Display(Name = "Total Revenue")]
        [DataType(DataType.Currency)]
        public decimal TotalRevenue { get; set; }

        // UPCOMING APPOINTMENTS (Next 5)
        public List<UpcomingAppointmentViewModel> UpcomingAppointments { get; set; } = new List<UpcomingAppointmentViewModel>();

        // RECENT APPOINTMENTS (Last 5 completed)
        public List<RecentAppointmentViewModel> RecentAppointments { get; set; } = new List<RecentAppointmentViewModel>();

        // RECENT REVIEWS (Last 5)
        public List<RecentReviewViewModel> RecentReviews { get; set; } = new List<RecentReviewViewModel>();

        // NOTIFICATIONS (Unread)
        public int UnreadNotificationsCount { get; set; }
        public List<NotificationSummaryViewModel> RecentNotifications { get; set; } = new List<NotificationSummaryViewModel>();

        // Appointments by status (for pie chart)
        public AppointmentStatusChartData AppointmentStatusData { get; set; } = new AppointmentStatusChartData();

        // Monthly appointments trend (for line chart - last 6 months)    
        public List<MonthlyTrendData> MonthlyTrend { get; set; } = new List<MonthlyTrendData>();

        // Revenue trend (last 6 months)
        public List<MonthlyRevenueData> RevenueTrend { get; set; } = new List<MonthlyRevenueData>();

        // Peak hours data (for bar chart)    
        public List<PeakHourData> PeakHours { get; set; } = new List<PeakHourData>();
    
        // Completion rate percentage    
        public double CompletionRate => TotalAppointments == 0 ? 0 : Math.Round((double)TotalCompleted / TotalAppointments * 100, 1);
    
        // Cancellation rate percentage
        public double CancellationRate => TotalAppointments == 0 ? 0 : Math.Round((double)TotalCancelled / TotalAppointments * 100, 1);

        // No-show rate percentage    
        public double NoShowRate => TotalAppointments == 0 ? 0 : Math.Round((double)TotalNoShow / TotalAppointments * 100, 1);

        // Average revenue per appointment    
        public decimal AverageRevenuePerAppointment => TotalCompleted == 0 ? 0 : Math.Round(TotalRevenue / TotalCompleted, 2);

        // Has appointments today    
        public bool HasAppointmentsToday => TodayAppointments > 0;

        // Has upcoming appointments    
        public bool HasUpcomingAppointments => UpcomingAppointments.Any();

        // Has recent reviews    
        public bool HasRecentReviews => RecentReviews.Any();

        // Profile image or default    
        public string ProfileImageOrDefault => string.IsNullOrEmpty(ProfileImageUrl) ? "/images/default-doctor-avatar.png" : ProfileImageUrl;

        // Welcome message based on time of day    
        public string WelcomeMessage
        {
            get
            {
                var hour = DateTime.Now.Hour;
                var greeting = hour < 12 ? "Good Morning" : hour < 18 ? "Good Afternoon" : "Good Evening";
                return $"{greeting}, Dr. {DoctorName.Split(' ').FirstOrDefault()}!";
            }
        }
    
        // Next appointment (if any)
        public UpcomingAppointmentViewModel? NextAppointment => UpcomingAppointments.FirstOrDefault();

        // Has next appointment
        public bool HasNextAppointment => NextAppointment != null;

        // Quick stats summary for cards
        public List<QuickStatCard> QuickStats => new List<QuickStatCard>
        {
            new QuickStatCard
            {
                Title = "Today's Appointments",
                Value = TodayAppointments.ToString(),
                Icon = "fas fa-calendar-day",
                Color = "primary",
                Change = TodayAppointments > 0 ? $"{TodayCompleted} completed" : "No appointments"
            },
            new QuickStatCard
            {
                Title = "This Week",
                Value = WeekAppointments.ToString(),
                Icon = "fas fa-calendar-week",
                Color = "success",
                Change = $"{WeekCompleted} completed"
            },
            new QuickStatCard
            {
                Title = "Rating",
                Value = Rating.ToString("0.0"),
                Icon = "fas fa-star",
                Color = "warning",
                Change = $"{TotalReviews} reviews"
            },
            new QuickStatCard
            {
                Title = "This Month Revenue",
                Value = $"${MonthRevenue:N0}",
                Icon = "fas fa-dollar-sign",
                Color = "info",
                Change = $"{MonthAppointments} appointments"
            }
        };
    }
}
