using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Products
{
    public record ImageId(Guid Value) : EntityId(Value)
    {
        public static ImageId New() => new(Guid.CreateVersion7());
    }
}
