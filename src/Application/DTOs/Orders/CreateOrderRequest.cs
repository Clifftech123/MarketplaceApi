using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Orders
{
    public sealed record CreateOrderRequest
    {
        [Required]
        public required Guid CustomerId { get; init; }

        [Required]
        public required ShippingAddressRequest ShippingAddress { get; init; }
    }
}
