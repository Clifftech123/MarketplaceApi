using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Orders
{
    public sealed record ShippingAddressRequest
    {
        [Required, StringLength(200)]
        public required string Line1 { get; init; }

        [StringLength(200)]
        public string? Line2 { get; init; }

        [Required, StringLength(100)]
        public required string City { get; init; }

        [Required, StringLength(20)]
        public required string PostalCode { get; init; }

        [Required, StringLength(100)]
        public required string Country { get; init; }
    }
}
