namespace MarketplaceApi.src.Domain.Common.Entities
{
    public record class EntityId(Guid Value)
    {
        public virtual bool Equals(EntityId? other) => Value.Equals(other?.Value);
        public override int GetHashCode() => Value.GetHashCode();

    }
}
