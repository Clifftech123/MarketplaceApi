namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record ProductResponse
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required decimal Price { get; init; }
        public required string Description { get; init; }
        public required Guid CategoryId { get; init; }
        public required Guid StoreId { get; init; }
        public required int StockQuantity { get; init; }
        public required IReadOnlyCollection<ProductTagResponse> Tags { get; init; }
        public required IReadOnlyCollection<ProductImageResponse> Images { get; init; }
    }
}
