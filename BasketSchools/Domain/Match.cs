namespace BasketSchools.Domain;

public class Match : IEntity<int>
{
    public int Id { get; set; }
    public required string HomeTeam { get; set; }
    public required string AwayTeam { get; set; }
    public required DateTime Date { get; set; }
    
    public override string ToString()
    {
        return $"{HomeTeam} vs {AwayTeam} | {Date:yyyy-MM-dd}";
    }
}