using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sample.Application;
using Sample.Application.DTOs;
using Sample.Application.Interfaces;

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
        /// <response code="200">Product list loaded</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("list")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType<IEnumerable<ProductDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiError>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            var result = await _products.ListAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return UnexpectedError(result.Error);
            }
        }

        /// <summary>
        /// Retrieves a specific product by unique id.
        /// </summary>
        /// <param name="id">Product unique identificator</param>
        /// <returns>a product</returns>
        /// <response code="200">Product found</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType<ProductDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiError>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiError>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _products.GetAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else if (result.Error is NotFoundError)
            {
                return NotFound(result.Error);
            }
            else
            {
                return UnexpectedError(result.Error);
            }
        }

        /// <summary>
        /// Updates a specific product description.
        /// </summary>
        /// <param name="id">Product unique identificator</param>
        /// <param name="description">product description for update</param>
        /// <returns>an updated product</returns>
        /// <response code="200">Product found and updated description</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch("{id}/description")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType<ProductDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiError>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiError>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDescription(int id, [FromBody] ProductDescriptionDTO description)
        {
            var result = await _products.UpdateDescriptionAsync(id, description);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else if (result.Error is NotFoundError)
            {
                return NotFound(result.Error);
            }
            else
            {
                return UnexpectedError(result.Error);
            }
        }
    }

    #endregion
}