using MarketplaceApi.src.Domain.Common.Aggregates;
using MarketplaceApi.src.Domain.Entities.Carts;
using MarketplaceApi.src.Domain.Entities.Products;
using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Domain.Entities.Orders
{
    public sealed record Order : AggregateRoot<OrderId>
    {
        public required Guid CustomerId { get; init; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public required ShippingAddress ShippingAddress { get; init; }

        private readonly List<OrderLine> _lines = [];
        public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();

        public decimal Total => _lines.Sum(l => l.LineTotal);

        private Order() { }

        public static Order Create(Guid customerId, ShippingAddress shippingAddress)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("Order must belong to a customer.", nameof(customerId));

            return new Order
            {
                Id = OrderId.New(),
                CustomerId = customerId,
                ShippingAddress = shippingAddress
            };
        }

        public static Order CreateFromCart(Cart cart, ShippingAddress shippingAddress)
        {
            if (cart.Items.Count == 0)
                throw new InvalidOperationException("Cannot create an order from an empty cart.");

            var order = Create(cart.CustomerId, shippingAddress);

            foreach (var item in cart.Items)
                order.AddLine(item.ProductId, item.ProductName, item.UnitPrice, item.Quantity);

            return order;
        }

        public void AddLine(ProductId productId, string productName, decimal unitPrice, int quantity)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot modify an order once it is no longer pending.");

            var existing = _lines.FirstOrDefault(l => l.ProductId == productId);
            if (existing is not null)
            {
                existing.UpdateQuantity(existing.Quantity + quantity);
                return;
            }

            _lines.Add(OrderLine.Create(productId, productName, unitPrice, quantity));
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkPaid()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Only a pending order can be marked as paid.");

            Status = OrderStatus.Paid;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkShipped()
        {
            if (Status != OrderStatus.Paid)
                throw new InvalidOperationException("Only a paid order can be shipped.");

            Status = OrderStatus.Shipped;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkDelivered()
        {
            if (Status != OrderStatus.Shipped)
                throw new InvalidOperationException("Only a shipped order can be delivered.");

            Status = OrderStatus.Delivered;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status is OrderStatus.Delivered or OrderStatus.Cancelled)
                throw new InvalidOperationException($"Cannot cancel an order that is already {Status}.");

            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
