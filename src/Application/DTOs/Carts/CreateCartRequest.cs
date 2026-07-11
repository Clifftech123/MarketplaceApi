using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Carts
{
    public sealed record CreateCartRequest
    {
        [Required]
        public required Guid CustomerId { get; init; }
    }
}
