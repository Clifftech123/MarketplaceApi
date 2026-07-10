using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Products.ValueObjects
{
    public record TagId(Guid Value) : EntityId(Value)
    {
        public static TagId New() => new(Guid.CreateVersion7());
    }
}
