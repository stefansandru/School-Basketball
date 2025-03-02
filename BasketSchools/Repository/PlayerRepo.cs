using BasketSchools.Domain;

namespace BasketSchools.Repository;

public class PlayerRepo : IRepository<int, Player>
{
    private List<Player> _players = new List<Player>();
    
    public PlayerRepo()
    {
        ReadFile();
    }
    
    public Player? FindOne(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID cannot be null or zero.");
        
        return _players.FirstOrDefault(p => p.Id == id);
    }
    
    public IEnumerable<Player> FindAll()
    {
        return _players;
    }
    
    public Player? Save(Player player)
    {
        if (player == null)
            throw new ArgumentException("Player cannot be null.");
        
        if (FindOne(player.Id) != null)
            return player;
        
        _players.Add(player);
        return null;
    }
    
    public Player? Delete(int id)
    {
        var player = FindOne(id);
        if (player == null)
            return null;
        
        _players.Remove(player);
        return player;
    }
    
    public Player? Update(Player player)
    {
        if (player == null)
            throw new ArgumentException("Player cannot be null.");
        
        var existing = FindOne(player.Id);
        if (existing == null)
            return player;
        
        existing.Name = player.Name;
        existing.School = player.School;
        existing.IdTeam = player.IdTeam;
        
        return null;
    }
    
    public void ReadFile()
    {
        var lines = File.ReadAllLines(
            "/Users/stefansandru/Documents/BasketSchools/BasketSchools/DataFiles/players.csv");
        foreach (var line in lines)
        {
            var values = line.Split(',');
            var player = new Player()
            {
                Id = int.Parse(values[0]),
                Name = (values[1]),
                School = (values[2]),
                IdTeam = int.Parse(values[3]),
            };
            _players.Add(player);
        }
    }   
}