using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Payments
{
    public sealed record PaymentTransactionResponse
    {
        public required Guid Id { get; init; }
        public required Guid OrderId { get; init; }
        public required string StripeTransactionId { get; init; }
        public string? StripePaymentIntentId { get; init; }
        public string? StripeChargeId { get; init; }
        public required int AmountPence { get; init; }
        public required string Currency { get; init; }
        public required PaymentStatus Status { get; init; }
        public required PaymentMethod Method { get; init; }
        public required DateTime TransactionDate { get; init; }
        public string? FailureReason { get; init; }
    }
}
