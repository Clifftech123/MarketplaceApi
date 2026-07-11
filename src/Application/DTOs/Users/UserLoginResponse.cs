namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record UserLoginResponse(string AccessToken, string RefreshToken, DateTime ExpiresAtUtc);
}
