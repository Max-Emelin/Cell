using AutoMapper;
using Cell.Application.Resources;
using Cell.Domain.Dto.CommentDto;
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
    public async Task<BaseResult<int>> DeleteUserCommentsAsync(Guid userId)
    {
        try
        {
            var comments = await _repository.GetAll()
                .Where(x => x.UserFromId == userId || x.UserToId == userId)
                .ToArrayAsync();

            foreach (var comment in comments)
                await DeleteCommentByIdAsync(comment.Id);

            return new BaseResult<int>()
            {
                Data = comments.Count()
            };
        }
        catch (Exception e)
        {
            return new BaseResult<int>()
            {
                ErrorMassage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }
    public async Task<BaseResult<Comment>> UpdateCommentAsync(UpdateCommentDto dto)
    {
        try
        {
            var updateComment = _mapper.Map<Comment>(dto);

            await _repository.UpdateAsync(updateComment);

            return new BaseResult<Comment>()
            {
                Data = updateComment
            };
        }
        catch (Exception e)
        {
            return new BaseResult<Comment>()
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