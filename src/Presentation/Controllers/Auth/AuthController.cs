using MarketplaceApi.src.Application.Abstractions.Auth;
using MarketplaceApi.src.Application.Abstractions.Common;
using MarketplaceApi.src.Application.Abstractions.Common.Messages;
using MarketplaceApi.src.Application.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.src.Presentation.Controllers.Auth
{
    public class AuthController(IAuthService authService) : ApiControllerBase
    {
        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="request">The new user's account details.</param>
        /// <returns>The generated access token, refresh token and expiry.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<AuthResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.RegisterAsync(request, cancellationToken);
            return Success(result, SuccessMessages.RegistrationSuccessful);
        }

        /// <summary>
        /// Validates a user's email and password, then sends a one-time password (OTP) to complete login.
        /// </summary>
        /// <param name="request">The user's email and password.</param>
        /// <returns>Information indicating an OTP was sent for the second login step.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginStepOneResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.LoginAsync(request, cancellationToken);
            return Success(result, SuccessMessages.OtpSentToEmail);
        }

        /// <summary>
        /// Completes login by verifying the OTP sent during the first login step.
        /// </summary>
        /// <param name="request">The user identifier and the OTP code to verify.</param>
        /// <returns>The generated access token, refresh token and user details.</returns>
        [HttpPost("login/verify")]
        [ProducesResponseType(typeof(ApiResponse<UserLoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> VerifyLogin(LoginWithOtpRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.VerifyLoginOtpAsync(request, cancellationToken);
            return Success(result, SuccessMessages.LoginSuccessful);
        }

        /// <summary>
        /// Confirms a user's email address using the OTP sent after registration.
        /// </summary>
        /// <param name="request">The user identifier, OTP token and purpose (AccountConfirmation).</param>
        /// <returns>A confirmation message.</returns>
        [HttpPost("confirm-email")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail(VerifyOtpRequest request, CancellationToken cancellationToken)
        {
            await authService.ConfirmEmailAsync(request, cancellationToken);
            return Success(SuccessMessages.EmailConfirmed);
        }

        /// <summary>
        /// Resends an OTP for the specified purpose (e.g. login or account confirmation).
        /// </summary>
        /// <param name="request">The user identifier and the OTP purpose.</param>
        /// <returns>Information indicating a new OTP was sent.</returns>
        [HttpPost("otp/resend")]
        [ProducesResponseType(typeof(ApiResponse<OtpResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResendOtp(SendOtpRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.ResendOtpAsync(request, cancellationToken);
            return Success(result, SuccessMessages.OtpSentToEmail);
        }

        /// <summary>
        /// Exchanges a valid refresh token for a new access token and refresh token.
        /// </summary>
        /// <param name="request">The current refresh token.</param>
        /// <returns>The newly generated access token, refresh token and expiry.</returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(ApiResponse<AuthResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.RefreshTokenAsync(request, cancellationToken);
            return Success(result);
        }

        /// <summary>
        /// Revokes the given refresh token and all other active refresh tokens for that user.
        /// </summary>
        /// <param name="request">The refresh token to revoke.</param>
        /// <returns>A confirmation message.</returns>
        [HttpPost("revoke")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Revoke(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            await authService.RevokeTokenAsync(request, cancellationToken);
            return Success(SuccessMessages.TokenRevoked);
        }

        /// <summary>
        /// Gets the profile of the currently authenticated user.
        /// </summary>
        /// <returns>The authenticated user's details.</returns>
        [HttpPost("me")]
        [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
        {
            var result = await authService.GetCurrentUserAsync();
            return Success(result);
        }

        /// <summary>
        /// Deactivates the currently authenticated user's account.
        /// </summary>
        /// <returns>A confirmation message.</returns>
        [HttpDelete("me")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCurrentUser(CancellationToken cancellationToken)
        {
            await authService.DeleteAccountAsync();
            return Success(SuccessMessages.UserDeleted);
        }

        /// <summary>
        /// Changes the currently authenticated user's password and revokes all existing sessions.
        /// </summary>
        /// <param name="request">The current password and the new password.</param>
        /// <returns>A confirmation message.</returns>
        [HttpPost("change-password")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            await authService.ChangeUserPasswordAsync(request, cancellationToken);
            return Success(SuccessMessages.PasswordChanged);
        }
    }
}
