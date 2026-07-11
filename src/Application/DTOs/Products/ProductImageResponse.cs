namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record ProductImageResponse
    {
        public required Guid Id { get; init; }
        public required string Url { get; init; }
        public string? AltText { get; init; }
    }
}
