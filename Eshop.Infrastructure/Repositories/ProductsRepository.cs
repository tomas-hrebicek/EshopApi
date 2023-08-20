using Eshop.Core.Entities;
using Eshop.Core.Specification;
using Eshop.Infrastructure.Data;

namespace Eshop.Infrastructure.Repositories
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

        public Product Get(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public IEnumerable<Product> List()
        {
            return _dbContext.Products.ToList();
        }

        public IQueryable<Product> Query()
        {
            return _dbContext.Products.AsQueryable<Product>();
        }

        public IEnumerable<Product> List(IPagination pagination)
        {
            return _dbContext.Products.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
