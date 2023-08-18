using Eshop.Api.DTOs;
using Eshop.Core.Entities;

namespace Eshop.Api
{
    internal static class ServicesExtension
    {
        public static void AddConfiguredAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper((configuration) =>
            {
                configuration.CreateMap<Product, ProductDTO>();
                configuration.CreateMap<ProductDescriptionDTO, Product>();
            });
        }
    }
}