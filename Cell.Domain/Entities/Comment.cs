using Cell.Domain.Interfaces;

namespace Cell.Domain.Entities
{
    public class Comment : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public User UserFrom { get; set; }
        public Guid UserFromId { get; set; }
        public User UserTo { get; set; }
        public Guid UserToId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}