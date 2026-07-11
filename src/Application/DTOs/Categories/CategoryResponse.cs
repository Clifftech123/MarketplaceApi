namespace MarketplaceApi.src.Application.DTOs.Categories
{
    public sealed record CategoryResponse
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
    }
}
