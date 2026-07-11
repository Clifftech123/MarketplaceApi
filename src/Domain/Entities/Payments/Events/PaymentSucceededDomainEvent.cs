using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Payments.ValueObjects;

namespace MarketplaceApi.src.Domain.Entities.Payments.Events
{
    public sealed record PaymentSucceededDomainEvent(PaymentTransactionId PaymentTransactionId, OrderId OrderId) : IDomainEvent;
}
