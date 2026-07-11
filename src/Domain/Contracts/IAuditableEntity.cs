namespace MarketplaceApi.src.Domain.Contracts
{
    public interface IAuditableEntity
    {
        string? CreatedBy { get; set; }
        string? UpdatedBy { get; set; }
    }
}
