using Cell.Domain.Interfaces;

namespace Cell.Domain.Entities
{
    public class Announcement : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? Address { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<Image> Images { get; } = new List<Image>();
    }
}