using AutoMapper;
using Sample.Application.DTOs;
using Sample.Domain.Domain;

namespace Sample.Api.Profiles
{
    /// <summary>
    /// Provides confirugration for mapping common objects to DTO (data transfer objects).
    /// </summary>
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<PaginationSettingsDTO, PaginationSettings>();
        }
    }
}
