using EasyConsole;
using FootballChairmanTycoonConsoleApp;
using System.Collections.Generic;

namespace Demo.Pages
{
    class MatchRoundResults : Page
    {
        private LeagueFixtureRound FixtureRound;

        public MatchRoundResults(Program program, List<LeagueFixtureRound> fixtures)
            : base("MatchRoundResults", program)
        {
            this.FixtureRound = fixtures[0];
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine($"\nMatch Round {FixtureRound.LeagueRound}\n");

            foreach (LeagueFixture fixture in FixtureRound.LeagueRoundFixtures)
            {
                var homeTeam = fixture.HomeTeam.Name;
                var awayTeam = fixture.AwayTeam.Name;
                var result = MatchSimulation.GetMatchResult(fixture);

                Output.WriteLine($"{homeTeam} {fixture.HomeGoals} - {fixture.AwayGoals} {awayTeam}");
            }

            Output.WriteLine("\n");
            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
