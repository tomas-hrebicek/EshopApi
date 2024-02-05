using Sample.Application.DTOs;
using Sample.Domain.Entities;

namespace Sample.Application.Interfaces
{
    public interface ISecurityService
    {
        Task<UserDTO> CreateAccount(CreateAccountDTO accountData);
        Task<User> Authenticate(string login, string password);
    }
}
