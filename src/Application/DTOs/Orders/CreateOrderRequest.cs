namespace MarketplaceApi.src.Application.DTOs.Orders
{
    public sealed record CreateOrderRequest
    {
        public required Guid CustomerId { get; init; }

        public required ShippingAddressRequest ShippingAddress { get; init; }
    }
}
