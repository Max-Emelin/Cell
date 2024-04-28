using Cell.Domain.Entities;

namespace Cell.Domain.Dto.AnnouncementDto;
public record AnnouncementDto
(
    Guid Id,
    Guid UserId,
    string Title,
    string? Description,
    string Price,
    string? Address,
    DateTime Created,
    ICollection<Image>? Images
);