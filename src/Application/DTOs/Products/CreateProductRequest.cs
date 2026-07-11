namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record CreateProductRequest
    {
        public required string Name { get; init; }

        public required decimal Price { get; init; }

        public string Description { get; init; } = string.Empty;

        public required Guid CategoryId { get; init; }

        public required Guid StoreId { get; init; }
    }
}
