using AutoMapper;
using Sample.Application.DTOs;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Api.Profiles
{
    /// <summary>
    /// Provides confirugration for mapping Product objects to DTO (data transfer objects).
    /// </summary>
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<PaginationSettingsDTO, PaginationSettings>();
            CreateMap<PagedList<Product>, PagedList<ProductDTO>>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDescriptionDTO, Product>();
        }
    }
}
