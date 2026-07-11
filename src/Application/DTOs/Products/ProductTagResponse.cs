namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record ProductTagResponse
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
    }
}
