using EasyConsole; using FootballChairmanTycoonConsoleApp;

namespace Demo.Pages
{
    class LeagueView : Page
    {
        private FootballLeague League;

        public LeagueView(Program program, FootballLeague league)
            : base($"{league.Name} Standings", program)
        {
            this.League = league;
        }

        public override void Display()
        {
            base.Display();

            League.LeagueStandings();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
