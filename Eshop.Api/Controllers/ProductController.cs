using AutoMapper;
using Eshop.Core.Interfaces;
using Eshop.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Eshop.Api.DTOs;
using Eshop.Application;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Eshop.Api.Controllers
{
    #region ProductController

    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ProductController : ControllerBase
    {
        private IProducts _products;
        private readonly IMapper _mapper;

        public ProductController(IProducts products, IMapper mapper)
        {
            _products = products;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all products list.
        /// </summary>
        /// <returns>a products list</returns>
        [HttpGet("list")]
        [MapToApiVersion("1.0")]
        public IEnumerable<ProductDTO> List()
        {
            var products = _products.List();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        /// <summary>
        /// Retrieves a products list page by page settings.
        /// </summary>
        /// <param name="pageSetting">Page settings</param>
        /// <returns>a product list page</returns>
        [HttpPost("list")]
        [MapToApiVersion("2.0")]
        public PagedList<ProductDTO> ListPagination(PaginationDTO pageSetting)
        {
            var products = new PagedList<Product>(_products.Query(), pageSetting.PageNumber, pageSetting.PageSize);
            return _mapper.Map<PagedList<Product>, PagedList<ProductDTO>>(products);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var product = _products.Get(id);
            return product == null ? NotFound() : Ok(_mapper.Map<Product, ProductDTO>(product));
        }

        /// <summary>
        /// Updates a specific product description.
        /// </summary>
        /// <param name="id">Product unique identificator</param>
        /// <param name="updateData">data for update</param>
        /// <returns>an updated product</returns>
        /// <response code="200">Product found and updated description</response>
        /// <response code="404">Product not found</response>
        [HttpPatch("{id}/description")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateDescription(int id, [FromBody] ProductDescriptionDTO updateData)
        {
            var product = _products.Get(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                _mapper.Map(updateData, product);
                _products.Update(product);
                return Get(id);
            }
        }
    }

    #endregion
}