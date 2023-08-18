using AutoMapper;
using Eshop.Core.Interfaces;
using Eshop.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Eshop.Api.DTOs;

namespace EshopApi.Controllers
{
    #region ProductController

    [ApiController]
    [Route("[controller]")]
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
        public IEnumerable<ProductDTO> List()
        {
            var products = _products.List();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _products.Get(id);
            return product == null ? NotFound() : Ok(_mapper.Map<Product, ProductDTO>(product));
        }

        [HttpPatch("{id}/description")]
        public IActionResult UpdateDescription(int id, [FromBody]ProductDescriptionDTO updateData)
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