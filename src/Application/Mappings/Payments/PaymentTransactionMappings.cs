using MarketplaceApi.src.Application.DTOs.Payments;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Payments.Entities;

namespace MarketplaceApi.src.Application.Mappings.Payments
{
    public static class PaymentTransactionMappings
    {
        public static PaymentTransactionResponse ToResponse(this PaymentTransaction transaction) => new()
        {
            Id = transaction.Id.Value,
            OrderId = transaction.OrderId.Value,
            StripeTransactionId = transaction.StripeTransactionId,
            StripePaymentIntentId = transaction.StripePaymentIntentId,
            StripeChargeId = transaction.StripeChargeId,
            AmountPence = transaction.AmountPence,
            Currency = transaction.Currency,
            Status = transaction.Status,
            Method = transaction.Method,
            TransactionDate = transaction.TransactionDate,
            FailureReason = transaction.FailureReason
        };

        public static PaymentTransaction ToEntity(this CreatePaymentTransactionRequest request)
            => PaymentTransaction.Create(
                new OrderId(request.OrderId),
                request.StripeTransactionId,
                request.AmountPence,
                request.Currency,
                request.Method,
                request.StripePaymentIntentId);
    }
}
