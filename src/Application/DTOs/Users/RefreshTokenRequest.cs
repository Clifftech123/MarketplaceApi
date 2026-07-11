namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record RefreshTokenRequest
    {
        public required string RefreshToken { get; init; }
    }
}
