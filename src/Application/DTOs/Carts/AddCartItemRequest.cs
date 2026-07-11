using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Carts
{
    public sealed record AddCartItemRequest
    {
        [Required]
        public required Guid ProductId { get; init; }

        [Required, StringLength(200)]
        public required string ProductName { get; init; }

        [Range(0, double.MaxValue)]
        public required decimal UnitPrice { get; init; }

        [Range(1, int.MaxValue)]
        public required int Quantity { get; init; }
    }
}
