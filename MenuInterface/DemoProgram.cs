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
            var season = new Season();

            AddPage(new MainPage(this, season));
            AddPage(new ViewTeams(this, season.ClubList));
            AddPage(new LeagueView(this, season.League));
            AddPage(new SimulateSeasonWeek(this, season));

            SetPage<MainPage>();
        }
    }
}
