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

        [HttpGet("list")]
        [MapToApiVersion("1.0")]
        public IEnumerable<ProductDTO> List()
        {
            var products = _products.List();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        [HttpPost("list")]
        [MapToApiVersion("2.0")]
        public PagedList<ProductDTO> ListPagination(PaginationDTO request)
        {
            var products = new PagedList<Product>(_products.Query(), request.PageNumber, request.PageSize);
            return _mapper.Map<PagedList<Product>, PagedList<ProductDTO>>(products);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var product = _products.Get(id);
            return product == null ? NotFound() : Ok(_mapper.Map<Product, ProductDTO>(product));
        }

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