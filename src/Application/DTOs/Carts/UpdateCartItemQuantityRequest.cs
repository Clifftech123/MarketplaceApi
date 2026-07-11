namespace MarketplaceApi.src.Application.DTOs.Carts
{
    public sealed record UpdateCartItemQuantityRequest
    {
        public required Guid ProductId { get; init; }

        public required int Quantity { get; init; }
    }
}
