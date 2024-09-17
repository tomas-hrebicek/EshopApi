using Sample.Application.DTOs;
using Sample.Domain.Domain;

namespace Sample.Application.Interfaces
{
    public interface IProductsService
    {
        Task<Result<ProductDTO>> GetAsync(int id);
        Task<Result<ProductDTO>> UpdateDescriptionAsync(int productId, ProductDescriptionDTO description);
        Task<Result<IEnumerable<ProductDTO>>> ListAsync();
        Task<Result<PagedList<ProductDTO>>> ListAsync(PaginationSettingsDTO paginationSettings);
    }
}
