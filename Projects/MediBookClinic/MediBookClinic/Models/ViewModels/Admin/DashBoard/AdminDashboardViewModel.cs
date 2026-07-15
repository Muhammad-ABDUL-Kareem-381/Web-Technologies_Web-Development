using System.ComponentModel.DataAnnotations;

namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // ViewModel for Admin/MasterAdmin Dashboard - System overview
    public class AdminDashboardViewModel
    {
        public string AdminName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // Admin or MasterAdmin
        public bool IsMasterAdmin => Role == "MasterAdmin";

        [Display(Name = "Pending Doctor Approvals")]
        public int PendingDoctorsCount { get; set; }

        public List<PendingDoctorViewModel> PendingDoctors { get; set; } = new List<PendingDoctorViewModel>();

        [Display(Name = "Total Users")]
        public int TotalUsers { get; set; }

        [Display(Name = "Total Doctors")]
        public int TotalDoctors { get; set; }

        [Display(Name = "Active Doctors")]
        public int ActiveDoctors { get; set; }

        [Display(Name = "Total Patients")]
        public int TotalPatients { get; set; }

        [Display(Name = "Total Admins")]
        public int TotalAdmins { get; set; }

        [Display(Name = "Total Appointments")]
        public int TotalAppointments { get; set; }

        [Display(Name = "Today's Appointments")]
        public int TodayAppointments { get; set; }

        [Display(Name = "This Week's Appointments")]
        public int WeekAppointments { get; set; }

        [Display(Name = "This Month's Appointments")]
        public int MonthAppointments { get; set; }

        [Display(Name = "Completed Appointments")]
        public int CompletedAppointments { get; set; }

        [Display(Name = "Cancelled Appointments")]
        public int CancelledAppointments { get; set; }

        [Display(Name = "Pending Appointments")]
        public int PendingAppointments { get; set; }

        [Display(Name = "Total Revenue")]
        [DataType(DataType.Currency)]
        public decimal TotalRevenue { get; set; }

        [Display(Name = "Today's Revenue")]
        [DataType(DataType.Currency)]
        public decimal TodayRevenue { get; set; }

        [Display(Name = "This Week's Revenue")]
        [DataType(DataType.Currency)]
        public decimal WeekRevenue { get; set; }

        [Display(Name = "This Month's Revenue")]
        [DataType(DataType.Currency)]
        public decimal MonthRevenue { get; set; }

        [Display(Name = "New Users This Month")]
        public int NewUsersThisMonth { get; set; }

        [Display(Name = "New Doctors This Month")]
        public int NewDoctorsThisMonth { get; set; }

        [Display(Name = "New Patients This Month")]
        public int NewPatientsThisMonth { get; set; }

        // RECENT ACTIVITY
        public List<RecentUserViewModel> RecentUsers { get; set; } = new List<RecentUserViewModel>();
        public List<RecentAppointmentViewModel> RecentAppointments { get; set; } = new List<RecentAppointmentViewModel>();

        // TOP PERFORMERS
        public List<TopDoctorViewModel> TopRatedDoctors { get; set; } = new List<TopDoctorViewModel>();
        public List<TopDoctorViewModel> MostBookedDoctors { get; set; } = new List<TopDoctorViewModel>();

        // SYSTEM HEALTH
        public SystemHealthViewModel SystemHealth { get; set; } = new SystemHealthViewModel();

        // User growth trend (last 6 months)
        public List<MonthlyGrowthData> UserGrowthTrend { get; set; } = new List<MonthlyGrowthData>();

        // Revenue trend (last 6 months)    
        public List<MonthlyRevenueData> RevenueTrend { get; set; } = new List<MonthlyRevenueData>();

        // Appointment status distribution    
        public AppointmentDistributionData AppointmentDistribution { get; set; } = new AppointmentDistributionData();

        // User distribution by role    
        public UserDistributionData UserDistribution { get; set; } = new UserDistributionData();

        // Welcome message    
        public string WelcomeMessage
        {
            get
            {
                var hour = DateTime.Now.Hour;
                var greeting = hour < 12 ? "Good Morning" : hour < 18 ? "Good Afternoon" : "Good Evening";
                return $"{greeting}, {AdminName.Split(' ').FirstOrDefault()}!";
            }
        }

        // Has pending doctors
        public bool HasPendingDoctors => PendingDoctorsCount > 0;

        // Completion rate
        public double CompletionRate => TotalAppointments == 0 ? 0 : Math.Round((double)CompletedAppointments / TotalAppointments * 100, 1);

        // Cancellation rate    
        public double CancellationRate => TotalAppointments == 0 ? 0 : Math.Round((double)CancelledAppointments / TotalAppointments * 100, 1);
    
        // Doctor activation rate
        public double DoctorActivationRate => TotalDoctors == 0 ? 0 : Math.Round((double)ActiveDoctors / TotalDoctors * 100, 1);

        // Average revenue per appointment    
        public decimal AverageRevenuePerAppointment => CompletedAppointments == 0 ? 0 : Math.Round(TotalRevenue / CompletedAppointments, 2);

        // Quick stats for dashboard cards    
        public List<AdminQuickStatCard> QuickStats => new List<AdminQuickStatCard>
        {
            new AdminQuickStatCard
            {
                Title = "Pending Approvals",
                Value = PendingDoctorsCount.ToString(),
                Icon = "fas fa-user-clock",
                Color = "warning",
                Trend = PendingDoctorsCount > 0 ? "Needs attention" : "All clear",
                ActionUrl = "/Admin/PendingDoctors"
            },
            new AdminQuickStatCard
            {
                Title = "Total Users",
                Value = TotalUsers.ToString(),
                Icon = "fas fa-users",
                Color = "primary",
                Trend = $"+{NewUsersThisMonth} this month",
                ActionUrl = "/Admin/Users"
            },
            new AdminQuickStatCard
            {
                Title = "Today's Appointments",
                Value = TodayAppointments.ToString(),
                Icon = "fas fa-calendar-day",
                Color = "success",
                Trend = $"{WeekAppointments} this week",
                ActionUrl = "/Admin/Appointments"
            },
            new AdminQuickStatCard
            {
                Title = "Month Revenue",
                Value = $"${MonthRevenue:N0}",
                Icon = "fas fa-dollar-sign",
                Color = "info",
                Trend = $"${TotalRevenue:N0} total",
                ActionUrl = "/Admin/Reports"
            }
        };
    }
}
