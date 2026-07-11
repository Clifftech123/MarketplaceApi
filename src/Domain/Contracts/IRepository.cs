using MarketplaceApi.src.Domain.Common.Aggregates;
using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Specifications;

namespace MarketplaceApi.src.Domain.Contracts
{
    /// <summary>
    /// Generic repository contract.
    /// TEntity must be an aggregate root; TEntityId is its strongly-typed identifier.
    /// </summary>
    public interface IRepository<TEntity, in TEntityId>
        where TEntity  : AggregateRoot<TEntityId>
        where TEntityId : EntityId
    {
        
        Task<TEntity>       AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity>       UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity?>      GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool>          DeleteByIdAsync(TEntityId id, CancellationToken cancellationToken = default);
        Task<bool>          ExistByIdAsync(TEntityId id, CancellationToken cancellationToken = default);
        Task<int>           CountAsync(CancellationToken cancellationToken = default);

       
        Task<TEntity?>      GetOneAsync<TSpec>(TSpec spec, CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>;

        Task<List<TEntity>> GetManyAsync<TSpec>(TSpec spec, CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>;

        Task<int>           CountAsync<TSpec>(TSpec spec, CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>;

        Task<bool>          ExistsAsync<TSpec>(TSpec spec, CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>;

        Task<bool>          DeleteAsync<TSpec>(TSpec spec, CancellationToken cancellationToken = default)
            where TSpec : Specification<TEntity>;

       
        Task<TResult?>      GetOneAsync<TSpec, TResult>(TSpec spec, CancellationToken cancellationToken = default)
            where TSpec   : ProjectionSpecification<TEntity, TResult>
            where TResult : ISelector;

        Task<List<TResult>> GetManyAsync<TSpec, TResult>(TSpec spec, CancellationToken cancellationToken = default)
            where TSpec   : ProjectionSpecification<TEntity, TResult>
            where TResult : ISelector;

        IRepository<TEntity, TEntityId> AsTracking();
    }
}
