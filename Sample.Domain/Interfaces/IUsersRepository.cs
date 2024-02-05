using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System.Threading.Tasks;

namespace Sample.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(int id);
        Task<User> GetAsync(string login);
        Task<User> InsertAsync(User item);
        Task UpdateAsync(User item);
        Task<PagedList<User>> ListAsync(PaginationSettings paginationSettings);
    }
}