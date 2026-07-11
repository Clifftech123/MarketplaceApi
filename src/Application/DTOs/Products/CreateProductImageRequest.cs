using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record CreateProductImageRequest
    {
        [Required, Url, StringLength(2000)]
        public required string Url { get; init; }

        [StringLength(300)]
        public string? AltText { get; init; }
    }
}
