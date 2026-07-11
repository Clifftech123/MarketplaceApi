namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record LoginWithOtpRequest(string UserIdentifier, string Code);
}
