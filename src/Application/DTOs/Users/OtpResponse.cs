namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record OtpResponse(string UserIdentifier, DateTimeOffset ExpiresAt, int ExpiresInMinutes);
}
