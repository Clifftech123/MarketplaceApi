namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record CreateProductImageRequest
    {
        public required string Url { get; init; }

        public string? AltText { get; init; }
    }
}
