using MarketplaceApi.src.Application.Abstractions.Auth;
using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace MarketplaceApi.src.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private UserManager<AppUser> _userManager;

        public AuthService(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResult> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {

        }

        public Task RevokeTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
