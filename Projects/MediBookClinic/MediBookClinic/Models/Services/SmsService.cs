using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Service;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MediBookClinic.Models.Services
{
    public class SmsService : ISmsService
    {
        private readonly SmsSettings _smsSettings;
        private readonly ILogger<SmsService> _logger;

        public SmsService(IOptions<SmsSettings> smsSettings, ILogger<SmsService> logger) // Here IOptions is from Microsoft.Extensions.Options, ILogger is from Microsoft.Extensions.Logging
        {
            _smsSettings = smsSettings.Value;
            _logger = logger;

            // Initialize Twilio
            TwilioClient.Init(_smsSettings.AccountSid, _smsSettings.AuthToken);
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            try
            {
                var messageResource = await MessageResource.CreateAsync(
                    body: message,
                    from: new PhoneNumber(_smsSettings.FromPhoneNumber),
                    to: new PhoneNumber(phoneNumber)
                );

                _logger.LogInformation("SMS sent successfully to {Phone}. SID: {Sid}",
                    phoneNumber, messageResource.Sid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send SMS to {Phone}", phoneNumber);
                throw;
            }
        }

        public async Task SendWelcomeSmsAsync(string phoneNumber, string firstName)
        {
            var message = $"Welcome to MediBook Clinic, {firstName}! " +
                         $"Your account has been created successfully. " +
                         $"Complete your profile to get started.";

            await SendSmsAsync(phoneNumber, message);
        }
    }
}
