namespace MarketplaceApi.src.Application.DTOs.Orders
{
    public sealed record OrderLineResponse
    {
        public required Guid Id { get; init; }
        public required Guid ProductId { get; init; }
        public required string ProductName { get; init; }
        public required decimal UnitPrice { get; init; }
        public required int Quantity { get; init; }
        public required decimal LineTotal { get; init; }
    }
}
