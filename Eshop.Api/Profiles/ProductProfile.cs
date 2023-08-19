using AutoMapper;
using Eshop.Api.DTOs;
using Eshop.Application;
using Eshop.Core.Entities;

namespace Eshop.Api.Profiles
{
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
