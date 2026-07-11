namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record LoginStepOneResponse(string UserIdentifier, DateTimeOffset ExpiresAt, int ExpiresInMinutes);
}
