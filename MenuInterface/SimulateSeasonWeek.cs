using EasyConsole;
using FootballChairmanTycoonConsoleApp;

namespace Demo.Pages
{
    class SimulateSeasonWeek : Page
    {
        private Season CurrentSeason;

        public SimulateSeasonWeek(Program program, Season season)
            : base("Simulate Season Week", program)
        {
            this.CurrentSeason = season;
        }

        public override void Display()
        {
            base.Display();

            CurrentSeason.ProgressToNextWeek();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
