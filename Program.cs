using System;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var playerList = JsonReader.ReadJsonPlayersFile();
            var clubList = JsonReader.ReadJsonClubsFile();

            foreach(FootballClub club in clubList)
            {
                var squadList = playerList.Where(x => x.PlayerCurrentClubID.Equals(club.ClubID)).ToList();

                club.UpdateSquadList(squadList);
            }

            //Console.WriteLine(playerList[0].PlayerValue);

            //Console.WriteLine(clubList[0].ClubName);
            //foreach(FootballPlayer player in clubList[0].ClubSquad)
            //{
            //    Console.WriteLine(player.PlayerFullName);
            //    Console.WriteLine(player.PlayerAge);
            //    Console.WriteLine(player.PlayerOverallRating + "\n");
            //}

            var result = MatchSimulation.GetMatchResult(clubList[0], clubList[12]);

            Console.WriteLine($"You {result}!");

            Console.ReadLine();
        }
    }
}
