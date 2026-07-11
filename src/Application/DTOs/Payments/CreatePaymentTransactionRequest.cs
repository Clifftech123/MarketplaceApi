using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Payments
{
    public sealed record CreatePaymentTransactionRequest
    {
        public required Guid OrderId { get; init; }

        public required string StripeTransactionId { get; init; }

        public string? StripePaymentIntentId { get; init; }

        public required int AmountPence { get; init; }

        public required string Currency { get; init; }

        public required PaymentMethod Method { get; init; }
    }
}
