namespace MarketplaceApi.src.Application.DTOs.Carts
{
    public sealed record CartResponse
    {
        public required Guid Id { get; init; }
        public required Guid CustomerId { get; init; }
        public required IReadOnlyCollection<CartItemResponse> Items { get; init; }
        public required decimal Total { get; init; }
    }
}
