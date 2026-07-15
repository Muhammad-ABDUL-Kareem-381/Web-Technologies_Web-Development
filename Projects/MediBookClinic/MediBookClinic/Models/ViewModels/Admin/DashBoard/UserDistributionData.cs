namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{
    // User distribution by role
    public class UserDistributionData
    {
        public int Doctors { get; set; }
        public int Patients { get; set; }
        public int Admins { get; set; }
        public int Total => Doctors + Patients + Admins;
        public double DoctorPercentage => Total == 0 ? 0 : Math.Round((double)Doctors / Total * 100, 1);
        public double PatientPercentage => Total == 0 ? 0 : Math.Round((double)Patients / Total * 100, 1);
        public double AdminPercentage => Total == 0 ? 0 : Math.Round((double)Admins / Total * 100, 1);
    }
}
