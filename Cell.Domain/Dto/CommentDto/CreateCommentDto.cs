namespace Cell.Domain.Dto.CommentDto;
public record CreateCommentDto
(
    Guid UserFromId,
    Guid UserToId,
    string Text
);