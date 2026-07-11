namespace MarketplaceApi.src.Application.DTOs.Stores
{
    public sealed record CreateStoreRequest
    {
        public required string Name { get; init; }

        public string Description { get; init; } = string.Empty;

        public required Guid OwnerId { get; init; }
    }
}
