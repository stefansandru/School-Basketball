namespace BasketSchools.Domain;

public class Player : Student
{
    public required int IdTeam { get; set; }
    
    public override string ToString()
    {
        return $"{Name} | {School} | {IdTeam}";
    }
}


