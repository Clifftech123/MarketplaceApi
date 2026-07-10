using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Products
{
    public record ProductId(Guid Value) : EntityId(Value)
    {
        public static ProductId New() => new(Guid.CreateVersion7());
    }
}
