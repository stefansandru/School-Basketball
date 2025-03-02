using BasketSchools.Domain;

namespace BasketSchools.Repository;

public class ActivePlayerRepo : IRepository<int, ActivePlayer>
{
    private List<ActivePlayer> _activePlayers = new List<ActivePlayer>();
    
    public ActivePlayerRepo()
    {
        ReadFile();
    }
    
    public ActivePlayer? FindOne(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID cannot be null or zero.");
        
        return _activePlayers.FirstOrDefault(p => p.Id == id );
    }
    
    public IEnumerable<ActivePlayer> FindAll()
    {
        return _activePlayers;
    }
    
    public ActivePlayer? Save(ActivePlayer activePlayer)
    {
        if (activePlayer == null)
            throw new ArgumentException("ActivePlayer cannot be null.");
        
        if (FindOne(activePlayer.Id) != null)
            return activePlayer;
        
        _activePlayers.Add(activePlayer);
        return null;
    }
    
    public ActivePlayer? Delete(int id)
    {
        var activePlayer = FindOne(id);
        if (activePlayer == null)
            return null;
        
        _activePlayers.Remove(activePlayer);
        return activePlayer;
    }
    
    public ActivePlayer? Update(ActivePlayer activePlayer)
    {
        if (activePlayer == null)
            throw new ArgumentException("ActivePlayer cannot be null.");
        
        var existing = FindOne(activePlayer.Id);
        if (existing == null)
            return activePlayer;
        
        existing.IdPlayer = activePlayer.IdPlayer;
        existing.IdMatch = activePlayer.IdMatch;
        existing.Points = activePlayer.Points;
        existing.Type = activePlayer.Type;
        
        return null;
    }

    public void ReadFile()
    {
        var lines = File.ReadAllLines(
            "/Users/stefansandru/Documents/BasketSchools/BasketSchools/DataFiles/ActivePlayers.csv");
        foreach (var line in lines)
        {
            var values = line.Split(',');
            var activePlayer = new ActivePlayer
            {
                Id = int.Parse(values[0]),
                IdPlayer = int.Parse(values[1]),
                IdMatch = int.Parse(values[2]),
                Points = int.Parse(values[3]),
                Type = values[4]
            };
            _activePlayers.Add(activePlayer);
        }
    }
}