using EasyConsole;
using FootballSimulationGameLibrary;

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

            var orderedLeague = League.GetLeagueStandings();

            Output.WriteLine($"{League.Name}\n");
            Output.WriteLine("{0,10}{1,30}{2,10}{3,10}{4,10}{5,10}{6,10}{7,10}{8,10}{9,10}",
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

                Output.WriteLine("{0,10} {1,30}{2,10}{3,10}{4,10}{5,10}{6,10}{7,10}{8,10}{9,10}",
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

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
