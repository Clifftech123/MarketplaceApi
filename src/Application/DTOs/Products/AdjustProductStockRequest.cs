namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record AdjustProductStockRequest
    {
        public required int Quantity { get; init; }
    }
}
