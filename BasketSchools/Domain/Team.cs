
namespace BasketSchools.Domain
{
    public class Team : IEntity<int>
    {  
        public int Id { get; set; }
        public required string Name { get; set; }
        
        public string ToString()
        {
            return $"{Name}";
        }
    }
}

