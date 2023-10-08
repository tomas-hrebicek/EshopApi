using Microsoft.EntityFrameworkCore;
using Sample.Core.Base;

namespace Sample.Infrastructure
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedList<TItem>> ToPagedListAsync<TItem>(this IQueryable<TItem> query, PaginationSettings paginationSettings)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (paginationSettings is null)
            {
                throw new ArgumentNullException(nameof(paginationSettings));
            }

            var totalCount = await query.CountAsync();
            var items = await query.Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize).Take(paginationSettings.PageSize).ToListAsync();
            return new PagedList<TItem>(items, totalCount, paginationSettings.PageNumber, paginationSettings.PageSize);
        }
    }
}
