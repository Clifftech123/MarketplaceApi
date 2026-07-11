using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Payments.Events;
using Microsoft.Extensions.Logging;

namespace MarketplaceApi.src.Application.EventHandlers.Payments
{
    public class PaymentSucceededLoggingHandler(ILogger<PaymentSucceededLoggingHandler> logger)
        : IDomainEventHandler<PaymentSucceededDomainEvent>
    {
        public Task HandleAsync(PaymentSucceededDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            logger.LogInformation(
                "Payment {PaymentTransactionId} succeeded for order {OrderId}",
                domainEvent.PaymentTransactionId, domainEvent.OrderId);
            return Task.CompletedTask;
        }
    }
}
