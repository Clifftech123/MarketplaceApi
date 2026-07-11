namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record AuthResult(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAtUtc);
}
