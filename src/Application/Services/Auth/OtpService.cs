using MarketplaceApi.src.Application.Abstractions.Auth;
using MarketplaceApi.src.Application.Abstractions.Common.Messages;
using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Application.Exceptions;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Domain.Enums;
using MarketplaceApi.src.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MarketplaceApi.src.Application.Services.Auth
{
    public class OtpService : IOtpService
    {
        private const int OtpExpiryMinutes = 5;
        private const int OtpRequestCooldownSeconds = 60;
        private const int MaxVerificationAttempts = 3;

        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        private readonly ILogger<OtpService> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public OtpService(
            UserManager<AppUser> userManager,
            ApplicationDbContext db,
            IMemoryCache cache,
            IEmailService emailService,
            ILogger<OtpService> logger,
            IJwtTokenService jwtTokenService,
            IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _db = db;
            _cache = cache;
            _emailService = emailService;
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
        }


        public async Task<OtpResponse> SendOtpAsync(SendOtpRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserIdentifier);

            // Small randomized delay to blunt timing-based user enumeration.
            await Task.Delay(Random.Shared.Next(150, 350));

            if (user is null || !user.IsActive)
            {
                _logger.LogWarning("OTP requested for unknown/inactive user {UserId}", request.UserIdentifier);
                throw new BusinessException(ErrorMessages.OtpRequestRejected);
            }

            var cooldownKey = CooldownKey(user.Id, request.Purpose);
            if (_cache.TryGetValue(cooldownKey, out _))
            {
                _logger.LogWarning("OTP cooldown active for user {UserId} purpose {Purpose}", user.Id, request.Purpose);
                throw new BusinessException(ErrorMessages.OtpCooldownActive);
            }

            await InvalidateExistingAsync(user.Id, request.Purpose);

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
            var validTo = DateTimeOffset.UtcNow.AddMinutes(OtpExpiryMinutes);

            _db.TwoFactorTokens.Add(new TwoFactorToken
            {
                UserIdentifier = user.Id.ToString(),
                Secret = user.SecurityStamp ?? string.Empty,
                Token = code,
                ValidTo = validTo,
                Purpose = request.Purpose.ToString()
            });

            await _db.SaveChangesAsync();
            _cache.Set(cooldownKey, true, TimeSpan.FromSeconds(OtpRequestCooldownSeconds));

            // Deliver the code. Token is persisted regardless of email failure.
            try
            {
                await _emailService.SendTwoFactorCodeAsync(
                    user.Email!, user.FullName ?? user.Email!, code, OtpExpiryMinutes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to deliver OTP email for user {UserId}", user.Id);
            }

            _logger.LogInformation("OTP issued for user {UserId} purpose {Purpose} (expires {ValidTo:o})",
                user.Id, request.Purpose, validTo);

            return new OtpResponse(user.Id.ToString(), validTo, OtpExpiryMinutes);
        }

        public async Task<bool> VerifyOtpAsync(VerifyOtpRequest request)
        {
            await VerifyOtpCoreAsync(request.UserIdentifier, request.Token, request.Purpose);
            return true;
        }

        public async Task<UserLoginResponse> LoginWithOtpAsync(LoginWithOtpRequest request)
        {
            var userId = await VerifyOtpCoreAsync(request.UserIdentifier, request.Code, OtpPurpose.Login);

            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new BusinessException(ErrorMessages.OtpInvalidOrExpired);

            if (!user.IsActive)
                throw new ForbiddenException(ErrorMessages.AccountDeactivated);

            var (accessToken, expiresAt) = _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            await _refreshTokenService.CreateAsync(user.Id, refreshToken);

            return new UserLoginResponse(accessToken, refreshToken, expiresAt);
        }

        public async Task<LoginStepOneResponse> ValidateCredentialsAndSendOtpAsync(LoginStepOneRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            // Always delay to prevent credential enumeration via timing.
            await Task.Delay(Random.Shared.Next(150, 350));

            if (user is null || !user.IsActive ||
                !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogWarning("Failed login attempt for email {Email}", request.Email);
                throw new UnauthorizedAccessException(ErrorMessages.InvalidCredentials);
            }

            var otpResult = await SendOtpAsync(new SendOtpRequest(user.Id.ToString(), OtpPurpose.Login));

            return new LoginStepOneResponse(user.Id.ToString(), otpResult.ExpiresAt, otpResult.ExpiresInMinutes);
        }

        /// <summary>
        /// Removes expired and used tokens. Call from a background/scheduled job.
        /// </summary>
        public async Task<int> CleanupExpiredTokensAsync(CancellationToken cancellationToken = default)
        {
            return await _db.TwoFactorTokens
                .Where(t => t.ValidTo <= DateTimeOffset.UtcNow || t.IsUsed)
                .ExecuteDeleteAsync(cancellationToken);
        }



        private async Task<Guid> VerifyOtpCoreAsync(
            string userIdentifier,
            string code,
            OtpPurpose purpose,
            CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(userIdentifier);
            ArgumentException.ThrowIfNullOrWhiteSpace(code);

            var purposeText = purpose.ToString();

            var token = await _db.TwoFactorTokens
                .FirstOrDefaultAsync(
                    t => t.UserIdentifier == userIdentifier
                      && t.Purpose == purposeText
                      && !t.IsUsed
                      && t.ValidTo > DateTimeOffset.UtcNow,
                    cancellationToken);

            if (token is null)
            {
                await Task.Delay(Random.Shared.Next(150, 350), cancellationToken);
                throw new BusinessException(ErrorMessages.OtpInvalidOrExpired);
            }

            var attemptKey = AttemptKey(token.Id);
            var attempts = (_cache.TryGetValue(attemptKey, out int existing) ? existing : 0) + 1;
            _cache.Set(attemptKey, attempts, token.ValidTo);

            if (attempts > MaxVerificationAttempts)
            {
                token.IsUsed = true;
                token.UsedAt = DateTimeOffset.UtcNow;
                await _db.SaveChangesAsync(cancellationToken);

                _logger.LogWarning("OTP verification attempts exceeded for token {TokenId}", token.Id);
                throw new BusinessException(ErrorMessages.OtpAttemptsExceeded);
            }

            if (!Guid.TryParse(userIdentifier, out var userId))
                throw new BusinessException(ErrorMessages.OtpInvalidOrExpired);

            var user = await _userManager.FindByIdAsync(userIdentifier);
            if (user is null || !user.IsActive)
                throw new BusinessException(ErrorMessages.OtpInvalidOrExpired);

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, TokenOptions.DefaultEmailProvider, code);

            if (!isValid)
            {
                _logger.LogWarning("OTP verification failed for user {UserId}", userId);
                throw new BusinessException(ErrorMessages.OtpInvalidOrExpired);
            }

            token.IsUsed = true;
            token.UsedAt = DateTimeOffset.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
            _cache.Remove(attemptKey);

            _logger.LogInformation("OTP verified for user {UserId} purpose {Purpose}", userId, purpose);
            return userId;
        }


        #region Helpers

        private async Task InvalidateExistingAsync(Guid userId, OtpPurpose purpose)
        {
            var purposeText = purpose.ToString();
            var userText = userId.ToString();
            var nowUtc = DateTimeOffset.UtcNow;

            await _db.TwoFactorTokens
                .Where(t => t.UserIdentifier == userText && t.Purpose == purposeText && !t.IsUsed)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(t => t.IsUsed, true)
                    .SetProperty(t => t.UsedAt, nowUtc));
        }

        private static string CooldownKey(Guid userId, OtpPurpose purpose) =>
            $"otp:cooldown:{userId}:{purpose}";

        private static string AttemptKey(string tokenId) =>
            $"otp:attempts:{tokenId}";

        #endregion
    }
}
