using EasyConsole;

namespace Demo.Pages
{
    class MainPage : MenuPage
    {
        public MainPage(Program program)
            : base("Main Page", program,
                  new Option("View Teams", () => program.NavigateTo<ViewTeams>()),
                  new Option("View League", () => program.NavigateTo<LeagueView>()),
                  new Option("Sim Match Week", () => program.NavigateTo<MatchRoundResults>()),
                  new Option("Exit", () => System.Environment.Exit(0)))
        {
        }
    }
}
