using MarketplaceApi.src.Application.Abstractions.Auth;
using MarketplaceApi.src.Application.Abstractions.Common.Messages;
using MarketplaceApi.src.Application.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.src.Presentation.Controllers.Auth
{
    public class AuthController(IAuthService authService, IOtpService otpService) : ApiControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.RegisterAsync(request, cancellationToken);
            return Success(result, SuccessMessages.RegistrationSuccessful);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.LoginAsync(request, cancellationToken);
            return Success(result, SuccessMessages.OtpSentToEmail);
        }

        [HttpPost("login/verify")]
        public async Task<IActionResult> VerifyLogin(LoginWithOtpRequest request)
        {
            var result = await otpService.LoginWithOtpAsync(request);
            return Success(result, SuccessMessages.LoginSuccessful);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.RefreshTokenAsync(request, cancellationToken);
            return Success(result);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            await authService.RevokeTokenAsync(request, cancellationToken);
            return Success(SuccessMessages.TokenRevoked);
        }
    }
}
