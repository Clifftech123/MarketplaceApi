using MarketplaceApi.src.Application.Abstractions.Auth;
using MarketplaceApi.src.Application.Options;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MarketplaceApi.src.Application.Services.Auth
{
    public class RefreshTokenService(ApplicationDbContext dbContext, IOptions<JwtOptions> options) : IRefreshTokenService
    {
        private readonly JwtOptions jwtOptions = options.Value;

        public async Task<RefreshToken> CreateAsync(Guid userId, string rawToken, CancellationToken ct = default)
        {
            var entity = RefreshToken.Create(userId, rawToken, DateTime.UtcNow.AddDays(jwtOptions.RefreshTokenExpiryDays));
            await dbContext.RefreshTokens.AddAsync(entity, ct);
            await dbContext.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<RefreshToken?> GetActiveTokenAsync(string rawToken, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(rawToken)) return null;

            var hashedToken = RefreshToken.Hash(rawToken);
            var now = DateTime.UtcNow;

            return await dbContext.RefreshTokens
                  .FirstOrDefaultAsync(
                      r => r.TokenHash == hashedToken && !r.IsRevoked && r.ExpiresAt > now,
                      ct);
        }

        public async Task RevokeAllForUserAsync(Guid userId, CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;
            var tokens = await dbContext.RefreshTokens
                  .Where(r => r.UserId == userId && !r.IsRevoked && r.ExpiresAt > now)
                  .ToListAsync(ct);

            foreach (var token in tokens)
                token.Revoke();

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<RefreshToken> RotateAsync(string oldRawToken, Guid userId, string newRawToken, CancellationToken ct = default)
        {
            var existing = await GetActiveTokenAsync(oldRawToken, ct)
                  ?? throw new InvalidOperationException("Refresh token is invalid or expired.");

            if (existing.UserId != userId)
                throw new InvalidOperationException("Refresh token does not belong to the specified user.");

            existing.Revoke();

            var replacement = RefreshToken.Create(
                userId,
                newRawToken,
                DateTime.UtcNow.AddDays(jwtOptions.RefreshTokenExpiryDays));

            await dbContext.RefreshTokens.AddAsync(replacement, ct);
            await dbContext.SaveChangesAsync(ct);
            return replacement;
        }
    }
}
