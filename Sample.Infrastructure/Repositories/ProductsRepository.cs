using Microsoft.EntityFrameworkCore;
using Sample.Core.Entities;
using Sample.Core.Specification;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure.Repositories
{
    /// <summary>
    /// Provides database operations with products.
    /// </summary>
    internal sealed class ProductsRepository : Core.Interfaces.IProducts
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

        public async Task<PagedList<Product>> ListAsync(Pagination pagination)
        {
            if (pagination is null)
            {
                throw new ArgumentNullException(nameof(pagination));
            }

            var query = _dbContext.Products.AsQueryable();
            var count = await query.CountAsync();
            var list = await query.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
            return new PagedList<Product>(list, new PagingInformation()
            {
                TotalItems = count,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            });
        }

        public async void UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
