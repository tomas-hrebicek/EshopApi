using Eshop.Core.Specification;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Api.DTOs
{
    public class PaginationDTO : IPagination
    {
        [DefaultValue(1)]
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        [Range(1, 1000)]
        public int PageSize { get; set; } = 10;
    }
}
