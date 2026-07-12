using MarketplaceApi.src.Domain.Entities.Users.Entities;
using System.Security.Claims;

namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface IJwtTokenService
    {
        (string accessToken, DateTime expiresAt) GenerateAccessToken(AppUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);


    }
}
