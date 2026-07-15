namespace MediBookClinic.Models.ViewModels.Admin.DashBoard
{

    // Appointment distribution for pie chart
    public class AppointmentDistributionData
    {
        public int Completed { get; set; }
        public int Pending { get; set; }
        public int Confirmed { get; set; }
        public int Cancelled { get; set; }
        public int NoShow { get; set; }
        public int Total => Completed + Pending + Confirmed + Cancelled + NoShow;
    }
}
