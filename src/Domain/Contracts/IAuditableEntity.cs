namespace MarketplaceApi.src.Domain.Contracts
{
    public interface IAuditableEntity
    {
        string? CreatedBy { get; }
        string? UpdatedBy { get; }
    }
}
