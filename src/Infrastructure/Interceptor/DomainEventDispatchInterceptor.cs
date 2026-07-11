using MarketplaceApi.src.Domain.Contracts;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MarketplaceApi.src.Infrastructure.Interceptor
{
    public class DomainEventDispatchInterceptor(IDomainEventDispatcher dispatcher) : SaveChangesInterceptor
    {
        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null) return result;

            var aggregateRoots = eventData.Context.ChangeTracker.Entries<IAggregateRoot>()
                .Select(e => e.Entity)
                .Where(a => a.DomainEvents.Count > 0)
                .ToList();

            if (aggregateRoots.Count == 0) return result;

            var domainEvents = aggregateRoots.SelectMany(a => a.DomainEvents).ToList();

            foreach (var aggregateRoot in aggregateRoots)
                aggregateRoot.ClearDomainEvents();

            await dispatcher.DispatchAsync(domainEvents, cancellationToken);

            return result;
        }
    }
}
