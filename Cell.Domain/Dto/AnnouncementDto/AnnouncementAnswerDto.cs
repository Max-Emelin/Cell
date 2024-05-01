namespace Cell.Domain.Dto.AnnouncementDto;
public class AnnouncementAnswerDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public string? Address { get; set; }
    public DateTime Created { get; set; }
    public List<string>? ImagePaths { get; set; }
}