namespace Cell.Domain.Dto.CommentDto;
public record CommentDto
(
    Guid Id,
    Guid UserFromId,
    Guid UserToId,
    string Text,
    DateTime Created
);