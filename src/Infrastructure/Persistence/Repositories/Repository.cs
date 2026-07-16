using MarketplaceApi.src.Application.Abstractions.Common.Utilities;
using MarketplaceApi.src.Domain.Common.Aggregates;
using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Specifications;
using MarketplaceApi.src.Infrastructure.Persistence.Context;
using MarketplaceApi.src.Infrastructure.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarketplaceApi.src.Infrastructure.Persistence.Repositories
{
    internal class Repository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>

        where TEntity : AggregateRoot<TEntityId>
        where TEntityId : EntityId
    {
        private readonly ApplicationDbContext _context;

        private DbSet<TEntity> DbSet => _context.Set<TEntity>();
        private readonly Func<TEntityId, Expression<Func<TEntity, bool>>> _predicateById = id => e => e.Id.Equals(id);

        private bool _tracking;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await DbSet.AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<TEntity?> GetByIdAsync(TEntityId entityId, CancellationToken cancellationToken = default)
            => await (_tracking ? DbSet.AsTracking() : DbSet.AsNoTracking())
                .FirstOrDefaultAsync(_predicateById(entityId), cancellationToken);

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await DbSet.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<bool> ExistByIdAsync(TEntityId entityId, CancellationToken cancellationToken = default)
            => await DbSet.AsNoTracking()
                .AnyAsync(_predicateById(entityId), cancellationToken);

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
            => await DbSet.AsNoTracking().CountAsync(cancellationToken);

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entry = DbSet.Update(entity);
            await SaveChangesAsync(cancellationToken);
            ClearChangeTracker();
            return entry.Entity;
        }

        public async Task<bool> DeleteByIdAsync(TEntityId entityId, CancellationToken cancellationToken = default)
        {
            var deleted =
                await DbSet.Where(_predicateById(entityId))
                    .ExecuteDeleteAsync(cancellationToken) > 0;
            await SaveChangesAsync(cancellationToken);
            ClearChangeTracker();
            return deleted;
        }

        public async Task<TEntity?> GetOneAsync<TSpec>(
            TSpec specification,
            CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>
            => await (_tracking ? DbSet.AsTracking() : DbSet.AsNoTracking())
                .Apply(specification)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<List<TEntity>> GetManyAsync<TSpec>(
            TSpec specification,
            CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>
            => await DbSet.AsNoTracking()
                .Apply(specification)
                .ToListAsync(cancellationToken);

        public async Task<int> CountAsync<TSpec>(
            TSpec specification,
            CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>
            => await DbSet.AsNoTracking()
                .Apply(specification)
                .CountAsync(cancellationToken);

        public async Task<bool> ExistsAsync<TSpec>(
            TSpec specification,
            CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>
            => await DbSet.AsNoTracking()
                .Apply(specification)
                .AnyAsync(cancellationToken);

        public async Task<bool> DeleteAsync<TSpec>(
            TSpec specification,
            CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>
        {
            var deleted = await DbSet
                .Apply(specification)
                .ExecuteDeleteAsync(cancellationToken) > 0;
            await SaveChangesAsync(cancellationToken);
            ClearChangeTracker();
            return deleted;
        }

        public async Task<TResult?> GetOneAsync<TSpec, TResult>(
            TSpec specification,
            CancellationToken cancellationToken = default)
            where TSpec : ProjectionSpecification<TEntity, TResult>
            where TResult : ISelector
            => await DbSet.AsNoTracking()
                .Apply(specification)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<List<TResult>> GetManyAsync<TSpec, TResult>(
            TSpec specification,
            CancellationToken cancellationToken = default)
            where TSpec : ProjectionSpecification<TEntity, TResult>
            where TResult : ISelector
            => await DbSet.AsNoTracking()
                .Apply(specification)
                .ToListAsync(cancellationToken);

        public IRepository<TEntity, TEntityId> AsTracking()
        {
            _tracking = true;
            return this;
        }

        private async Task SaveChangesAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);

        private void ClearChangeTracker()
        {
            _context.ChangeTracker.Clear();
            _tracking = false;
        }


        public IQueryable<TEntity> Query()
        {
            var query = _tracking ? DbSet.AsTracking() : DbSet.AsNoTracking();
            _tracking = false;
            return query;
        }

        public async Task<PagedResult<TEntity>> GetPagedAsync<TSpec>(TSpec spec, int page, int pageSize, CancellationToken cancellationToken = default) where TSpec : Specification<TEntity>
        {
            var query = DbSet.AsNoTracking().Apply(spec);
            return await PagedResult<TEntity>.CreateAsync(query, page, pageSize, cancellationToken);
        }
    }
}
