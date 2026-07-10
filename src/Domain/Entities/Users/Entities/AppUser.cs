using Microsoft.AspNetCore.Identity;
using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public UserRole Role { get; set; }

        public bool NotifyOnOrderPlaced { get; set; } = true;
        public bool NotifyOnOrderShipped { get; set; } = true;
        public bool NotifyOnPriceDrop { get; set; } = true;
        public bool NotifyWeeklySummary { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
    }
}
