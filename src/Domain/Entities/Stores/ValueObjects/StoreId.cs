using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Stores.ValueObjects
{
    public record StoreId(Guid Value) : EntityId(Value)
    {
        public static StoreId New() => new(Guid.CreateVersion7());
    }
}
