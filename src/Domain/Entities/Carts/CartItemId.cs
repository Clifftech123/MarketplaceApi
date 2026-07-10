using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Carts
{
    public record CartItemId(Guid Value) : EntityId(Value)
    {
        public static CartItemId New() => new(Guid.CreateVersion7());
    }
}
