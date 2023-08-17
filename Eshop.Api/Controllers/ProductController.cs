using AutoMapper;
using Eshop.Core.Interfaces;
using Eshop.Core.Entities;
using EshopApi.DTO;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IEnumerable<ProductDTO> List()
        {
            var goods = _products.List();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(goods);
        }
    }

    #endregion
}