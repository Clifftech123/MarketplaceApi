namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record AuthResult
    {
        public required string AccessToken { get; init; }

        public required string RefreshToken { get; init; }

        public required DateTime AccessTokenExpiresAtUtc { get; init; }

        public required UserResponse User { get; init; }
    }
}
