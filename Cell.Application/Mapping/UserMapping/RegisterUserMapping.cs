using AutoMapper;
using Cell.Domain.Dto.UserDto;
using Cell.Domain.Entities;

namespace Cell.Application.Mapping.UserMapping;

public class RegisterUserMapping : Profile
{
    public RegisterUserMapping()
    {
        CreateMap<User, RegisterUserDto>().ReverseMap();
    }
}