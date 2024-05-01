using Cell.Domain.Entities;

namespace Cell.Domain.Dto.AnnouncementDto;
public record CreateAnnouncementDto
(
    Guid UserId,
    string Title,
    string? Description,
    string Price,
    string? Address,
    ICollection<Image>? Images
);