using System.ComponentModel.DataAnnotations;

namespace Eshop.Api.DTOs
{
    /// <summary>
    /// Represents a product.
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Product unique identificator
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Uri to product image.
        /// </summary>
        [Required]
        public string ImgUri { get; set; }
        /// <summary>
        /// Product price.
        /// </summary>
        [Required]
        public decimal Price { get; set; }
        /// <summary>
        /// Product description.
        /// </summary>
        public string Description { get; set; }
    }
}