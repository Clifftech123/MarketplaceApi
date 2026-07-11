using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Domain.Entities.Users.Entities;

namespace MarketplaceApi.src.Application.Mappings.Users
{
    public static class UserMappings
    {
        public static UserResponse ToResponse(this AppUser user) => new()
        {
            Id = user.Id,

            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            IsActive = user.IsActive,
            NotifyOnOrderPlaced = user.NotifyOnOrderPlaced,
            NotifyOnOrderShipped = user.NotifyOnOrderShipped,
            NotifyOnPriceDrop = user.NotifyOnPriceDrop,
            NotifyWeeklySummary = user.NotifyWeeklySummary,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt
        };

        /// <summary>
        /// Builds an unsaved <see cref="AppUser"/> from the request. The password is intentionally
        /// left off the entity — pass request.Password to UserManager.CreateAsync(user, password)
        /// so Identity can hash it; AppUser never holds a raw password.
        /// </summary>
        public static AppUser ToEntity(this CreateUserRequest request) => new()
        {
            Email = request.Email,
            UserName = request.Email,
            FullName = request.FullName
        };
    }
}
