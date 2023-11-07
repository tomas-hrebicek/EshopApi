using Sample.Application.DTOs;
using Sample.Domain.Domain;

namespace Sample.Application.Interfaces
{
    public interface IProductsService
    {
        Task<ProductDTO> GetAsync(int id);
        Task<ProductDTO> UpdateDescriptionAsync(int productId, ProductDescriptionDTO description);
        Task<IEnumerable<ProductDTO>> ListAsync();
        Task<PagedList<ProductDTO>> ListAsync(PaginationSettingsDTO paginationSettings);
    }
}
