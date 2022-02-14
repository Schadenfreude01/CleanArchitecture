using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArchitecture.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public EmailService(IOptions<EmailSetting> emailSetting, ILogger<EmailService> logger)
        {
            this.emailSetting = emailSetting.Value;
            this.logger = logger;
        }

        public EmailSetting emailSetting { get; }
        public ILogger<EmailService> logger { get; }

        public async Task<bool> SendEmail(Application.Models.Email email)
        {
            var client = new SendGridClient(emailSetting.ApiKey);
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var body = email.Body;
            var from = new EmailAddress
            {
                Email = emailSetting.FromAddress,
                Name = emailSetting.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await client.SendEmailAsync(sendGridMessage);

            if(response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            logger.LogError("El email no puedo enviarse, existen errores.");
            return false;
        }
    }
}
