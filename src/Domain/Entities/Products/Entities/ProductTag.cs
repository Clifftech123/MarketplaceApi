using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;

namespace MarketplaceApi.src.Domain.Entities.Products.Entities
{
    public sealed record ProductTag : Entity<TagId>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public required ProductId ProductId { get; init; }
        public Product? Product { get; init; }

        private ProductTag() { }

        public static ProductTag Create(string name, string description, ProductId productId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name is required.", nameof(name));

            return new ProductTag
            {
                Id = TagId.New(),
                Name = name,
                Description = description,
                ProductId = productId
            };
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name is required.", nameof(name));

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
