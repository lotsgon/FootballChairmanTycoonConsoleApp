using FootballChairmanTycoonConsoleApp.JsonData;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace FootballChairmanTycoonConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var playerList = JsonReader.ReadJsonPlayersFile();
            var clubList = JsonReader.ReadJsonClubsFile();

            foreach (FootballClub club in clubList)
            {
                var squadList = playerList.Where(x => x.CurrentClubID.Equals(club.ID)).ToList();

                club.UpdateSquadList(squadList);
            }

            var league = new FootballLeague("England", clubList, "Premier League");

            TransferSimulation.SimulateTransferDay(clubList, playerList);

            var playersMoved = playerList.Where(x => x.JustMoved);

            foreach (FootballPlayer player in playersMoved)
            {
                Console.WriteLine("{0,30}{1,30}{2,10}{3,10}{4,10}{5,10}{6,10}",
                              player.ShortName,
                              player.CurrentClubID,
                              player.PreviousClubID,
                              player.Position,
                              player.Value,
                              player.OverallRating,
                              player.JustMoved);
            }

            //Console.WriteLine(league.Fixtures.Count);

            ////////////////////////////////////////////////////////////////////////
            // SEASON SIMULATION
            ////////////////////////////////////////////////////////////////////////
            //foreach (LeagueFixtureRound fixtureRound in league.Fixtures)
            //{
            //    Console.WriteLine($"\nMatch Round {fixtureRound.LeagueRound}\n");

            //    foreach (LeagueFixture fixture in fixtureRound.LeagueRoundFixtures)
            //    {
            //        var homeTeam = fixture.HomeTeam.Name;
            //        var awayTeam = fixture.AwayTeam.Name;
            //        var result = MatchSimulation.GetMatchResult(fixture);

            //        Console.WriteLine($"{homeTeam} {fixture.HomeGoals} - {fixture.AwayGoals} {awayTeam}");
            //    }
            //}

            ////////////////////////////////////////////////////////////////////////
            // DEBUG FIXTURE CHECKER
            ////////////////////////////////////////////////////////////////////////
            //foreach (FootballClub club in clubList)
            //{

            //    foreach (FootballClub club2 in clubList)
            //    {
            //        var count = 0;
            //        foreach (LeagueFixtureRound fixtureRound in league.Fixtures)
            //        {
            //            foreach (LeagueFixture fixture in fixtureRound.LeagueRoundFixtures)
            //            {
            //                var homeTeam = fixture.HomeTeam.ID;
            //                var awayTeam = fixture.AwayTeam.ID;

            //                if (homeTeam.Equals(club.ID) && awayTeam.Equals(club2.ID))
            //                {
            //                    count++;
            //                }

            //                //Console.WriteLine($"{homeTeam} {fixture.HomeGoals} - {fixture.AwayGoals} {awayTeam}");
            //            }
            //        }
            //        //Console.WriteLine($"{club.ClubName} Matches Against {club2.ClubName} - {count}");
            //    }
            //}

            //league.LeagueStandings();

            //Console.WriteLine(playerList[0].PlayerValue);

            //Console.WriteLine(clubList[0].ClubName);
            //foreach (FootballPlayer player in clubList[0].ClubSquad)
            //{
            //    Console.WriteLine(player.PlayerFullName);
            //    Console.WriteLine(player.PlayerAge);
            //    Console.WriteLine(player.PlayerOverallRating + "\n");
            //}

            //var result = MatchSimulation.GetMatchResult(clubList[0], clubList[12]);
            //Console.WriteLine($"You {result}!");

            //var result2 = MatchSimulation.GetMatchResult(clubList[4], clubList[17]);
            //Console.WriteLine($"You {result2}!");

            Console.ReadLine();
        }
    }

    public static class Win32
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, short attributes);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);
    }
}
