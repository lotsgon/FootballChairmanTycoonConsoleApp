using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp.JsonData
{
    public class FootballLeague
    {
        public string LeagueNation { get; private set; }
        public string LeagueName { get; private set; }
        public List<FootballClub> LeagueTeams { get; private set; }
        public List<LeagueFixtureRound> LeagueFixtures { get; set; }

        public FootballLeague(string leagueNation, List<FootballClub> leagueTeams, string leagueName)
        {
            this.LeagueName = leagueName;
            this.LeagueNation = leagueNation;
            this.LeagueTeams = leagueTeams;
            this.LeagueFixtures = this.GenerateLeagueFixtures(LeagueTeams);
        }

        private List<LeagueFixtureRound> GenerateLeagueFixtures(List<FootballClub> leagueTeams)
        {
            ShuffleTeamList(leagueTeams);

            int halfLeagueRounds = leagueTeams.Count - 1;
            int matchesPerWeek = leagueTeams.Count / 2;

            List<LeagueFixtureRound> sortedLeagueFixtures = new List<LeagueFixtureRound>();

            for (int leagueRound = 1; leagueRound <= halfLeagueRounds; leagueRound++)
            {
                sortedLeagueFixtures.Add(new LeagueFixtureRound(GetUniqueHomeFixtureRound(leagueRound, matchesPerWeek), leagueRound));
            }

            for (int leagueRound = 1; leagueRound <= halfLeagueRounds; leagueRound++)
            {
                sortedLeagueFixtures.Add(new LeagueFixtureRound(GetReverseAwayFixtureRound(sortedLeagueFixtures[leagueRound - 1].LeagueRoundFixtures), leagueRound + halfLeagueRounds));
            }

            return sortedLeagueFixtures;
        }

        private List<LeagueFixture> GetUniqueHomeFixtureRound(int fixtureRound, int matchesPerWeek)
        {
            List<LeagueFixture> roundFixtures = new List<LeagueFixture>();

            var clubs = matchesPerWeek * 2;

            for (int weeklyMatchNum = 1; weeklyMatchNum <= matchesPerWeek; weeklyMatchNum++)
            {
                if (weeklyMatchNum == 1)
                {
                    roundFixtures.Add(new LeagueFixture(LeagueTeams[0], LeagueTeams[(fixtureRound + clubs - weeklyMatchNum - 1) % (clubs - 1) + 1]));
                }
                else
                {
                    roundFixtures.Add(new LeagueFixture(LeagueTeams[(fixtureRound + weeklyMatchNum - 2) % (clubs - 1) + 1], LeagueTeams[(fixtureRound + clubs - weeklyMatchNum - 1) % (clubs - 1) + 1]));
                }
            }

            return roundFixtures;
        }

        private List<LeagueFixture> GetReverseAwayFixtureRound(List<LeagueFixture> leagueFixtures)
        {
            List<LeagueFixture> roundFixtures = new List<LeagueFixture>();

            foreach (LeagueFixture fixture in leagueFixtures)
            {
                var homeTeam = fixture.HomeTeam;
                var awayTeam = fixture.AwayTeam;

                roundFixtures.Add(new LeagueFixture(awayTeam, homeTeam));
            }

            return roundFixtures;
        }

        public void LeagueStandings()
        {
            var orderedLeague = LeagueTeams.OrderByDescending(x => x.ClubStatistics.Points).ThenByDescending(x => x.ClubStatistics.GoalDifference).ThenByDescending(x => x.ClubStatistics.GoalsFor);

            Console.WriteLine($"{LeagueName}\n");
            Console.WriteLine("{0,10}{1,30}{2,10}{3,10}{4,10}{5,10}{6,10}{7,10}{8,10}{9,10}",
                              "Pos",
                              "Club",
                              "MP",
                              "W",
                              "D",
                              "L",
                              "GF",
                              "GA",
                              "GD",
                              "Pts");

            var position = 1;

            foreach (FootballClub club in orderedLeague)
            {
                var clubStats = club.ClubStatistics;


                Console.WriteLine("{0,10} {1,30}{2,10}{3,10}{4,10}{5,10}{6,10}{7,10}{8,10}{9,10}",
                              position,
                              club.ClubName,
                              clubStats.MatchesPlayed,
                              clubStats.MatchesWon,
                              clubStats.MatchesDrew,
                              clubStats.MatchesLost,
                              clubStats.GoalsFor,
                              clubStats.GoalsAgainst,
                              clubStats.GoalDifference,
                              clubStats.Points);

                position++;
            }
        }

        private void ShuffleTeamList(List<FootballClub> teamList)
        {
            Random rng = new Random();

            int n = teamList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                FootballClub value = teamList[k];
                teamList[k] = teamList[n];
                teamList[n] = value;
            }
        }
    }
}
