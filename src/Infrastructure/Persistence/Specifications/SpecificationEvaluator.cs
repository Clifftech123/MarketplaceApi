using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Specifications;

namespace MarketplaceApi.src.Infrastructure.Persistence.Specifications
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> Apply<TEntity>(
            this IQueryable<TEntity> query,
            Specification<TEntity> specification)
            where TEntity : IAggregateRoot
        {
            if (specification.Criteria is { } criteria)
                query = query.Where(criteria);

            query = specification.Includes.Aggregate(query, (current, include) => include(current));

            if (specification.OrderBy is { } orderBy)
                query = query.OrderBy(orderBy);
            else if (specification.OrderByDescending is { } orderByDesc)
                query = query.OrderByDescending(orderByDesc);

            if (specification.Pagination is { } pagination)
                query = query.Skip(pagination.Skip).Take(pagination.Take);

            return query;
        }

        public static IQueryable<TResult> Apply<TEntity, TResult>(
            this IQueryable<TEntity> query,
            ProjectionSpecification<TEntity, TResult> specification)
            where TEntity : IAggregateRoot
            where TResult : ISelector
        {
            if (specification.Criteria is { } criteria)
                query = query.Where(criteria);

            query = specification.Includes.Aggregate(query, (current, include) => include(current));

            if (specification.OrderBy is { } orderBy)
                query = query.OrderBy(orderBy);
            else if (specification.OrderByDescending is { } orderByDesc)
                query = query.OrderByDescending(orderByDesc);

            if (specification.Pagination is { } pagination)
                query = query.Skip(pagination.Skip).Take(pagination.Take);

            return specification.Select.Apply(query);
        }
    }
}
