using EasyConsole;
using FootballSimulationGameLibrary;

namespace Demo.Pages
{
    class MainPage : MenuPage
    {
        Season Season;

        public MainPage(Program program, Season season)
            : base($"Week - Year", program,
                  new Option("View Teams", () => program.NavigateTo<ViewTeams>()),
                  new Option("View League", () => program.NavigateTo<LeagueView>()),
                  new Option("Simulate Season Week", () => program.NavigateTo<SimulateSeasonWeek>()),
                  new Option("Exit", () => System.Environment.Exit(0)))
        {
            this.Season = season;
        }

        public override void Display()
        {
            this.Title = $"Week {Season.Week} - {Season.Year}";

            base.Display();
        }


    }
}
