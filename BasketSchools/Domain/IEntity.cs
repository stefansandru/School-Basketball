
namespace BasketSchools.Domain
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}

