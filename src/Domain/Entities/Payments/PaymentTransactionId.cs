using MarketplaceApi.src.Domain.Common.Entities;

namespace MarketplaceApi.src.Domain.Entities.Payments
{
    public record PaymentTransactionId(Guid Value) : EntityId(Value)
    {
        public static PaymentTransactionId New() => new(Guid.CreateVersion7());
    }
}
