using AutoMapper;
using Cell.Domain.Dto.UserDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.UserMapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}