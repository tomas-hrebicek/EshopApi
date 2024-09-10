using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure
{
    public static class MigrationProvider
    {
        /// <summary>
        /// Starts application database context migration
        /// </summary>
        /// <param name="services">service provider</param>
        public static void Migrate(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
        }
    }
}
