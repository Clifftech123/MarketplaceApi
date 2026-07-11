using MarketplaceApi.src.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public class AppUser : IdentityUser<Guid>, IAuditableEntity
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<AppUserRole> UserRoles { get; } = [];

        public bool NotifyOnOrderPlaced { get; set; } = true;
        public bool NotifyOnOrderShipped { get; set; } = true;
        public bool NotifyOnPriceDrop { get; set; } = true;
        public bool NotifyWeeklySummary { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
