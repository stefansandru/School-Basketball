using BasketSchools.Domain;
using BasketSchools.Repository;
using BasketSchools.Service;

// todo: read and write data from/to files

ActivePlayerRepo activePlayerRepo = new ActivePlayerRepo();
MatchRepo matchRepo = new MatchRepo();
PlayerRepo playerRepo = new PlayerRepo();
TeamRepo teamRepo = new TeamRepo();

Service service  =  new Service(
    activePlayerRepo, matchRepo, playerRepo, teamRepo);

int choice = 0;
while (true)
{
    Console.Write("1. Find Team Players\n" +
                  "2. Find Team Players of a Match\n" +
                  "3. Find Match in a Time Period\n" +
                  "4. Get Match Score\n" +
                  ">>>");
    choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            try
            {
                Console.Write("Enter Team ID: ");
                int teamId = int.Parse(Console.ReadLine());
                service.GetTeamPlayers(teamId).ToList().ForEach(Console.WriteLine);
            } catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            break;

        case 2:
            try
            {
                Console.Write("Enter Match ID: ");
                int matchId = int.Parse(Console.ReadLine());
                Console.Write("Enter Team ID: "); 
                int teamId2 = int.Parse(Console.ReadLine());
                service.GetTeamPlayersOfAMatch(matchId, teamId2).
                    ToList()
                    .ForEach(playerName => Console.WriteLine(playerName.ToString()));
            }catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            break;

        case 3:
            try
            {
                 Console.Write("Enter Start Date: ");
                 DateTime startDate = DateTime.Parse(Console.ReadLine());
                 Console.Write("Enter End Date: ");
                 DateTime endDate = DateTime.Parse(Console.ReadLine());
                 service.GetMatchesInATimePeriod(startDate, endDate).ToList().ForEach(Console.WriteLine);
            } catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        
        case 4:
            try
            {
                Console.Write("Enter Match ID: ");
                int matchId2 = int.Parse(Console.ReadLine());
                Console.WriteLine(service.GetMatchScore(matchId2));
            } catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}