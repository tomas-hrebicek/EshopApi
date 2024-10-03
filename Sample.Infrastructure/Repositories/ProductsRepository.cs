using Microsoft.EntityFrameworkCore;
using Sample.Domain.Domain;
using Sample.Domain.Entities;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure.Repositories
{
    /// <summary>
    /// Provides database operations with products.
    /// </summary>
    internal sealed class ProductsRepository : Domain.Interfaces.IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<PagedList<Product>> ListAsync(PaginationSettings paginationSettings)
        {
            if (paginationSettings is null)
            {
                throw new ArgumentNullException(nameof(paginationSettings));
            }

            return await _dbContext.Products.ToPagedListAsync(paginationSettings);
        }

        public async void UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
