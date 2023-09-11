using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.DTOs;
using Sample.Application;
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
        [HttpPost("list")]
        [MapToApiVersion("2.0")]
        public PagedList<ProductDTO> ListPagination(PaginationDTO pageSetting)
        {
            var products = new PagedList<Product>(_products.Query(), pageSetting.PageNumber, pageSetting.PageSize);
            return _mapper.Map<PagedList<Product>, PagedList<ProductDTO>>(products);
        }
    }

    #endregion
}