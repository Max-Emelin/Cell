using Cell.Domain.Interfaces;

namespace Cell.Domain.Entities
{
    public class User : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public List<Announcement> Announcements { get; } = new List<Announcement>();
        public List<Comment> Comments { get; } = new List<Comment>();
    }
}