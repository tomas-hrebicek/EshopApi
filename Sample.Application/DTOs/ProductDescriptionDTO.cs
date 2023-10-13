namespace Sample.Application.DTOs
{
    /// <summary>
    /// Represents product description data.
    /// </summary>
    public record ProductDescriptionDTO
    {
        /// <summary>
        /// product description
        /// </summary>
        public string Description { get; set; }
    }
}