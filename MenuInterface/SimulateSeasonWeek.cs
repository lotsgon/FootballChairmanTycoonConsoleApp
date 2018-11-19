using EasyConsole;
using FootballSimulationGameLibrary;

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

            var fixtureRound = CurrentSeason.League.GetMatchWeekFixtures(CurrentSeason.Week);

            if (fixtureRound.LeagueRound > 0)
            {
                Output.WriteLine($"\nMatch Round {fixtureRound.LeagueRound}\n");
            }

            foreach (LeagueFixture fixture in fixtureRound.LeagueRoundFixtures)
            {
                var homeTeam = fixture.HomeTeam.Name;
                var awayTeam = fixture.AwayTeam.Name;
                MatchSimulation.GetMatchResult(fixture);

                Output.WriteLine($"{homeTeam} {fixture.HomeGoals} - {fixture.AwayGoals} {awayTeam}");
            }

            Output.WriteLine("\n");
            Output.WriteLine("Takeovers");

            var chairmanMoved = TakeoverSimulation.GetTakeovers(CurrentSeason.ChairmenList);

            foreach (FootballChairman chairman in chairmanMoved)
            {
                Output.WriteLine("{0,30}{1,30}{2,10}{3,10}{4,10}{5,10}{6,10}",
                              chairman.ShortName,
                              chairman.Happiness,
                              chairman.CurrentClub != null ? chairman.CurrentClub.Name : "Free Agent",
                              chairman.PreviousClub != null ? chairman.PreviousClub.Name : "Free Agent",
                              chairman.Type,
                              chairman.OverallRating,
                              chairman.JustMoved);
            }

            Output.WriteLine("\n");
            Output.WriteLine("Transfers");

            var playersMoved = TransferSimulation.GetWeeklyTransfers(CurrentSeason.PlayerList);

            foreach (FootballPlayer player in playersMoved)
            {
                Output.WriteLine("{0,30}{1,30}{2,10}{3,10}{4,10}{5,10}{6,10}",
                              player.ShortName,
                              player.CurrentClub.Name,
                              player.PreviousClub.Name,
                              player.Position,
                              player.Value,
                              player.OverallRating,
                              player.JustMoved);
            }

            Output.WriteLine("\n");
            Output.WriteLine("Manager Movement");

            var managerMovements = TransferManagerSimulation.GetManagerChanges(CurrentSeason.ManagerList);

            foreach (FootballManager manager in managerMovements)
            {
                Output.WriteLine("{0,30}{1,30}{2,10}{3,10}{4,10}{5,10}{6,10}",
                              manager.ShortName,
                              manager.CurrentClub != null ? manager.CurrentClub.Name : "Free Agent",
                              manager.PreviousClub != null ? manager.PreviousClub.Name : "Free Agent",
                              manager.OverallRating,
                              manager.JustMoved);
            }

            Output.WriteLine("\n");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
