using MarketplaceApi.src.Application.DTOs.Orders;
using MarketplaceApi.src.Domain.Entities.Orders.Entities;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;

namespace MarketplaceApi.src.Application.Mappings.Orders
{
    public static class OrderMappings
    {
        public static OrderResponse ToResponse(this Order order) => new()
        {
            Id = order.Id.Value,
            CustomerId = order.CustomerId,
            Status = order.Status,
            ShippingAddress = order.ShippingAddress.ToResponse(),
            Lines = [.. order.Lines.Select(ToResponse)],
            Total = order.Total
        };

        public static OrderLineResponse ToResponse(this OrderLine line) => new()
        {
            Id = line.Id.Value,
            ProductId = line.ProductId.Value,
            ProductName = line.ProductName,
            UnitPrice = line.UnitPrice,
            Quantity = line.Quantity,
            LineTotal = line.LineTotal
        };

        public static ShippingAddressResponse ToResponse(this ShippingAddress address) => new()
        {
            Line1 = address.Line1,
            Line2 = address.Line2,
            City = address.City,
            PostalCode = address.PostalCode,
            Country = address.Country
        };

        public static ShippingAddress ToValueObject(this ShippingAddressRequest request) => new()
        {
            Line1 = request.Line1,
            Line2 = request.Line2,
            City = request.City,
            PostalCode = request.PostalCode,
            Country = request.Country
        };

        public static Order ToEntity(this CreateOrderRequest request)
            => Order.Create(request.CustomerId, request.ShippingAddress.ToValueObject());

        public static void ApplyTo(this AddOrderLineRequest request, Order order)
            => order.AddLine(new ProductId(request.ProductId), request.ProductName, request.UnitPrice, request.Quantity);
    }
}
