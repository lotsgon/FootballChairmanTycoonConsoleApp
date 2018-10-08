using System.Collections.Generic;

namespace FootballChairmanTycoonConsoleApp
{
    public class LeagueFixtureRound
    {
        public List<LeagueFixture> LeagueRoundFixtures { get; private set; }
        public int LeagueRound { get; private set; }

        public LeagueFixtureRound(List<LeagueFixture> leagueRoundFixtures, int leagueRound)
        {
            this.LeagueRoundFixtures = leagueRoundFixtures;
            this.LeagueRound = leagueRound;
        }
    }
}
