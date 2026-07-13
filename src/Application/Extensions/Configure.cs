using FluentValidation;
using MarketplaceApi.src.Application.Abstractions.Auth;
using MarketplaceApi.src.Application.Abstractions.Email;
using MarketplaceApi.src.Application.Filters;
using MarketplaceApi.src.Application.Middleware;
using MarketplaceApi.src.Application.Services.Auth;
using MarketplaceApi.src.Application.Services.Email;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.src.Application.Extensions
{
    public static class Configure
    {
        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            // Scans this assembly for AbstractValidator<T> implementations
            builder.Services.AddValidatorsFromAssembly(typeof(Configure).Assembly);

            // Runs any registered validator against action arguments before the action executes
            builder.Services.Configure<MvcOptions>(options => options.Filters.Add<ValidationFilter>());

            // Auth services
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddScoped<IOtpService, OtpService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Placeholder until a real email provider is wired up — logs the OTP instead of sending it
            builder.Services.AddScoped<IEmailService, LoggingEmailService>();

            builder.Services.AddScoped<IEmailLogService, EmailLogService>();
        }
    }
}
