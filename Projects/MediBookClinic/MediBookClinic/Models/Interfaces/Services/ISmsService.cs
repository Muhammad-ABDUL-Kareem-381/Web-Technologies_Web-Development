namespace MediBookClinic.Models.Interfaces.Service
{
    public interface ISmsService
    {
        Task SendSmsAsync(string phoneNumber, string message);
        Task SendWelcomeSmsAsync(string phoneNumber, string firstName);
    }
}
