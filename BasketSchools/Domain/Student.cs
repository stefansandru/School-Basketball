namespace BasketSchools.Domain
{
    public class Student : IEntity<int>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string School { get; set; }
        
        public override string ToString()  
        {
            return $"{Name} | {School}";
        }
    }
}

