using MarketplaceApi.src.Domain.Common.Aggregates;
using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Entities.Categories;
using MarketplaceApi.src.Domain.Entities.Stores;

namespace MarketplaceApi.src.Domain.Entities.Products
{
    public sealed record Product : AggregateRoot<ProductId>, ISoftDeletable
    {
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public required CategoryId CategoryId { get; init; }
        public required StoreId StoreId { get; init; }
        public int StockQuantity { get; private set; }

        private readonly List<Tag> _tags = [];
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        private readonly List<Image> _images = [];
        public IReadOnlyCollection<Image> Images => _images.AsReadOnly();

        public Category? Category { get; init; }
        public Store? Store { get; init; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }
        public string? DeletedBy { get; private set; }

        private Product() { }

        public static Product Create(string name, decimal price, string description, CategoryId categoryId, StoreId storeId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            return new Product
            {
                Id = ProductId.New(),
                Name = name,
                Price = price,
                Description = description,
                CategoryId = categoryId,
                StoreId = storeId
            };
        }

        public void UpdateDetails(string name, decimal price, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            Name = name;
            Price = price;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));

            StockQuantity += quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));
            if (quantity > StockQuantity)
                throw new InvalidOperationException("Cannot remove more stock than is available.");

            StockQuantity -= quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddTag(Tag tag)
        {
            if (_tags.Any(t => t.Name.Equals(tag.Name, StringComparison.OrdinalIgnoreCase)))
                return;

            _tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveTag(TagId tagId)
        {
            _tags.RemoveAll(t => t.Id == tagId);
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddImage(Image image)
        {
            _images.Add(image);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveImage(ImageId imageId)
        {
            _images.RemoveAll(i => i.Id == imageId);
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete(string? deletedBy = null)
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedOnUtc = DateTime.UtcNow;
            DeletedBy = deletedBy;
        }

        public void Restore()
        {
            if (!IsDeleted)
                throw new InvalidOperationException("Product is not deleted.");

            IsDeleted = false;
            DeletedOnUtc = null;
            DeletedBy = null;
        }
    }
}
