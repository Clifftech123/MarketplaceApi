namespace MarketplaceApi.src.Application.DTOs.Categories
{
    public sealed record UpdateCategoryRequest
    {
        public required string Name { get; init; }

        public string Description { get; init; } = string.Empty;
    }
}
