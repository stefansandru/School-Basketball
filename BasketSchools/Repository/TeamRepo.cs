using BasketSchools.Domain;

namespace BasketSchools.Repository;

public class TeamRepo : IRepository<int, Team>
{
    private readonly List<Team> _teams = new();

    public TeamRepo()
    {
        ReadFile();
    }
    
    public Team? FindOne(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID cannot be null or zero.");

        return _teams.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Team> FindAll()
    {
        return _teams;
    }

    public Team? Save(Team team)
    {
        if (team == null)
            throw new ArgumentException("Entity cannot be null.");

        if (FindOne(team.Id) != null)
            return team;

        _teams.Add(team);
        return null;
    }

    public Team? Delete(int id)
    {
        var team = FindOne(id);
        if (team == null)
            return null;

        _teams.Remove(team);
        return team;
    }

    public Team? Update(Team team)
    {
        if (team == null)
            throw new ArgumentException("Entity cannot be null.");

        var existing = FindOne(team.Id);
        if (existing == null)
            return team;

        existing.Name = team.Name;

        return null;
    }
    
    public void ReadFile()
    {
        var lines = File.ReadAllLines(
            "/Users/stefansandru/Documents/BasketSchools/BasketSchools/DataFiles/teams.csv");
        foreach (var line in lines)
        {
            var values = line.Split(',');
            var team = new Team
            {
                Id = int.Parse(values[0]),
                Name = (values[1]),
            };
            _teams.Add(team);
        }
    }
}