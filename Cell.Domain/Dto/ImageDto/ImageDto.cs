namespace Cell.Domain.Dto.ImageDto;

public class ImageDto
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public string Path { get; set; }
}
