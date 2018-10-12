using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp
{
    public class FootballLeague
    {
        public string Nation { get; private set; }
        public string Name { get; private set; }
        public List<FootballClub> Teams { get; private set; }
        public List<LeagueFixtureRound> Fixtures { get; set; }

        public FootballLeague(string nation, List<FootballClub> teams, string name)
        {
            this.Name = name;
            this.Nation = nation;
            this.Teams = teams;
            GenerateLeagueFixtures();
        }

        public void GenerateLeagueFixtures()
        {
            ShuffleTeamList(this.Teams);

            int halfLeagueRounds = this.Teams.Count - 1;
            int matchesPerWeek = this.Teams.Count / 2;

            List<LeagueFixtureRound> sortedLeagueFixtures = new List<LeagueFixtureRound>();

            for (int leagueRound = 1; leagueRound <= halfLeagueRounds; leagueRound++)
            {
                sortedLeagueFixtures.Add(new LeagueFixtureRound(GetUniqueHomeFixtureRound(leagueRound, matchesPerWeek), leagueRound, leagueRound + 6));
            }

            for (int leagueRound = 1; leagueRound <= halfLeagueRounds; leagueRound++)
            {
                sortedLeagueFixtures.Add(new LeagueFixtureRound(GetReverseAwayFixtureRound(sortedLeagueFixtures[leagueRound - 1].LeagueRoundFixtures), leagueRound + halfLeagueRounds, leagueRound + halfLeagueRounds + 6));
            }

            ShuffleFixtureList(sortedLeagueFixtures);

            this.Fixtures = sortedLeagueFixtures;
        }

        private List<LeagueFixture> GetUniqueHomeFixtureRound(int fixtureRound, int matchesPerWeek)
        {
            List<LeagueFixture> roundFixtures = new List<LeagueFixture>();

            var clubs = matchesPerWeek * 2;

            for (int weeklyMatchNum = 1; weeklyMatchNum <= matchesPerWeek; weeklyMatchNum++)
            {
                if (weeklyMatchNum == 1)
                {
                    roundFixtures.Add(new LeagueFixture(Teams[0], Teams[(fixtureRound + clubs - weeklyMatchNum - 1) % (clubs - 1) + 1]));
                }
                else
                {
                    roundFixtures.Add(new LeagueFixture(Teams[(fixtureRound + weeklyMatchNum - 2) % (clubs - 1) + 1], Teams[(fixtureRound + clubs - weeklyMatchNum - 1) % (clubs - 1) + 1]));
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
            var orderedLeague = Teams.OrderByDescending(x => x.Statistics.Points).ThenByDescending(x => x.Statistics.GoalDifference).ThenByDescending(x => x.Statistics.GoalsFor);

            Console.WriteLine($"{Name}\n");
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
                var clubStats = club.Statistics;

                Console.WriteLine("{0,10} {1,30}{2,10}{3,10}{4,10}{5,10}{6,10}{7,10}{8,10}{9,10}",
                              position,
                              club.Name,
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

        private void ShuffleFixtureList(List<LeagueFixtureRound> fixtureList)
        {
            int n = (fixtureList.Count / 2) - 1;
            int k = 0;
            while (n > k)
            {
                LeagueFixtureRound value = fixtureList[k];
                fixtureList[k] = fixtureList[n + k + 1];
                fixtureList[n + k + 1] = value;

                k += 2;
            }

            for (int i = 0; i < 38; i++)
            {
                fixtureList[i].SetRoundAndWeek(i+1);
            }
        }
    }
}
