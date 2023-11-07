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
        [HttpGet("list")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ProductDTO>))]
        public async Task<IActionResult> ListPagination([FromRoute] PaginationSettingsDTO pageSetting)
        {
            return Ok(await _products.ListAsync(pageSetting));
        }
    }

    #endregion
}