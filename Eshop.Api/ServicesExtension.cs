using Eshop.Core.Entities;
using EshopApi.DTO;

namespace Eshop.Api
{
    internal static class ServicesExtension
    {
        public static void AddConfiguredAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper((configuration) =>
            {
                configuration.CreateMap<Product, ProductDTO>();
            });
        }
    }
}