using AutoMapper;
using Eshop.Api.DTOs;
using Eshop.Application;
using Eshop.Core.Entities;

namespace Eshop.Api.Profiles
{
    /// <summary>
    /// Provides confirugration for mapping Product objects to DTO (data transfer objects).
    /// </summary>
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<PagedList<Product>, PagedList<ProductDTO>>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDescriptionDTO, Product>();
        }
    }
}
