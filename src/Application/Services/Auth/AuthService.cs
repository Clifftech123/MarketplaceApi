using MarketplaceApi.src.Application.Abstractions.Auth;
using MarketplaceApi.src.Application.Abstractions.Common.Messages;
using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Application.Exceptions;
using MarketplaceApi.src.Application.Mappings.Users;
using MarketplaceApi.src.Domain.Constants;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MarketplaceApi.src.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOtpService _otpService;
        private readonly IRefreshTokenService _refreshTokenService;

        private readonly ICurrentUserService _currentUserService;

        public AuthService(IJwtTokenService jwtTokenService, UserManager<AppUser> userManager, IOtpService otpService, IRefreshTokenService refreshTokenService, ICurrentUserService currentUserService)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _otpService = otpService;
            _refreshTokenService = refreshTokenService;
            _currentUserService = currentUserService;
        }

        public async Task ChangeUserPasswordAsync(Guid userId, ChangePasswordRequest request, CancellationToken ct = default)
        {
            var user = await GtUserByIdOrThrowAsync();
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                throw new BusinessException(result.Errors.FirstOrDefault()?.Description ?? ErrorMessages.PasswordChangeFailed);

            // Remove all existing refresh tokens for the user to ensure they must log in again with the new password
            await _refreshTokenService.RevokeAllForUserAsync(user.Id, ct);

        }


        public async Task DeleteAccountAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await GtUserByIdOrThrowAsync();

            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new BusinessException(string.Join(" ", result.Errors.Select(e => e.Description)));
        }

        public async Task<UserResponse> GetCurrentUserAsync(string userId)
        {
            var user = await GtUserByIdOrThrowAsync();
            return user.ToResponse();

        }

        public Task<LoginStepOneResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {

            return _otpService.ValidateCredentialsAndSendOtpAsync(
                new LoginStepOneRequest(request.Email, request.Password));
        }

        public async Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var existingToken = await _refreshTokenService.GetActiveTokenAsync(request.RefreshToken, cancellationToken)
                ?? throw new UnauthorizedAccessException(ErrorMessages.RefreshTokenInvalid);

            var user = await _userManager.FindByIdAsync(existingToken.UserId.ToString())
                ?? throw new UnauthorizedAccessException(ErrorMessages.RefreshTokenInvalid);

            if (!user.IsActive)
                throw new ForbiddenException(ErrorMessages.AccountDeactivated);

            var (accessToken, expiresAt) = _jwtTokenService.GenerateAccessToken(user);
            var newRawRefreshToken = _jwtTokenService.GenerateRefreshToken();
            await _refreshTokenService.RotateAsync(request.RefreshToken, user.Id, newRawRefreshToken, cancellationToken);

            return new AuthResult(accessToken, newRawRefreshToken, expiresAt);
        }

        public async Task<AuthResult> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            var existing = await _userManager.FindByEmailAsync(request.Email);
            if (existing is not null)
                throw new ConflictException(ErrorMessages.EmailAlreadyRegistered);

            var user = request.ToEntity();

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
                throw new BusinessException(string.Join(" ", createResult.Errors.Select(e => e.Description)));


            var role = request.AccountType switch
            {
                AccountType.Vendor => Roles.Vendor,
                _ => Roles.Customer
            };

            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
                throw new BusinessException(string.Join(" ", roleResult.Errors.Select(e => e.Description)));


            await _otpService.SendOtpAsync(new SendOtpRequest(user.Id.ToString(), OtpPurpose.AccountConfirmation));

            var (accessToken, expiresAt) = _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            await _refreshTokenService.CreateAsync(user.Id, refreshToken, cancellationToken);

            return new AuthResult(accessToken, refreshToken, expiresAt);
        }



        public async Task RevokeTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var existingToken = await _refreshTokenService.GetActiveTokenAsync(request.RefreshToken, cancellationToken)
                ?? throw new UnauthorizedAccessException(ErrorMessages.RefreshTokenInvalid);

            await _refreshTokenService.RevokeAllForUserAsync(existingToken.UserId, cancellationToken);
        }


        #region Private Methods

        private async Task<AppUser> GtUserByIdOrThrowAsync()
        {
            var userId = await _currentUserService.GetCurrentUserIdAsync()
                 ?? throw new UnauthorizedAccessException(ErrorMessages.UserNotAuthenticated);
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new NotFoundException(ErrorMessages.UserNotFound);
            if (!user.IsActive)
                throw new ForbiddenException(ErrorMessages.AccountDeactivated);
            return user;

        }

        #endregion


    }
}



