using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Orders
{
    public record OrderId(Guid Value) : EntityId(Value)
    {
        public static OrderId New() => new(Guid.CreateVersion7());
    }
}
