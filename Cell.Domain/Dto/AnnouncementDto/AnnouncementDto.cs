using Cell.Domain.Entities;

namespace Cell.Domain.Dto.AnnouncementDto;
public class AnnouncementDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Price { get; set; }
    public string? Address { get; set; }
    public DateTime Created { get; set; }
    public ICollection<Image>? Images { get; set; }
}