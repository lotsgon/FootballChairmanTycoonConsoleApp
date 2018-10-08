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

        public FootballLeague(string leagueNation, List<FootballClub> leagueTeams)
        {
            this.LeagueNation = leagueNation;
            this.LeagueTeams = leagueTeams;
            this.LeagueFixtures = this.GenerateLeagueFixtures(LeagueTeams);
        }

        private List<LeagueFixtureRound> GenerateLeagueFixtures(List<FootballClub> leagueTeams)
        {
            List<LeagueFixture> fixtures = new List<LeagueFixture>();

            for (int homeTeamID = 0; homeTeamID < leagueTeams.Count; homeTeamID++)
            {
                for (int awayTeamID = 0; awayTeamID < leagueTeams.Count; awayTeamID++)
                {
                    var homeTeam = leagueTeams[homeTeamID];
                    var awayTeam = leagueTeams[awayTeamID];

                    if (homeTeam != awayTeam)
                    {
                        fixtures.Add(new LeagueFixture(homeTeam, awayTeam));
                    }
                }
            }

            fixtures.Reverse();

            int leagueRounds = (leagueTeams.Count-1) * 2;
            int matchesPerWeek = leagueTeams.Count / 2;

            List<LeagueFixtureRound> sortedLeagueFixtures = new List<LeagueFixtureRound>();

            for (int leagueRound = 0; leagueRound < leagueRounds; leagueRound++)
            {
                List<LeagueFixture> roundFixtures = new List<LeagueFixture>();

                sortedLeagueFixtures.Add(new LeagueFixtureRound(GetUniqueFixtureRound(fixtures, matchesPerWeek), leagueRound + 1));
            }

            return sortedLeagueFixtures;
        }

        private List<LeagueFixture> GetUniqueFixtureRound(List<LeagueFixture> leagueFixtures, int matchesPerWeek)
        {
            List<LeagueFixture> roundFixtures = new List<LeagueFixture>();

            for (int weeklyMatch = 0; weeklyMatch < matchesPerWeek; weeklyMatch++)
            {
                for (int leagueFixtureCount = leagueFixtures.Count -1; leagueFixtureCount >= 0; leagueFixtureCount--)
                {
                    if (!roundFixtures.Any(roundFixture => roundFixture.HomeTeam == leagueFixtures[leagueFixtureCount].HomeTeam || roundFixture.AwayTeam == leagueFixtures[leagueFixtureCount].HomeTeam || roundFixture.HomeTeam == leagueFixtures[leagueFixtureCount].AwayTeam || roundFixture.AwayTeam == leagueFixtures[leagueFixtureCount].AwayTeam))
                    {
                        roundFixtures.Add(leagueFixtures[leagueFixtureCount]);
                        leagueFixtures.RemoveAt(leagueFixtureCount);
                    }
                }
            }

            return roundFixtures;
        }
    }
}
