using MarketplaceApi.src.Application.Abstractions.Email;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Domain.Entities.Users.ValueObjects;
using MarketplaceApi.src.Domain.Enums;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace MarketplaceApi.src.Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly IRepository<EmailLog, EmailLogId> _repository;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<EmailOptions> emailOptions,
            IRepository<EmailLog, EmailLogId> repository,
            ILogger<EmailService> logger)
        {
            _emailOptions = emailOptions.Value;
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(
            string toEmail,
            string toName,
            string subject,
            string htmlBody,
            CancellationToken cancellationToken = default)
        {
            var emailLog = EmailLog.Create(toEmail, subject, EmailType.Welcome);

            try
            {
                using var message = new MailMessage
                {
                    From = new MailAddress(_emailOptions.SenderEmail, _emailOptions.SenderName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                };
                message.To.Add(new MailAddress(toEmail, toName));

                using var client = new SmtpClient(_emailOptions.SmtpHost, _emailOptions.SmtpPort)
                {
                    EnableSsl = _emailOptions.EnableSsl,
                    Credentials = new NetworkCredential(_emailOptions.SmtpUsername, _emailOptions.SmtpPassword)
                };

                await client.SendMailAsync(message, cancellationToken);

                emailLog.MarkSent();
                await _repository.AddAsync(emailLog, cancellationToken);

                _logger.LogInformation("Email sent to {ToEmail} with subject {Subject}", toEmail, subject);
                return true;
            }
            catch (Exception ex)
            {
                emailLog.MarkFailed(ex.Message);
                await _repository.AddAsync(emailLog, cancellationToken);

                _logger.LogError(ex, "Failed to send email to {ToEmail} with subject {Subject}", toEmail, subject);
                return false;
            }
        }
    }
}