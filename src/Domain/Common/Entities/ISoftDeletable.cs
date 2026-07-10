namespace MarketplaceApi.src.Domain.Common.Entities
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
        DateTime? DeletedOnUtc { get; }
        string? DeletedBy { get; }
    }
}
