using EasyConsole;
using FootballSimulationGameLibrary;

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
            Output.WriteLine($"Chairman: {Club.Chairman.FullName}");
            Output.WriteLine($"Chairman Fortune: {Club.Chairman.PersonalFortune:C2}");
            Output.WriteLine("Squad:");

            var orderedSquad = Club.GetSquadList();

            Output.WriteLine("{0,0}{1,10}{2,20}{3,10}",
                              "Pos",
                              "Age",
                              "Name",
                              "Ovr");

            foreach (FootballPlayer player in orderedSquad)
            {
                Output.WriteLine("{0,0} {1,10}{2,20}{3,10}",
                              player.Position,
                              player.Age,
                              player.ShortName,
                              player.OverallRating);
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
