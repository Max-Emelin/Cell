using Cell.Domain.Dto.UserDto;
using Cell.Domain.Result;

namespace Cell.Domain.Interfaces.Services;
public interface IUserService
{
    Task<BaseResult<UserDto>> GetUserByIdAsync(Guid id);
    Task<BaseResult<UserDto>> CreateUserAsync(RegisterUserDto dto);
    Task<BaseResult<UserDto>> DeleteUserByIdAsync(Guid id);
    Task<BaseResult<UserDto>> UpdateUserAsync(UserDto dto);
    Task<BaseResult<Guid>> LoginUser(LoginUserDto dto);

}