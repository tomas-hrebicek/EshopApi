using Sample.Application.DTOs;
using Sample.Domain.Domain;

namespace Sample.Application.Interfaces
{
    public interface IUsersService
    {
        Task<Result<UserDTO>> GetAsync(int id);
        Task<Result<PagedList<UserDTO>>> ListAsync(PaginationSettingsDTO paginationSettings);
    }
}
