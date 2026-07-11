using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Carts
{
    public sealed record UpdateCartItemQuantityRequest
    {
        [Required]
        public required Guid ProductId { get; init; }

        [Range(1, int.MaxValue)]
        public required int Quantity { get; init; }
    }
}
