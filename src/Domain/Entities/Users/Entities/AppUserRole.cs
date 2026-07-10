using Microsoft.AspNetCore.Identity;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public sealed class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUser? User { get; init; }
        public AppRole? Role { get; init; }
    }
}
