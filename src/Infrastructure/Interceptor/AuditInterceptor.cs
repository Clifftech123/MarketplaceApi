using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Infrastructure.Persistence.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace MarketplaceApi.src.Infrastructure.Interceptor
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private const string RedactedValue = "[REDACTED]";

        private static readonly HashSet<string> SensitiveProperties = new(StringComparer.OrdinalIgnoreCase)
        {
            "PasswordHash",
            "SecurityStamp",
            "ConcurrentStamp"
        };

        private readonly IHttpContextAccessor? _httpContextAccessor;

        public AuditInterceptor(IHttpContextAccessor? httpContextAccessor = null)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null) return ValueTask.FromResult(result);

            var user = _httpContextAccessor?.HttpContext?.User;
            var userName = user?.Identity?.Name ?? "System";
            var userId = Guid.TryParse(user?.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : (Guid?)null;
            var auditTrails = new List<AuditTrail>();

            foreach (var entry in eventData.Context.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted))
                    continue;

                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedBy = userName;
                else
                    entry.Entity.UpdatedBy = userName;

                auditTrails.Add(BuildAuditTrail(entry, userId));
            }

            if (auditTrails.Count > 0)
                eventData.Context.Set<AuditTrail>().AddRange(auditTrails);

            return ValueTask.FromResult(result);
        }

        private static AuditTrail BuildAuditTrail(EntityEntry entry, Guid? userId)
        {
            var trailType = entry.State switch
            {
                EntityState.Added => TrailType.Create,
                EntityState.Deleted => TrailType.Delete,
                _ => TrailType.Update
            };

            var oldValues = new Dictionary<string, object?>();
            var newValues = new Dictionary<string, object?>();
            var changedColumns = new List<string>();
            string? primaryKey = null;

            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;

                if (property.Metadata.IsPrimaryKey())
                {
                    primaryKey = property.CurrentValue?.ToString();
                    continue;
                }

                var isSensitive = SensitiveProperties.Contains(propertyName);

                switch (trailType)
                {
                    case TrailType.Create:
                        newValues[propertyName] = isSensitive ? RedactedValue : property.CurrentValue;
                        break;
                    case TrailType.Delete:
                        oldValues[propertyName] = isSensitive ? RedactedValue : property.OriginalValue;
                        break;
                    case TrailType.Update:
                        if (!property.IsModified) continue;
                        oldValues[propertyName] = isSensitive ? RedactedValue : property.OriginalValue;
                        newValues[propertyName] = isSensitive ? RedactedValue : property.CurrentValue;
                        changedColumns.Add(propertyName);
                        break;
                }
            }

            return new AuditTrail
            {
                UserId = userId,
                TrailType = trailType,
                EntityName = entry.Entity.GetType().Name,
                PrimaryKey = primaryKey,
                OldValues = oldValues,
                NewValues = newValues,
                ChangedColumns = changedColumns
            };
        }
    }
}
