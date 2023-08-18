using Eshop.Core.Entities;

namespace Eshop.Core.Interfaces
{
    public interface IProducts
    {
        Product Get(int id);
        IEnumerable<Product> List();
    }
}
