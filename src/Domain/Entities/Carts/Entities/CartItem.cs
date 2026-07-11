using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Carts.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;

namespace MarketplaceApi.src.Domain.Entities.Carts.Entities
{
    public sealed record CartItem : Entity<CartItemId>, IAuditableEntity
    {
        public required ProductId ProductId { get; init; }
        public required string ProductName { get; init; }
        public required decimal UnitPrice { get; init; }
        public int Quantity { get; private set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        private CartItem() { }

        public static CartItem Create(ProductId productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));

            return new CartItem
            {
                Id = CartItemId.New(),
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity
            };
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));

            Quantity = quantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
