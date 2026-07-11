using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Orders
{
    public sealed record OrderResponse
    {
        public required Guid Id { get; init; }
        public required Guid CustomerId { get; init; }
        public required OrderStatus Status { get; init; }
        public required ShippingAddressResponse ShippingAddress { get; init; }
        public required IReadOnlyCollection<OrderLineResponse> Lines { get; init; }
        public required decimal Total { get; init; }
    }
}
