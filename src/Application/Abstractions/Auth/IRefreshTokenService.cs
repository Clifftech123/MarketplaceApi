using MarketplaceApi.src.Domain.Entities.Users.Entities;

namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface IRefreshTokenService
    {

        Task<RefreshToken?> GetActiveTokenAsync(string rawToken, CancellationToken ct = default);


        Task<RefreshToken> CreateAsync(Guid userId, string rawToken, CancellationToken ct = default);

        Task<RefreshToken> RotateAsync(string oldRawToken, Guid userId, string newRawToken, CancellationToken ct = default);


        Task RevokeAllForUserAsync(Guid userId, CancellationToken ct = default);
    }
}
