using System.Security.Cryptography;
using System.Text;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public Guid UserId { get; private init; }
        public string TokenHash { get; private init; } = string.Empty;
        public DateTime ExpiresAt { get; private init; }
        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
        public bool IsRevoked { get; private set; }
        public DateTime? RevokedAt { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => !IsRevoked && !IsExpired;

        private RefreshToken() { } // EF Core

        public static RefreshToken Create(Guid userId, string rawToken, DateTime expiresAt) =>
            new()
            {
                UserId = userId,
                TokenHash = Hash(rawToken),
                ExpiresAt = expiresAt
            };

        public static string Hash(string rawToken)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }

        public void Revoke()
        {
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }
    }
}
