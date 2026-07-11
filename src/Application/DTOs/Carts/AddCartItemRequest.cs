namespace MarketplaceApi.src.Application.DTOs.Carts
{
    public sealed record AddCartItemRequest
    {
        public required Guid ProductId { get; init; }

        public required string ProductName { get; init; }

        public required decimal UnitPrice { get; init; }

        public required int Quantity { get; init; }
    }
}
