using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Core.Specification
{
    /// <summary>
    /// Represents one page of items with information about page.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public sealed class PagedList<TItem>
    {
        public PagedList() { }

        public PagedList(IEnumerable<TItem> source, PagingInformation paging)
        {
            if (paging is null)
            {
                throw new ArgumentNullException(nameof(paging));
            }

            this.Paging = paging;
            this.Item = source;
        }

        public PagingInformation Paging { get; set; }
        public IEnumerable<TItem> Item { get; set; }
    }

    /// <summary>
    /// Represents informations about page.
    /// </summary>
    public sealed class PagingInformation : Pagination
    {
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);
    }

    /// <summary>
    /// Represents pagination settings
    /// </summary>
    public class Pagination
    {
        private int _pageNumber = 1;
        private int _pageSize = 10;

        public int PageNumber 
        {
            get => _pageNumber;
            set
            {
                _pageNumber = value < 1 ? 1 : value; 
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1) 
                    _pageSize = 1;
                else if (value > 1000) 
                    _pageSize = 1000;
                else 
                    _pageSize = value;
            }
        }
    }
}
