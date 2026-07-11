using MarketplaceApi.src.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MarketplaceApi.src.Infrastructure.Interceptor
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {

        private readonly IHttpContextAccessor? _httpContextAccessor;

        public SoftDeleteInterceptor(IHttpContextAccessor? httpContextAccessor = null)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null) return ValueTask.FromResult(result);

            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "System";

            foreach (var entry in eventData.Context.ChangeTracker.Entries<ISoftDeletable>())
            {
                if (entry.State != EntityState.Deleted) continue;

                entry.State = EntityState.Modified;
                entry.Entity.Delete(userName);

                CascadeSoftDelete(eventData.Context, entry.Entity, userName);
            }

            return ValueTask.FromResult(result);
        }

        private static void CascadeSoftDelete(DbContext context, ISoftDeletable parentEntity, string deletedBy)
        {
            foreach (var navigation in context.Entry(parentEntity).Navigations)
            {
                if (navigation.CurrentValue is null) continue;

                if (navigation.CurrentValue is IEnumerable<ISoftDeletable> children)
                {
                    foreach (var child in children)
                    {
                        if (child.IsDeleted) continue;
                        child.Delete(deletedBy);
                        context.Entry(child).State = EntityState.Modified;
                    }
                }
                else if (navigation.CurrentValue is ISoftDeletable child && !child.IsDeleted)
                {
                    child.Delete(deletedBy);
                    context.Entry(child).State = EntityState.Modified;
                }
            }
        }
    }
}

