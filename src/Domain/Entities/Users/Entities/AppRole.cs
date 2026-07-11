using MarketplaceApi.src.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public sealed class AppRole : IdentityRole<Guid>, IAuditableEntity
    {
        public ICollection<AppUserRole> UserRoles { get; } = [];

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
