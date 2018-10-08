﻿using FootballChairmanTycoonConsoleApp.JsonData;
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

            var league = new FootballLeague("England", clubList);

            Console.WriteLine(league.LeagueFixtures.Count);

            foreach (LeagueFixtureRound fixtureRound in league.LeagueFixtures)
            {
                Console.WriteLine($"\nMatch Round {fixtureRound.LeagueRound}\n");

                foreach (LeagueFixture fixture in fixtureRound.LeagueRoundFixtures)
                {
                    var homeTeam = fixture.HomeTeam.ClubName;
                    var awayTeam = fixture.AwayTeam.ClubName;
                    var result = MatchSimulation.GetMatchResult(fixture);

                    Console.WriteLine($"{homeTeam} {fixture.HomeGoals} - {fixture.AwayGoals} {awayTeam}");
                }
            }

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
}
