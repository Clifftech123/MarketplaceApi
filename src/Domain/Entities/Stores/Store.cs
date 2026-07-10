using MarketplaceApi.src.Domain.Common.Aggregates;
using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Entities.Products;
using MarketplaceApi.src.Domain.Entities.Users;

namespace MarketplaceApi.src.Domain.Entities.Stores
{
    public sealed record Store : AggregateRoot<StoreId>, ISoftDeletable
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public required Guid OwnerId { get; init; }
        public AppUser? Owner { get; init; }

        private readonly List<Product> _products = [];
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }
        public string? DeletedBy { get; private set; }

        private Store() { }

        public static Store Create(string name, string description, Guid ownerId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Store name is required.", nameof(name));
            if (ownerId == Guid.Empty)
                throw new ArgumentException("Store must have an owner.", nameof(ownerId));

            return new Store
            {
                Id = StoreId.New(),
                Name = name,
                Description = description,
                OwnerId = ownerId
            };
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Store name is required.", nameof(name));

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
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
                throw new InvalidOperationException("Store is not deleted.");

            IsDeleted = false;
            DeletedOnUtc = null;
            DeletedBy = null;
        }
    }
}
