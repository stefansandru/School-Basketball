namespace BasketSchools.Domain;

public class ActivePlayer : IEntity<int> // m-n
{
    public int Id { get; set; }
    public int IdPlayer { get; set; }
    public int IdMatch { get; set; }
    public int Points { get; set; }
    public required string Type{ get; set; }
    
    public override string ToString()
    {
        return $"{IdPlayer} | {IdMatch} | {Points} | {Type}";
    }
}

internal enum PlayerType
{
    Reserve,
    Participant
}