using System;
using System.Collections.Generic;

namespace Sample.Core.Base
{
    /// <summary>
    /// Represents a paged list of items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class PagedList<T>
    {
        public PagedList(IList<T> items, int totalCount, int pageNumber, int pageSize) 
        {
            this.Items = items ?? throw new ArgumentNullException(nameof(items));
            this.TotalCount = totalCount;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// Gets the items on the current page.
        /// </summary>
        public IList<T> Items { get; private set; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>s
        /// Gets the total count of items across all pages.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int PageCount => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// Gets the number of the first item on the current page.
        /// </summary>
        public int FirstItemOnPage => (PageNumber - 1) * PageSize + 1;

        /// <summary>
        /// Indicates whether the current page has a subsequent page.
        /// </summary>
        public bool HasNextPage => PageNumber < PageCount;
        
        /// <summary>
        /// Indicates whether the current page has a preceding page.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Indicates whether the current page is the first page.
        /// </summary>
        public bool IsFirstPage => PageNumber == 1;

        /// <summary>
        /// Indicates whether the current page is the last page.
        /// </summary>
        public bool IsLastPage => PageNumber == PageCount;

        /// <summary>
        /// Gets the number of the last item on the current page.
        /// </summary>
        public int LastItemOnPage => FirstItemOnPage + Items.Count - 1;

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get.</param>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Items.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                }
                return Items[index];
            }
        }
    }
}
