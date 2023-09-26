using Sample.Core.Entities;
using Sample.Core.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Core.Interfaces
{
    public interface IProducts
    {
        Task<Product> GetAsync(int id);
        void UpdateAsync(Product item);
        Task<IEnumerable<Product>> ListAsync();
        Task<PagedList<Product>> ListAsync(Pagination pagination);
    }
}
