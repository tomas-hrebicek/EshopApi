using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Interfaces;
using Sample.Application.Services;

namespace Sample.Application
{
    public static class ServicesExtension
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServicesExtension).Assembly);
            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IProductsService, ProductsService>();
        }
    }
}