using MarketplaceApi.src.Domain.Common.Aggregates;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Carts.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;

namespace MarketplaceApi.src.Domain.Entities.Carts.Entities
{
    public sealed record Cart : AggregateRoot<CartId>, IAuditableEntity
    {
        public required Guid CustomerId { get; init; }

        private readonly List<CartItem> _items = [];
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        public decimal Total => _items.Sum(i => i.UnitPrice * i.Quantity);

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        private Cart() { }

        public static Cart Create(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("Cart must belong to a customer.", nameof(customerId));

            return new Cart
            {
                Id = CartId.New(),
                CustomerId = customerId
            };
        }

        public void AddItem(ProductId productId, string productName, decimal unitPrice, int quantity)
        {
            var existing = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existing is not null)
            {
                existing.UpdateQuantity(existing.Quantity + quantity);
            }
            else
            {
                _items.Add(CartItem.Create(productId, productName, unitPrice, quantity));
            }

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveItem(ProductId productId)
        {
            _items.RemoveAll(i => i.ProductId == productId);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateItemQuantity(ProductId productId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId)
                ?? throw new InvalidOperationException("Item not found in cart.");

            item.UpdateQuantity(quantity);
            UpdatedAt = DateTime.UtcNow;
        }

        public void Clear()
        {
            _items.Clear();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
