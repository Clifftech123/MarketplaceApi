namespace MarketplaceApi.src.Domain.Contracts
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
        DateTime? DeletedOnUtc { get; }
        string? DeletedBy { get; }

        void Delete(string? deletedBy = null);
    }
}
