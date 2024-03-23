using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Interfaces;
using Sample.Infrastructure.Data;
using Sample.Infrastructure.Repositories;

namespace Sample.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for infrastructure layer
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="connectionString">connection string for database</param>
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            connectionString = TranslateConnectionString(connectionString);
            services.AddDbContext<ApplicationDbContext>(opt => 
                opt.UseNpgsql(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            AddRepositories(services);
        }

        private static string TranslateConnectionString(string connectionString)
        {
            var dataDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            return connectionString.Replace("|DataDirectory|", dataDirectory);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
        }
    }
}
