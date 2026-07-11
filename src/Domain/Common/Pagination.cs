namespace MarketplaceApi.src.Domain.Common
{
    /// <summary>
    /// Carries skip/take values for paginated queries.
    /// </summary>
    public sealed record Pagination(int Skip, int Take);
}
