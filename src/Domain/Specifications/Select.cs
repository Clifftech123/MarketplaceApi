using MarketplaceApi.src.Domain.Contracts;
using System.Linq.Expressions;

namespace MarketplaceApi.src.Domain.Specifications
{
    /// <summary>
    /// Wraps either a single-item or collection projection expression and applies it
    /// as a LINQ Select / SelectMany against an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    public readonly struct Select<TEntity, TResult>
        where TEntity  : IAggregateRoot
        where TResult  : ISelector
    {
        private readonly Expression<Func<TEntity, TResult>>?              _selectSingle;
        private readonly Expression<Func<TEntity, IEnumerable<TResult>>>? _selectMany;

        private Select(Expression<Func<TEntity, TResult>> selectSingle)
        {
            _selectSingle = selectSingle;
            _selectMany   = null;
        }

        private Select(Expression<Func<TEntity, IEnumerable<TResult>>> selectMany)
        {
            _selectMany   = selectMany;
            _selectSingle = null;
        }

        public static implicit operator Select<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> selectSingle) => new(selectSingle);

        public static implicit operator Select<TEntity, TResult>(
            Expression<Func<TEntity, IEnumerable<TResult>>> selectMany) => new(selectMany);

        public IQueryable<TResult> Apply(IQueryable<TEntity> query)
            => _selectSingle is not null
                ? query.Select(_selectSingle)
                : query.SelectMany(_selectMany!);
    }
}
