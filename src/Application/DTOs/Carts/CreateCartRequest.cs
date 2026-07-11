namespace MarketplaceApi.src.Application.DTOs.Carts
{
    public sealed record CreateCartRequest
    {
        public required Guid CustomerId { get; init; }
    }
}
