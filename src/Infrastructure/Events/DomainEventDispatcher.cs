using MarketplaceApi.src.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace MarketplaceApi.src.Infrastructure.Events
{
    public class DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger) : IDomainEventDispatcher
    {
        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handleMethod = handlerType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))!;

                foreach (var handler in (IEnumerable<object>)serviceProvider.GetServices(handlerType))
                {
                    // The originating SaveChanges already committed by the time this runs
                    // (dispatch happens in SavedChangesAsync), so a handler failure must not
                    // propagate back to the caller as if the save itself had failed.
                    try
                    {
                        await (Task)handleMethod.Invoke(handler, [domainEvent, cancellationToken])!;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex,
                            "Domain event handler {HandlerType} failed handling {EventType}",
                            handler.GetType().Name, domainEvent.GetType().Name);
                    }
                }
            }
        }
    }
}
