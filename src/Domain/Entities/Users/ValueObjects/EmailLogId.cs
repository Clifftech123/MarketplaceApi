using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Users.ValueObjects
{
    public record EmailLogId(Guid Value) : EntityId(Value)
    {
        public static EmailLogId New() => new(Guid.CreateVersion7());
    }
}
