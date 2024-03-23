using AutoMapper;
using Sample.Application.DTOs;
using Sample.Application.Interfaces;
using Sample.Domain.Entities;
using Sample.Domain.Interfaces;

namespace Sample.Application.Services
{
    /// <summary>
    /// https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
    /// </summary>
    internal class SecurityService : ISecurityService
    {
        private const int HASH_LENGTH = 64;

        public SecurityService(IUsersRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private readonly IMapper _mapper;
        private readonly IUsersRepository _repository;

        public async Task<UserDTO> CreateAccount(CreateAccountDTO accountData)
        {
            var existingUser = await _repository.GetAsync(accountData.Username);
            if (existingUser is not null)
            {
                throw new ArgumentException("already exists"); 
            }
            
            SecurityProvider passwordProvider = new SecurityProvider();
            var salt = passwordProvider.CreateSalt(HASH_LENGTH);
            var hash = passwordProvider.HashPassword(accountData.Password, salt);

            User newUser = new User()
            {
                Username = accountData.Username,
                Password = hash,
                Salt = salt
            };

            newUser = await _repository.InsertAsync(newUser);
            return _mapper.Map<User, UserDTO>(newUser);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User user = await _repository.GetAsync(username);
            
            if (user is null)
            {
                return null;
            }
            else
            {
                SecurityProvider passwordProvider = new SecurityProvider();
                bool passwordOk = passwordProvider.VerifyPassword(password, user.Password, user.Salt);
                return passwordOk ? user : null;
            }
        }
    }
}