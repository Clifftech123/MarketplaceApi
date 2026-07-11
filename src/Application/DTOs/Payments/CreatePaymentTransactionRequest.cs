using MarketplaceApi.src.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Payments
{
    public sealed record CreatePaymentTransactionRequest
    {
        [Required]
        public required Guid OrderId { get; init; }

        [Required, StringLength(255)]
        public required string StripeTransactionId { get; init; }

        [StringLength(255)]
        public string? StripePaymentIntentId { get; init; }

        [Range(1, int.MaxValue)]
        public required int AmountPence { get; init; }

        [Required, StringLength(3, MinimumLength = 3)]
        public required string Currency { get; init; }

        [Required]
        public required PaymentMethod Method { get; init; }
    }
}
