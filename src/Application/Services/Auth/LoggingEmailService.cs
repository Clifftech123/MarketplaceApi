using MarketplaceApi.src.Application.Abstractions.Auth;
using Microsoft.Extensions.Logging;

namespace MarketplaceApi.src.Application.Services.Auth
{
    /// <summary>
    /// Placeholder until a real email provider (SMTP/SendGrid/etc.) is wired up.
    /// Logs the OTP instead of sending it — do not use in production.
    /// </summary>
    public class LoggingEmailService(ILogger<LoggingEmailService> logger) : IEmailService
    {
        public Task SendTwoFactorCodeAsync(string toEmail, string toName, string code, int expiryMinutes)
        {
            logger.LogWarning(
                "No email provider configured — OTP for {ToEmail} is {Code} (valid {ExpiryMinutes}m)",
                toEmail, code, expiryMinutes);
            return Task.CompletedTask;
        }
    }
}
