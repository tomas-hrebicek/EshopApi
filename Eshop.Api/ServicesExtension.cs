using AutoMapper;
using Eshop.Api.DTOs;
using Eshop.Application;
using Eshop.Core.Entities;

namespace Eshop.Api
{
    internal static class ServicesExtension
    {
        public static void AddConfiguredAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper((configuration) =>
            {
                configuration.CreateMap<PagedList<Product>, PagedList<ProductDTO>>();
                configuration.CreateMap<Product, ProductDTO>();
                configuration.CreateMap<ProductDescriptionDTO, Product>();
            });
        }
    }
}