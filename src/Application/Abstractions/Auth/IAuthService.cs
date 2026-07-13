using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="request">The new user's account details.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The generated access token, refresh token and expiry.</returns>
        Task<AuthResult> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Validates a user's email and password, then sends a one-time password (OTP) to complete login.
        /// </summary>
        /// <param name="request">The user's email and password.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>Information indicating an OTP was sent for the second login step.</returns>
        Task<LoginStepOneResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Completes login by verifying the OTP sent during the first login step.
        /// </summary>
        /// <param name="request">The user identifier and the OTP code to verify.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The generated access token, refresh token and user details.</returns>
        Task<UserLoginResponse> VerifyLoginOtpAsync(LoginWithOtpRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirms a user's email address using the OTP sent after registration.
        /// </summary>
        /// <param name="request">The user identifier, OTP token and purpose (AccountConfirmation).</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        Task ConfirmEmailAsync(VerifyOtpRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Resends an OTP for the specified purpose (e.g. login or account confirmation).
        /// </summary>
        /// <param name="request">The user identifier and the OTP purpose.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>Information indicating a new OTP was sent.</returns>
        Task<OtpResponse> ResendOtpAsync(SendOtpRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Exchanges a valid refresh token for a new access token and refresh token.
        /// </summary>
        /// <param name="request">The current refresh token.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The newly generated access token, refresh token and expiry.</returns>
        Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Revokes the given refresh token and all other active refresh tokens for that user.
        /// </summary>
        /// <param name="request">The refresh token to revoke.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        Task RevokeTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the profile of the currently authenticated user.
        /// </summary>
        /// <returns>The authenticated user's details.</returns>
        Task<UserResponse> GetCurrentUserAsync();

        /// <summary>
        /// Changes the currently authenticated user's password and revokes all existing sessions.
        /// </summary>
        /// <param name="request">The current password and the new password.</param>
        /// <param name="ct">A token to cancel the operation.</param>
        Task ChangeUserPasswordAsync(ChangePasswordRequest request, CancellationToken ct = default);

        /// <summary>
        /// Deactivates the currently authenticated user's account.
        /// </summary>
        /// <param name="ct">A token to cancel the operation.</param>
        Task DeleteAccountAsync(CancellationToken ct = default);
    }
}
