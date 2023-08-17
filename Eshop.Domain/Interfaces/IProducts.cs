using Eshop.Core.Entities;

namespace Eshop.Core.Interfaces
{
    public interface IProducts
    {
        IEnumerable<Product> List();
    }
}
