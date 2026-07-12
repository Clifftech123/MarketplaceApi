using MarketplaceApi.src.Application.Abstractions.Auth;
using System.Security.Claims;

namespace MarketplaceApi.src.Application.Services.Auth
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public Task<string?> GetCurrentUserIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId != null ? Task.FromResult<string?>(userId) : Task.FromResult<string?>(null);

        }


    }
}
