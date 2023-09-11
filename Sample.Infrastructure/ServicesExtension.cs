using Sample.Core.Interfaces;
using Sample.Infrastructure.Data;
using Sample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sample.Infrastructure
{
    public static class ServicesExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            connectionString = TranslateConnectionString(connectionString);
            services.AddDbContext<ApplicationDbContext>(opt => 
                opt.UseSqlServer(connectionString)
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
            services.AddTransient<IProducts, ProductsRepository>();
        }
    }
}
