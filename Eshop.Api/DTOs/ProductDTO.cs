﻿using System.ComponentModel.DataAnnotations;

namespace Eshop.Api.DTOs
{
    public class ProductDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImgUri { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}