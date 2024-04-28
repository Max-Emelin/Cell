using AutoMapper;
using Cell.Application.Resources;
using Cell.Domain.Dto.UserDto;
using Cell.Domain.Entities;
using Cell.Domain.Enum;
using Cell.Domain.Interfaces;
using Cell.Domain.Interfaces.Services;
using Cell.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace Trashcan.Application.Services;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _repository;
    private readonly IMapper _mapper;

    public UserService(IBaseRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseResult<UserDto>> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception e)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<BaseResult<UserDto>> CreateUserAsync(RegisterUserDto dto)
    {
        try
        {
            if (dto == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _repository.CreateAsync(_mapper.Map<User>(dto));

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(
                    _repository.GetAll()
                        .OrderBy(x => x.Created)
                        .Last()
                    )
            };

        }
        catch (Exception e)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<BaseResult<UserDto>> DeleteUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _repository.RemoveAsync(user);

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception e)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<BaseResult<UserDto>> UpdateUserAsync(UserDto dto)
    {
        try
        {
            var user = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _repository.UpdateAsync(_mapper.Map<User>(dto));

            return new BaseResult<UserDto>()
            {
                Data = dto
            };
        }
        catch (Exception e)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}