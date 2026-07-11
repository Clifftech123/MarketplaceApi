namespace MarketplaceApi.src.Application.DTOs.Stores
{
    public sealed record UpdateStoreRequest
    {
        public required string Name { get; init; }

        public string Description { get; init; } = string.Empty;
    }
}
