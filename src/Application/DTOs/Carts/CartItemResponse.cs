namespace MarketplaceApi.src.Application.DTOs.Carts
{
    public sealed record CartItemResponse
    {
        public required Guid Id { get; init; }
        public required Guid ProductId { get; init; }
        public required string ProductName { get; init; }
        public required decimal UnitPrice { get; init; }
        public required int Quantity { get; init; }
        public required decimal LineTotal { get; init; }
    }
}
