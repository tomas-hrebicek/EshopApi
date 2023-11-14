using System.ComponentModel.DataAnnotations;

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
        [MaxLength(5000)]
        public string Description { get; set; }
    }
}