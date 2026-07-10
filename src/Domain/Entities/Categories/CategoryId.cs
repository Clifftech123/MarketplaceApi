using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Categories
{
    public record CategoryId(Guid Value) : EntityId(Value)
    {
        public static CategoryId New() => new(Guid.CreateVersion7());
    }
}
