using FluentValidation;
using MarketplaceApi.src.Application.Filters;
using MarketplaceApi.src.Application.Middleware;
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
        }
    }
}
