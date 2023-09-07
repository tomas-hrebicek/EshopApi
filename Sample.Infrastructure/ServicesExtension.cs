using Sample.Core.Interfaces;
using Sample.Infrastructure.Data;
using Sample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Infrastructure
{
    public static class ServicesExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            AddRepositories(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IProducts, ProductsRepository>();
        }
    }
}
