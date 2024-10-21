using AutoMapper;
using Sample.Application;
using Sample.Application.DTOs;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Api.Profiles
{
    /// <summary>
    /// Provides confirugration for mapping User objects to DTO (data transfer objects).
    /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<PagedList<User>, PagedList<UserDTO>>();
            CreateMap<User, UserDTO>();
            CreateMap<User, Account>();
        }
    }
}
