using Cell.Domain.Entities;

namespace Cell.Domain.Dto.AnnouncementDto;
public record CreateAnnouncementDto
(
    Guid UserId,
    string Title,
    string? Description,
    double Price,
    string? Address
);