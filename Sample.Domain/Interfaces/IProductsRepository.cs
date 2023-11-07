using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Domain.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(int id);
        void UpdateAsync(Product item);
        Task<IEnumerable<Product>> ListAsync();
        Task<PagedList<Product>> ListAsync(PaginationSettings paginationSettings);
    }
}