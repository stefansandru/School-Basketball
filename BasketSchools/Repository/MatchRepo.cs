using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using BasketSchools.Domain;

namespace BasketSchools.Repository;

public class MatchRepo : IRepository<int, Match>
{
    public List<Match> _matches = new List<Match>();
    
    public MatchRepo()
    {
        ReadFile();
    }
    
    public Match? FindOne(int id)
    {
        return _matches.FirstOrDefault(m => m.Id == id);
    }
    
    public IEnumerable<Match> FindAll()
    {
        return _matches;
    }
    
    public Match? Save(Match match)
    {
        if (match == null)
            throw new ArgumentException("Match cannot be null.");
        
        if (FindOne(match.Id) != null)
            return match;
        
        _matches.Add(match);
        return null;
    }
    
    public Match? Delete(int id)
    {
        var match = FindOne(id);
        if (match == null)
            return null;
        
        _matches.Remove(match);
        return match;
    }
    
    public Match? Update(Match match)
    {
        if (match == null)
            throw new ArgumentException("Match cannot be null.");
        
        var existing = FindOne(match.Id);
        if (existing == null)
            return match;
        
        existing.HomeTeam = match.HomeTeam;
        existing.AwayTeam = match.AwayTeam;
        existing.Date = match.Date;
        
        return null;
    }

    public void ReadFile()
{
    var lines = File.ReadAllLines(
        "/Users/stefansandru/Documents/BasketSchools/BasketSchools/DataFiles/matches.csv");
    foreach (var line in lines)
    {
        var values = line.Split(',');
        try
        {
            var match = new Match
            {
                Id = int.Parse(values[0]),
                HomeTeam = values[1],
                AwayTeam = values[2],
                Date = DateTime.ParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            _matches.Add(match);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Invalid date format for line: {line}. Error: {ex.Message}");
        }
    }
}
}