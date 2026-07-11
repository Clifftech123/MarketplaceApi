namespace MarketplaceApi.src.Application.DTOs.Categories
{
    public sealed record CreateCategoryRequest
    {
        public required string Name { get; init; }

        public string Description { get; init; } = string.Empty;
    }
}
