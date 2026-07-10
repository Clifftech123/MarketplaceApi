using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;

namespace MarketplaceApi.src.Domain.Entities.Orders.Entities
{
    public sealed record OrderLine : Entity<OrderLineId>
    {
        public required ProductId ProductId { get; init; }
        public required string ProductName { get; init; }
        public required decimal UnitPrice { get; init; }
        public int Quantity { get; private set; }

        public decimal LineTotal => UnitPrice * Quantity;

        private OrderLine() { }

        public static OrderLine Create(ProductId productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));
            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

            return new OrderLine
            {
                Id = OrderLineId.New(),
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
