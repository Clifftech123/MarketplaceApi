namespace MarketplaceApi.src.Domain.Common.Entities
{
    public interface IAuditableEntity
    {
        string? CreatedBy { get; }
        string? UpdatedBy { get; }
    }
}
