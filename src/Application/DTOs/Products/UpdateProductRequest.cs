using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record UpdateProductRequest
    {
        [Required, StringLength(200)]
        public required string Name { get; init; }

        [Range(0, double.MaxValue)]
        public required decimal Price { get; init; }

        [StringLength(2000)]
        public string Description { get; init; } = string.Empty;
    }
}
