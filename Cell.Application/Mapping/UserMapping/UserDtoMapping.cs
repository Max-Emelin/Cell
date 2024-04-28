using AutoMapper;
using Cell.Domain.Dto.UserDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.UserMapping;

public class UserDtoMapping : Profile
{
    public UserDtoMapping()
    {
        CreateMap<UserDto, User>().ReverseMap();
    }
}