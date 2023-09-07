using Sample.Core.Entities;

namespace Sample.Core.Interfaces
{
    public interface IProducts
    {
        Product Get(int id);
        void Update(Product item);
        IEnumerable<Product> List();
        IQueryable<Product> Query();
    }
}
