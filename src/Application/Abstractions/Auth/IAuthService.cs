using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

        Task<LoginStepOneResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

        Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

        Task<UserResponse> GetCurrentUserAsync(string userId);



        Task ChangeUserPasswordAsync(Guid userId, ChangePasswordRequest request, CancellationToken ct = default);

        Task DeleteAccountAsync(Guid userId, CancellationToken ct = default);
    }
}
