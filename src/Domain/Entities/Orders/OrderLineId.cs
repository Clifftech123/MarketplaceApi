using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Orders
{
    public record OrderLineId(Guid Value) : EntityId(Value)
    {
        public static OrderLineId New() => new(Guid.CreateVersion7());
    }
}
