using MarketplaceApi.src.Domain.Common.Aggregates;
using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Payments.ValueObjects;
using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Domain.Entities.Payments.Entities
{
    public sealed record PaymentTransaction : AggregateRoot<PaymentTransactionId>, ISoftDeletable, IAuditableEntity
    {
        public required OrderId OrderId { get; init; }

        public string? StripePaymentIntentId { get; private set; }
        public string? StripeChargeId { get; private set; }
        public string? StripeInvoiceId { get; private set; }
        public string? StripeRefundId { get; private set; }
        public required string StripeTransactionId { get; init; }

        public int AmountPence { get; private set; }
        public string Currency { get; private set; } = "GBP";

        public PaymentStatus Status { get; private set; }
        public PaymentMethod Method { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public string? StripeResponse { get; private set; }
        public string? FailureReason { get; private set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }
        public string? DeletedBy { get; private set; }

        private PaymentTransaction() { }

        public static PaymentTransaction Create(
            OrderId orderId,
            string stripeTransactionId,
            int amountPence,
            string currency,
            PaymentMethod method,
            string? stripePaymentIntentId = null)
        {
            if (string.IsNullOrWhiteSpace(stripeTransactionId))
                throw new ArgumentException("Stripe transaction id is required.", nameof(stripeTransactionId));
            if (amountPence <= 0)
                throw new ArgumentException("Amount must be positive.", nameof(amountPence));

            return new PaymentTransaction
            {
                Id = PaymentTransactionId.New(),
                OrderId = orderId,
                StripeTransactionId = stripeTransactionId,
                StripePaymentIntentId = stripePaymentIntentId,
                AmountPence = amountPence,
                Currency = currency,
                Method = method,
                Status = PaymentStatus.Pending,
                TransactionDate = DateTime.UtcNow
            };
        }

        public void MarkSucceeded(string stripeChargeId, string? stripeResponse = null)
        {
            Status = PaymentStatus.Succeeded;
            StripeChargeId = stripeChargeId;
            StripeResponse = stripeResponse;
            FailureReason = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkFailed(string failureReason, string? stripeResponse = null)
        {
            Status = PaymentStatus.Failed;
            FailureReason = failureReason;
            StripeResponse = stripeResponse;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkRefunded(string stripeRefundId)
        {
            if (Status != PaymentStatus.Succeeded)
                throw new InvalidOperationException("Only a succeeded payment can be refunded.");

            Status = PaymentStatus.Refunded;
            StripeRefundId = stripeRefundId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete(string? deletedBy = null)
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedOnUtc = DateTime.UtcNow;
            DeletedBy = deletedBy;
        }

        public void Restore()
        {
            if (!IsDeleted)
                throw new InvalidOperationException("Payment transaction is not deleted.");

            IsDeleted = false;
            DeletedOnUtc = null;
            DeletedBy = null;
        }
    }
}
