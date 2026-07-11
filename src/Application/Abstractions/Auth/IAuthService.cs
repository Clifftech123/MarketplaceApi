using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

        Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

        Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

        Task RevokeTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    }
}
