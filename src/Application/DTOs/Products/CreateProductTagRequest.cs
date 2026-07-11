using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record CreateProductTagRequest
    {
        [Required, StringLength(50)]
        public required string Name { get; init; }

        [StringLength(500)]
        public string Description { get; init; } = string.Empty;
    }
}
