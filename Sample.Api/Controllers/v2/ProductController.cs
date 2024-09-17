using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.DTOs;
using Sample.Application.Interfaces;
using Sample.Domain.Domain;

namespace Sample.Api.Controllers.v2
{
    #region ProductController

    [ApiVersion("2.0")]
    public class ProductController : ApiController
    {
        private readonly IProductsService _products;

        public ProductController(IProductsService products)
        {
            _products = products;
        }

        /// <summary>
        /// Retrieves a products list page by page settings.
        /// </summary>
        /// <param name="pageSetting">Page settings</param>
        /// <returns>a product list page</returns>
        /// <response code="200">Product list loaded</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("list")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType<PagedList<ProductDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiError>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListPagination([FromRoute] PaginationSettingsDTO pageSetting)
        {
            var result = await _products.ListAsync(pageSetting);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return UnexpectedError(result.Error);
            }
        }
    }

    #endregion
}