using Eshop.Core.Specification;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Application
{
    /// <summary>
    /// Represents one page of items with information about page.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public sealed class PagedList<TItem>
    {
        public PagedList() { }

        public PagedList(IQueryable<TItem> source, int pageNumber, int pageSize)
        {
            this.Paging = new PagingInformation()
            {
                TotalItems = source.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            this.Item = source.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public PagingInformation Paging { get; set; }
        public List<TItem> Item { get; set; }
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
    public class Pagination : IPagination
    {
        private int _pageNumber = 1;
        private int _pageSize = 10;

        [DefaultValue(1)]
        [Range(1, int.MaxValue)]
        public int PageNumber 
        {
            get => _pageNumber;
            set
            {
                _pageNumber = value < 1 ? 1 : value; 
            }
        }

        [DefaultValue(10)]
        [Range(1, 1000)]
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
