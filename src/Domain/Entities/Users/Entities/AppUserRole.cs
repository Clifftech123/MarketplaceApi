using MarketplaceApi.src.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public sealed class AppUserRole : IdentityUserRole<Guid>, IAuditableEntity
    {
        public AppUser? User { get; init; }
        public AppRole? Role { get; init; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
