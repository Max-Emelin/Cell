using Cell.Domain.Interfaces;

namespace Cell.Domain.Entities
{
    public class Image : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public Announcement Announcement { get; set; }
        public Guid AnnouncementId { get; set; }
        public string Path { get; set; }
    }
}