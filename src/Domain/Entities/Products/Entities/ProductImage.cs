using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;

namespace MarketplaceApi.src.Domain.Entities.Products.Entities
{
    public sealed record ProductImage : Entity<ImageId>, IAuditableEntity
    {
        public string Url { get; private set; } = string.Empty;
        public string? AltText { get; private set; }

        public required ProductId ProductId { get; init; }
        public Product? Product { get; init; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        private ProductImage() { }

        public static ProductImage Create(string url, ProductId productId, string? altText = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Image URL is required.", nameof(url));

            return new ProductImage
            {
                Id = ImageId.New(),
                Url = url,
                AltText = altText,
                ProductId = productId
            };
        }

        public void UpdateAltText(string? altText)
        {
            AltText = altText;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
