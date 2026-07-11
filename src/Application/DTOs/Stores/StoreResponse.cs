namespace MarketplaceApi.src.Application.DTOs.Stores
{
    public sealed record StoreResponse
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required Guid OwnerId { get; init; }
    }
}
