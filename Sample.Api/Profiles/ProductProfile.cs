using AutoMapper;
using Sample.Api.DTOs;
using Sample.Application;
using Sample.Core.Entities;

namespace Sample.Api.Profiles
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
