namespace MarketplaceApi.src.Application.Abstractions.Email
{
    public interface IEmailService
    {
        /// <summary>Sends an email. Caller is responsible for building subject/body content.</summary>
        Task<bool> SendEmailAsync(
            string toEmail,
            string toName,
            string subject,
            string htmlBody,
            CancellationToken cancellationToken = default);
    }
}