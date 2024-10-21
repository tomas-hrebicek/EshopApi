using Sample.Application.DTOs;
using Sample.Domain.Entities;

namespace Sample.Application.Interfaces
{
    public interface ISecurityService
    {
        Task<Result<UserDTO>> CreateAccount(CreateAccountDTO accountData);
        Task<Result<Account>> Authenticate(string login, string password);
    }
}
