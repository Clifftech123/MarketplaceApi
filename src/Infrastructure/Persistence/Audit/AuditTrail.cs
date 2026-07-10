using MarketplaceApi.src.Domain.Entities.Users;

namespace MarketplaceApi.src.Infrastructure.Persistence.Audit
{
    public sealed class AuditTrail
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid? UserId { get; init; }
        public AppUser? User { get; init; }

        public TrailType TrailType { get; init; }
        public DateTime DateUtc { get; init; } = DateTime.UtcNow;

        public required string EntityName { get; init; }
        public string? PrimaryKey { get; init; }

        public Dictionary<string, object?> OldValues { get; init; } = [];
        public Dictionary<string, object?> NewValues { get; init; } = [];
        public List<string> ChangedColumns { get; init; } = [];
    }
}
