using MarketplaceApi.src.Application.Middleware;

namespace MarketplaceApi.src.Application.Extensions
{
    public static class Configure
    {
        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
        }
    }
}
