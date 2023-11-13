using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.DTOs;
using Sample.Application.Interfaces;
using Sample.Domain.Entities;

namespace Sample.Api.Controllers.v1
{
    #region ProductController

    [ApiVersion("1.0")]
    public class ProductController : ApiController
    {
        private readonly IProductsService _products;

        public ProductController(IProductsService products)
        {
            _products = products;
        }

        /// <summary>
        /// Retrieves all products list.
        /// </summary>
        /// <returns>a products list</returns>
        [HttpGet("list")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDTO>))]
        public async Task<IActionResult> List()
        {
            return Ok(await _products.ListAsync());
        }

        /// <summary>
        /// Retrieves a specific product by unique id.
        /// </summary>
        /// <param name="id">Product unique identificator</param>
        /// <returns>a product</returns>
        /// <response code="200">Product found</response>
        /// <response code="404">Product not found</response>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _products.GetAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Updates a specific product description.
        /// </summary>
        /// <param name="id">Product unique identificator</param>
        /// <param name="description">product description for update</param>
        /// <returns>an updated product</returns>
        /// <response code="200">Product found and updated description</response>
        /// <response code="404">Product not found</response>
        [HttpPatch("{id}/description")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDescription(int id, [FromBody] ProductDescriptionDTO description)
        {
            var product = await _products.UpdateDescriptionAsync(id, description);
            return product is null ? NotFound() : Ok(product);
        }
    }

    #endregion
}