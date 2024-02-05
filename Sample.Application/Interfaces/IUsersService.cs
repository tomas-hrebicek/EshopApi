using Sample.Application.DTOs;
using Sample.Domain.Domain;

namespace Sample.Application.Interfaces
{
    public interface IUsersService
    {
        Task<UserDTO> GetAsync(int id);
        Task<PagedList<UserDTO>> ListAsync(PaginationSettingsDTO paginationSettings);
    }
}
