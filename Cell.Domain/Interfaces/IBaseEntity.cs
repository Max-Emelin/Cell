namespace Cell.Domain.Interfaces
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}