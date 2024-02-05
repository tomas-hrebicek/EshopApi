using AutoMapper;
using Sample.Application.DTOs;
using Sample.Application.Interfaces;
using Sample.Domain.Domain;
using Sample.Domain.Entities;
using Sample.Domain.Interfaces;

namespace Sample.Application.Services
{
    internal class UsersService : IUsersService
    {
        public UsersService(IUsersRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private readonly IMapper _mapper;
        private readonly IUsersRepository _repository;

        public async Task<UserDTO> GetAsync(int id)
        {
            var user = await _repository.GetAsync(id);
            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task<PagedList<UserDTO>> ListAsync(PaginationSettingsDTO paginationSettings)
        {
            var settings = _mapper.Map<PaginationSettingsDTO, PaginationSettings>(paginationSettings);
            var users = await _repository.ListAsync(settings);
            return _mapper.Map<PagedList<User>, PagedList<UserDTO>>(users);
        }
    }
}