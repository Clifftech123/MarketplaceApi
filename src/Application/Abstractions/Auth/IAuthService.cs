using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

        Task<LoginStepOneResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

        Task<UserLoginResponse> VerifyLoginOtpAsync(LoginWithOtpRequest request, CancellationToken cancellationToken = default);

        Task ConfirmEmailAsync(VerifyOtpRequest request, CancellationToken cancellationToken = default);

        Task<OtpResponse> ResendOtpAsync(SendOtpRequest request, CancellationToken cancellationToken = default);

        Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

        Task RevokeTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

        Task<UserResponse> GetCurrentUserAsync();
        Task ChangeUserPasswordAsync(ChangePasswordRequest request, CancellationToken ct = default);

        Task DeleteAccountAsync(CancellationToken ct = default);
    }
}
