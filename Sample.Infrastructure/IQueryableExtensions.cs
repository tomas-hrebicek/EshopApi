using Microsoft.EntityFrameworkCore;
using Sample.Domain.Domain;

namespace Sample.Infrastructure
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Loads page by page settings from data source.
        /// </summary>
        /// <typeparam name="TItem">page item type</typeparam>
        /// <param name="query">data source</param>
        /// <param name="paginationSettings">page settings</param>
        /// <returns>page from source</returns>
        /// <exception cref="ArgumentNullException">query (data source) and pagination settings are required</exception>
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
