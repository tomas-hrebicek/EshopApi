using AutoMapper;
using Sample.Api.DTOs;
using Sample.Core.Entities;
using Sample.Core.Specification;

namespace Sample.Api.Profiles
{
    /// <summary>
    /// Provides confirugration for mapping Product objects to DTO (data transfer objects).
    /// </summary>
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<PaginationDTO, Pagination>();
            CreateMap<PagedList<Product>, PagedList<ProductDTO>>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDescriptionDTO, Product>();
        }
    }
}
