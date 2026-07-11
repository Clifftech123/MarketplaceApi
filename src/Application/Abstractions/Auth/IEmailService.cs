namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface IEmailService
    {
        Task SendTwoFactorCodeAsync(string toEmail, string toName, string code, int expiryMinutes);
    }
}
