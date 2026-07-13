using MarketplaceApi.src.Domain.Common;
using MarketplaceApi.src.Domain.Contracts;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MarketplaceApi.src.Domain.Specifications
{
    /// <summary>
    /// Base specification for queries that return <typeparamref name="TEntity"/> directly.
    /// Derive from this class to encapsulate query criteria, includes, ordering and pagination.
    /// </summary>
    public abstract class Specification<TEntity> where TEntity : IAggregateRoot
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; init; }

        public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; } = [];

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public Pagination? Pagination { get; private set; }

        public bool IgnoresQueryFilters { get; private set; }

        protected Specification(Expression<Func<TEntity, bool>>? criteria = null)
            => Criteria = criteria;

        protected void AddInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
            => Includes.Add(include);

        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
            => OrderBy = expression;

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
            => OrderByDescending = expression;

        protected void AddPagination(Pagination? pagination)
            => Pagination = pagination;

        /// <summary>
        /// Opts this specification out of any global EF Core query filters (e.g. soft-delete filters),
        /// so entities normally excluded (like deleted rows) can be retrieved.
        /// </summary>
        protected void ApplyIgnoreQueryFilters()
            => IgnoresQueryFilters = true;
    }
}
