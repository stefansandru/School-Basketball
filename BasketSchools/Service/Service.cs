using BasketSchools.Domain;
using BasketSchools.Repository;

namespace BasketSchools.Service;

public class Service
{ 
    private IRepository<int, ActivePlayer>  _activePlayerRepository;
    private IRepository<int, Match> _matchRepository;
    private IRepository<int, Player> _playerRepository;
    private IRepository<int, Team> _teamRepository;

    public Service(
        IRepository<int, ActivePlayer> activePlayerRepository,
        IRepository<int, Match> matchRepository,
        IRepository<int, Player> playerRepository,
        IRepository<int, Team> teamRepository)
    {
        _activePlayerRepository = activePlayerRepository;
        _matchRepository = matchRepository;
        _playerRepository = playerRepository;
        _teamRepository = teamRepository;
    }
    
    public IEnumerable<Player> GetTeamPlayers(int teamId)
    {
        var team = _teamRepository.FindOne(teamId);
        if (team == null)
            throw new ArgumentException("Team not found.");
        
        return _playerRepository.FindAll().Where(p => p.IdTeam == team.Id);
    }

    public IEnumerable<string> GetTeamPlayersOfAMatch(int matchId, int teamId) 
    {
        var match = _matchRepository.FindOne(matchId);
        if (match == null)
            throw new ArgumentException("Match not found.");
    
        var team = _teamRepository.FindOne(teamId);
        if (team == null)
            throw new ArgumentException("Team not found.");
    
        var teamPlayersId = _playerRepository.FindAll()
            .Where(player => player.IdTeam == teamId)
            .Select(player => player.Id)
            .ToList();
    
        var activePlayers = _activePlayerRepository.FindAll()
            .Where(ap => ap.IdMatch == matchId && teamPlayersId.Contains(ap.IdPlayer))
            .Join(_playerRepository.FindAll(),
                activePlayer => activePlayer.IdPlayer,
                player => player.Id,
                (activePlayer, player) => player.Name)
            .ToList();
        
        return activePlayers;
    }
    
    public IEnumerable<Match> GetMatchesInATimePeriod(DateTime start, DateTime end)
    {
        return _matchRepository.FindAll()
            .Where(match => match.Date >= start && match.Date <= end);
    }
    
    public Tuple<int, int> GetMatchScore(int matchId)
    {   
        var match = _matchRepository.FindOne(matchId);
        if (match == null)
            throw new ArgumentException("Match not found.");

        var activePlayers = _activePlayerRepository.FindAll()
    .Where(ap => ap.IdMatch == matchId)
    .Join(_playerRepository.FindAll(),
        activePlayer => activePlayer.IdPlayer,
        player => player.Id,
        (activePlayer, player) => new { player.IdTeam, activePlayer.Points })
    .Join(_teamRepository.FindAll(),
        ap => ap.IdTeam,
        team => team.Id,
        (ap, team) => new { TeamName = team.Name, ap.Points });
        
        var team1Score = activePlayers
            .Where(ap => ap.TeamName == match.HomeTeam)
            .Sum(ap => ap.Points);

        var team2Score = activePlayers
            .Where(ap => ap.TeamName == match.AwayTeam)
            .Sum(ap => ap.Points);

        return new Tuple<int, int>(team1Score, team2Score);
    }
}