using AutoMapper;
using Cell.Application.Resources;
using Cell.Domain.Dto.CommentDto;
using Cell.Domain.Dto.UserDto;
using Cell.Domain.Entities;
using Cell.Domain.Enum;
using Cell.Domain.Interfaces;
using Cell.Domain.Interfaces.Services;
using Cell.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace Trashcan.Application.Services;

public class CommentService : ICommentService
{
    private readonly IBaseRepository<Comment> _repository;
    private readonly IMapper _mapper;

    public CommentService(IBaseRepository<Comment> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<BaseResult<CommentDto>> CreateCommentAsync(CreateCommentDto dto)
    {
        try
        {
            if (dto == null)
            {
                return new BaseResult<CommentDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _repository.CreateAsync(_mapper.Map<Comment>(dto));

            return new BaseResult<CommentDto>()
            {
                Data = _mapper.Map<CommentDto>(
                    _repository.GetAll()
                        .OrderBy(x => x.Created)
                        .Last()
                    )
            };

        }
        catch (Exception e)
        {
            return new BaseResult<CommentDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
    public async Task<BaseResult<CommentDto>> DeleteCommentByIdAsync(Guid id)
    {
        try
        {
            var comment = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null)
            {
                return new BaseResult<CommentDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _repository.RemoveAsync(comment);

            return new BaseResult<CommentDto>()
            {
                Data = _mapper.Map<CommentDto>(comment)
            };
        }
        catch (Exception e)
        {
            return new BaseResult<CommentDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
    public async Task<BaseResult<CommentDto>> UpdateCommentAsync(CommentDto dto)
    {
        try
        {
            var comment = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (comment == null)
            {
                return new BaseResult<CommentDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            await _repository.UpdateAsync(_mapper.Map<Comment>(dto));

            return new BaseResult<CommentDto>()
            {
                Data = dto
            };
        }
        catch (Exception e)
        {
            return new BaseResult<CommentDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
    public async Task<CollectionResult<CommentDto>> GetUserCommentsAsync(Guid userId)
    {
        try
        {
            var comments = await _repository.GetAll()
                .Where(x => x.UserToId == userId)
                .Select(x => _mapper.Map<CommentDto>(x))
                .ToArrayAsync();

            if (!comments.Any())
            {
                return new CollectionResult<CommentDto>()
                {
                    ErrorMassage = ErrorMessage.DataNotFount,
                    ErrorCode = (int)ErrorCode.DataNotFount
                };
            }

            return new CollectionResult<CommentDto>()
            {
                Data = comments,
                Count = comments.Length
            };
        }
        catch (Exception e)
        {
            return new CollectionResult<CommentDto>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
}