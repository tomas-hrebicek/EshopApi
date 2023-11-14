using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Domain.Entities
{
    /// <summary>
    /// Represents Sample product.
    /// </summary>
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
        [Required]
        [MaxLength(2048)]
        public Uri ImgUri { get; set; }
        [Required]
        public decimal Price { get; set; }
        [MaxLength(5000)]
        public string Description { get; set; }
    }
}