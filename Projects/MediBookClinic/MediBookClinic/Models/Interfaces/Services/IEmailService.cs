namespace MediBookClinic.Models.Interfaces.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlBody);
        Task SendWelcomeEmailAsync(string toEmail, string firstName, string lastName, string userType);
        Task SendAdminNotificationAsync(string toEmail, string doctorName, string email, string phone, string specialization, string license, int experience, int doctorId);
    }
}
