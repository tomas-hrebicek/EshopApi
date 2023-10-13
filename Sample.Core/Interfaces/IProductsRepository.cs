using Sample.Core.Base;
using Sample.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Core.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(int id);
        void UpdateAsync(Product item);
        Task<IEnumerable<Product>> ListAsync();
        Task<PagedList<Product>> ListAsync(PaginationSettings paginationSettings);
    }
}