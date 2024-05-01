using Cell.Domain.Dto.CommentDto;
using Cell.Domain.Result;

namespace Cell.Domain.Interfaces.Services;
public interface ICommentService
{
    Task<BaseResult<CommentDto>> CreateCommentAsync(CreateCommentDto dto);
    Task<BaseResult<CommentDto>> DeleteCommentByIdAsync(Guid id);
    Task<BaseResult<CommentDto>> UpdateCommentAsync(CommentDto dto);
    Task<CollectionResult<CommentDto>> GetUserCommentsAsync(Guid userId);
    Task<BaseResult<int>> DeleteUserCommentsAsync(Guid userId);
}