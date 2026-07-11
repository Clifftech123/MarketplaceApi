namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record CreateProductTagRequest
    {
        public required string Name { get; init; }

        public string Description { get; init; } = string.Empty;
    }
}
