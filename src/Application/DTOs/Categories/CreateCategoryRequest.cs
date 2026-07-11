using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Categories
{
    public sealed record CreateCategoryRequest
    {
        [Required, StringLength(100)]
        public required string Name { get; init; }

        [StringLength(1000)]
        public string Description { get; init; } = string.Empty;
    }
}
