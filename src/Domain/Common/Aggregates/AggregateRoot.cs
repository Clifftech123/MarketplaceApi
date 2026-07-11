using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Contracts;

namespace MarketplaceApi.src.Domain.Common.Aggregates
{
    public abstract record AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot where TEntityId : EntityId
    {

        public List<IDomainEvent> DomainEvents { get; } = [];


        // Add Domain event
        public void RaseDomainEvent<TDomainEvent>(IDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent => DomainEvents.Add(domainEvent);

        // Remove Domain event 

        public void RemoveDomainEvent<TDomainEvent>(IDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent => DomainEvents.Remove(domainEvent);


        // Clear Domain events
        public void ClearDomainEvents() => DomainEvents.Clear();


    }
}
