using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;

namespace MarketplaceApi.src.Domain.Entities.Products.Entities
{
    public sealed record Image : Entity<ImageId>
    {
        public string Url { get; private set; } = string.Empty;
        public string? AltText { get; private set; }

        public required ProductId ProductId { get; init; }
        public Product? Product { get; init; }

        private Image() { }

        public static Image Create(string url, ProductId productId, string? altText = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Image URL is required.", nameof(url));

            return new Image
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
