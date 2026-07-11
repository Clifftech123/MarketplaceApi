using MarketplaceApi.src.Domain.Common;
using MarketplaceApi.src.Domain.Contracts;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MarketplaceApi.src.Domain.Specifications
{
    /// <summary>
    /// Base specification for queries that project <typeparamref name="TEntity"/> into
    /// a read-model / DTO of type <typeparamref name="TResult"/>.
    /// </summary>
    public abstract class ProjectionSpecification<TEntity, TResult>
        where TEntity  : IAggregateRoot
        where TResult  : ISelector
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; } = [];

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public Pagination? Pagination { get; private set; }

        /// <summary>The projection expression applied via Select / SelectMany.</summary>
        public Select<TEntity, TResult> Select { get; private set; }

        protected ProjectionSpecification(Expression<Func<TEntity, bool>>? criteria = null)
            => Criteria = criteria;

        protected void AddInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
            => Includes.Add(include);

        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
            => OrderBy = expression;

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
            => OrderByDescending = expression;

        protected void AddPagination(Pagination? pagination)
            => Pagination = pagination;

        /// <summary>Project each entity to a single <typeparamref name="TResult"/>.</summary>
        protected void ProjectTo(Expression<Func<TEntity, TResult>> select)
            => Select = select;

        /// <summary>Project each entity to a collection of <typeparamref name="TResult"/>.</summary>
        protected void ProjectTo(Expression<Func<TEntity, IEnumerable<TResult>>> select)
            => Select = select;
    }
}
