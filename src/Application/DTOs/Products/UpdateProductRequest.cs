namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record UpdateProductRequest
    {
        public required string Name { get; init; }

        public required decimal Price { get; init; }

        public string Description { get; init; } = string.Empty;
    }
}
