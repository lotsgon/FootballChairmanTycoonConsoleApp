using System.Collections.Generic;

namespace FootballChairmanTycoonConsoleApp.JsonData
{
    public class FootballLeague
    {
        public string LeagueNation { get; private set; }
        public List<FootballClub> LeagueTeams { get; private set; }

        public FootballLeague(string leagueNation, List<FootballClub> leagueTeams)
        {
            this.LeagueNation = leagueNation;
            this.LeagueTeams = leagueTeams;
        }

    }
}
