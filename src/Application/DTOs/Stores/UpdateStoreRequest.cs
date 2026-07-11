using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Stores
{
    public sealed record UpdateStoreRequest
    {
        [Required, StringLength(150)]
        public required string Name { get; init; }

        [StringLength(1000)]
        public string Description { get; init; } = string.Empty;
    }
}
