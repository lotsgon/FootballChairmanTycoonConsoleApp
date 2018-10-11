using Demo.Pages;
using EasyConsole;
using FootballChairmanTycoonConsoleApp;
using System.Collections.Generic;

namespace Demo
{
    class DemoProgram : Program
    {
        public DemoProgram()
            : base("Football Chairman Tycoon", breadcrumbHeader: true)
        {
            List<FootballPlayer> playerList = new List<FootballPlayer>();
            var clubList = JsonReader.ReadJsonClubsFile();
            var managerList = JsonReader.ReadJsonManagersFile();

            foreach (FootballClub club in clubList)
            {
                playerList.AddRange(club.Squad);
                managerList.Add(club.Manager);
            }

            var league = new FootballLeague("England", clubList, "Premier League");

            AddPage(new MainPage(this));
            AddPage(new ViewTeams(this, clubList));
            AddPage(new Page1A(this));
            AddPage(new Page1Ai(this));
            AddPage(new Page1B(this));
            AddPage(new LeagueView(this, league));
            AddPage(new MatchRoundResults(this, league.Fixtures));

            SetPage<MainPage>();
        }
    }
}
