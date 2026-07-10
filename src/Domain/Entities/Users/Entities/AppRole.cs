using Microsoft.AspNetCore.Identity;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public sealed class AppRole : IdentityRole<Guid>
    {
        public ICollection<AppUserRole> UserRoles { get; } = [];
    }
}
