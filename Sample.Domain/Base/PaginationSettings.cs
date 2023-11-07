namespace Sample.Domain.Domain
{
    /// <summary>
    /// Represents pagination settings
    /// </summary>
    public class PaginationSettings
    {
        /// <summary>
        /// What page to display.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// How much items is on one page.
        /// </summary>
        public int PageSize { get; set; }
    }
}
