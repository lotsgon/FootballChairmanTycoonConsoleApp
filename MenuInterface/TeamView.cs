using EasyConsole;
using FootballChairmanTycoonConsoleApp;

namespace Demo.Pages
{
    class TeamView : Page
    {
        private FootballClub Club;

        public TeamView(Program program, FootballClub club)
            : base($"{club.Name}", program)
        {
            this.Club = club;
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine($"Full Name: {Club.LongName}");
            Output.WriteLine($"Manager: {Club.Manager.FullName}");
            Output.WriteLine($"Year Founded: {Club.YearFounded}");
            Output.WriteLine($"Stadium: {Club.HomeStadium}");
            Output.WriteLine($"Stadium Capacity: {Club.HomeStadiumCapacity}");
            Output.WriteLine($"Value: {Club.Value:C2}");
            Output.WriteLine("Squad:");
            Club.ShowSquadList();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
