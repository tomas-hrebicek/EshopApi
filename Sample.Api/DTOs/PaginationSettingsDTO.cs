using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Api.DTOs
{
    /// <summary>
    /// Represents paging settings
    /// </summary>
    public class PaginationSettingsDTO
    {
        /// <summary>
        /// What page to display.
        /// </summary>
        [DefaultValue(1)]
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// How much items is on one page.
        /// </summary>
        [DefaultValue(10)]
        [Range(1, 1000)]
        public int PageSize { get; set; } = 10;
    }
}
