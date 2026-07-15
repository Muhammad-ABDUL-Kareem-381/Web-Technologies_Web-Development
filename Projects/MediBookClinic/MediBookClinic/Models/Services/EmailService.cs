using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Service;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace MediBookClinic.Models.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                // Here SmtpClient is a class from System
                using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
                {
                    EnableSsl = _emailSettings.EnableSsl,
                    Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password) // Here NetworkCredential is a class from System.Net
                };

                var mailMessage = new MailMessage // Here MailMessage is a class from System.Net.Mail
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
                _logger.LogInformation("Email sent successfully to {Email}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", toEmail);
                throw;
            }
        }

        public async Task SendWelcomeEmailAsync(string toEmail, string firstName, string lastName, string userType)
        {
            string subject = $"Welcome to MediBook Clinic, {firstName}!";
            string body = $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #4CAF50;'>Welcome to MediBook Clinic!</h2>
                        <p>Dear {firstName} {lastName},</p>
                        <p>Thank you for registering with MediBook Clinic as a {userType}.</p>
                        <p>Your account has been successfully created. You can now:</p>
                        <ul>
                            {(userType == "Doctor" ?
                                "<li>Manage your availability</li><li>View and manage appointments</li><li>Update your profile</li>" :
                                "<li>Book appointments with doctors</li><li>View your medical history</li><li>Rate and review doctors</li>")}
                        </ul>
                        <p style='margin-top: 30px; color: #666; font-size: 12px;'>
                            Don't forget to upload your profile picture from your profile page!
                        </p>
                    </div>
                </body>
                </html>";

            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendAdminNotificationAsync(string toEmail, string doctorName, string email,
            string phone, string specialization, string license, int experience, int doctorId)
        {
            var subject = "New Doctor Registration - Verification Required";
            var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #FF9800;'>New Doctor Registration</h2>
                        <p>A new doctor has registered and requires verification:</p>
                        <table style='border-collapse: collapse; width: 100%; margin: 20px 0;'>
                            <tr style='background-color: #f2f2f2;'>
                                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Name:</strong></td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{doctorName}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Email:</strong></td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{email}</td>
                            </tr>
                            <tr style='background-color: #f2f2f2;'>
                                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Phone:</strong></td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{phone}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Specialization:</strong></td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{specialization}</td>
                            </tr>
                            <tr style='background-color: #f2f2f2;'>
                                <td style='padding: 10px; border: 1px solid #ddd;'><strong>License:</strong></td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{license}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #ddd;'><strong>Experience:</strong></td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{experience} years</td>
                            </tr>
                        </table>
                    </div>
                </body>
                </html>";

            await SendEmailAsync(toEmail, subject, body);
        }
    }
}
