using MarketplaceApi.src.Application.Abstractions.Auth;
using MarketplaceApi.src.Application.Options;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MarketplaceApi.src.Application.Services.Auth
{
    public class JwtTokenService : IJwtTokenService
    {
        private const int MinKeyLengthBytes = 32; // 256 bits, minimum for HMAC-SHA256

        private readonly JwtOptions _jwtOptions;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public JwtTokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;

            var keyBytes = Encoding.UTF8.GetBytes(_jwtOptions.Secret ?? string.Empty);
            if (keyBytes.Length < MinKeyLengthBytes)
                throw new ArgumentException($"The JWT signing key must be at least {MinKeyLengthBytes} bytes long.");

            _symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
        }

        public (string accessToken, DateTime expiresAt) GenerateAccessToken(AppUser user)
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiryMinutes);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }

        public string GenerateRefreshToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
            => ValidateTokenCore(token, validateLifetime: false);

        private ClaimsPrincipal ValidateTokenCore(string token, bool validateLifetime)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token cannot be null or empty.", nameof(token));

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                throw new SecurityTokenException("Invalid token format.");

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _symmetricSecurityKey,
                ValidateLifetime = validateLifetime,
                ClockSkew = TimeSpan.FromSeconds(30),
                ValidAlgorithms = [SecurityAlgorithms.HmacSha256] // prevent alg substitution attacks
            };

            var principal = handler.ValidateToken(token, parameters, out var validatedToken);
            EnsureHmacSha256(validatedToken);
            return principal;
        }

        private static void EnsureHmacSha256(SecurityToken validatedToken)
        {
            if (validatedToken is not JwtSecurityToken jwt ||
                !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase) ||
                jwt.SigningKey is null)
                throw new SecurityTokenException("Invalid token algorithm.");
        }

    }
}

