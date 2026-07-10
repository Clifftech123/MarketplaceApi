using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Products
{
    public sealed record Tag : Entity<TagId>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public required ProductId ProductId { get; init; }
        public Product? Product { get; init; }

        private Tag() { }

        public static Tag Create(string name, string description, ProductId productId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name is required.", nameof(name));

            return new Tag
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
