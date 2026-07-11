using MarketplaceApi.src.Application.DTOs.Carts;
using MarketplaceApi.src.Domain.Entities.Carts.Entities;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;

namespace MarketplaceApi.src.Application.Mappings.Carts
{
    public static class CartMappings
    {
        public static CartResponse ToResponse(this Cart cart) => new()
        {
            Id = cart.Id.Value,
            CustomerId = cart.CustomerId,
            Items = [.. cart.Items.Select(ToResponse)],
            Total = cart.Total
        };

        public static CartItemResponse ToResponse(this CartItem item) => new()
        {
            Id = item.Id.Value,
            ProductId = item.ProductId.Value,
            ProductName = item.ProductName,
            UnitPrice = item.UnitPrice,
            Quantity = item.Quantity,
            LineTotal = item.UnitPrice * item.Quantity
        };

        public static Cart ToEntity(this CreateCartRequest request)
            => Cart.Create(request.CustomerId);

        public static void ApplyTo(this AddCartItemRequest request, Cart cart)
            => cart.AddItem(new ProductId(request.ProductId), request.ProductName, request.UnitPrice, request.Quantity);

        public static void ApplyTo(this UpdateCartItemQuantityRequest request, Cart cart)
            => cart.UpdateItemQuantity(new ProductId(request.ProductId), request.Quantity);
    }
}
