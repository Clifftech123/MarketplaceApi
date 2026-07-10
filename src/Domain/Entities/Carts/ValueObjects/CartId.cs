using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Carts.ValueObjects
{
    public record CartId(Guid Value) : EntityId(Value)
    {
        public static CartId New() => new(Guid.CreateVersion7());
    }
}
