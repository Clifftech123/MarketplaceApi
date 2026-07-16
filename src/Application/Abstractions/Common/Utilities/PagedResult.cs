using Microsoft.EntityFrameworkCore;

namespace MarketplaceApi.src.Application.Abstractions.Common.Utilities
{
    public class PagedResult<T>
    {

        public IReadOnlyList<T> Items { get; init; } = [];
        public int TotalCount { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;

        public PagedResult<TDto> MapTo<TDto>(Func<T, TDto> map) => new()
        {
            Items = Items.Select(map).ToList(),
            TotalCount = TotalCount,
            Page = Page,
            PageSize = PageSize
        };

        public static async Task<PagedResult<T>> CreateAsync(
            IQueryable<T> query,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

    }
}
