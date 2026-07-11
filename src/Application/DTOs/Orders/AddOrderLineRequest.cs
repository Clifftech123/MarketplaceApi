namespace MarketplaceApi.src.Application.DTOs.Orders
{
    public sealed record AddOrderLineRequest
    {
        public required Guid ProductId { get; init; }

        public required string ProductName { get; init; }

        public required decimal UnitPrice { get; init; }

        public required int Quantity { get; init; }
    }
}
