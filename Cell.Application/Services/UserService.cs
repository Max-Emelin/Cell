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
    private readonly IBaseRepository<User> _userRepository;
    private readonly ICommentService _commentService;
    private readonly IAnnouncementService _announcementService;
    private readonly IMapper _mapper;

    public UserService
    (
        IBaseRepository<User> userRepository,
        ICommentService commentService,
        IAnnouncementService announcementService,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _commentService = commentService;
        _announcementService = announcementService;
        _mapper = mapper;
    }

    public async Task<BaseResult<UserDto>> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _userRepository.GetAll()
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

            var userExists = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (userExists != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMassage = ErrorMessage.UserAlreadyExists,
                    ErrorCode = (int)ErrorCode.UserAlreadyExists
                };
            }

            await _userRepository.CreateAsync(_mapper.Map<User>(dto));

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(
                    _userRepository.GetAll()
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
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _userRepository.RemoveAsync(user);
            await DeleteUserAnnouncementsAndComments(id);

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
            await _userRepository.UpdateAsync(_mapper.Map<User>(dto));

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

    private async Task<BaseResult<bool>> DeleteUserAnnouncementsAndComments(Guid userId)
    {
        try
        {
            await _announcementService.DeleteUserAnnouncementsAsync(userId);
            await _commentService.DeleteUserCommentsAsync(userId);

            return new BaseResult<bool>()
            {
                Data = true
            };
        }
        catch (Exception e)
        {
            return new BaseResult<bool>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<BaseResult<Guid>> LoginUser(LoginUserDto dto)
    {
        try
        {
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Login == dto.Login);
            if (user == null)
            {
                return new BaseResult<Guid>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            if (user.Password != dto.Password)
            {
                return new BaseResult<Guid>()
                {
                    ErrorMassage = ErrorMessage.InvalidPassword,
                    ErrorCode = (int)ErrorCode.InvalidPassword
                };
            }

            return new BaseResult<Guid>()
            {
                Data = user.Id
            };

        }
        catch (Exception e)
        {
            return new BaseResult<Guid>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}