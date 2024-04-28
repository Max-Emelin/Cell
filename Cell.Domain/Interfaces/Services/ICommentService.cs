using Cell.Domain.Dto.CommentDto;
using Cell.Domain.Result;

namespace Cell.Domain.Interfaces.Services;
public interface ICommentService
{
    Task<BaseResult<CommentDto>> CreateCommentAsync(CommentDto dto);
    Task<BaseResult<CommentDto>> DeleteCommentByIdAsync(Guid id);
    Task<BaseResult<CommentDto>> UpdateCommentAsync(CommentDto dto);
    Task<CollectionResult<CommentDto>> GetUserCommentsAsync(Guid userId);
}