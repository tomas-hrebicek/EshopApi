using Eshop.Core.Specification;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Api.DTOs
{
    public class PaginationDTO : IPagination
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
                _pageSize = value < 1 ? 1 : value;
            }
        }
    }
}
