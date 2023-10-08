using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.DTOs;
using Sample.Core.Base;
using Sample.Core.Entities;
using Sample.Core.Interfaces;

namespace Sample.Api.Controllers.v2
{
    #region ProductController

    [ApiVersion("2.0")]
    public class ProductController : ApiController
    {
        private IProducts _products;
        private readonly IMapper _mapper;

        public ProductController(IProducts products, IMapper mapper)
        {
            _products = products;
            _mapper = mapper;
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
            var settings = _mapper.Map<PaginationSettingsDTO, PaginationSettings>(pageSetting);
            var products = await _products.ListAsync(settings);
            return Ok(_mapper.Map<PagedList<Product>, PagedList<ProductDTO>>(products));
        }
    }

    #endregion
}