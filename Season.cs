using EasyConsole;
using System.Collections.Generic;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp
{
    public class Season
    {
        public List<FootballPlayer> PlayerList { get; private set; } = new List<FootballPlayer>();
        public List<FootballClub> ClubList { get; private set; } = JsonReader.ReadJsonClubsFile();
        public List<FootballManager> ManagerList { get; private set; } = JsonReader.ReadJsonManagersFile();
        public FootballLeague League { get; private set; } = JsonReader.ReadJsonLeaguesFile();
        public int Week { get; private set; } = 1;
        public int Year { get; private set; } = 2018;
        public int WeekMax { get; private set; } = 52;
        public int SummerTransferWindowStart { get; private set; } = 1;
        public int SummerTransferWindowEnd { get; private set; } = 7;
        public int WinterTransferWindowStart { get; private set; } = 29;
        public int WinterTransferWindowEnd { get; private set; } = 32;

        public Season()
        {
            ClubList.AddRange(League.Teams);

            foreach (FootballClub club in this.ClubList)
            {
                this.PlayerList.AddRange(club.Squad);
                this.ManagerList.Add(club.Manager);
            }
        }

        public void ProgressToNextWeek()
        {
            if (Week == WeekMax)
            {
                ResetSeason();
                return;
            }

            if (Week == 29)
            {
                TransferSimulation.UpdateSquadsForTransferWindow(PlayerList);
                this.Year += 1;
            }

            if (Week >= SummerTransferWindowStart && Week <= SummerTransferWindowEnd || Week >= WinterTransferWindowStart && Week <= WinterTransferWindowEnd)
            {
                TransferSimulation.SimulateTransferDay(ClubList, PlayerList);
                TransferSimulation.ShowWeeklyTransfers(PlayerList);
            }

            var leagueStart = League.Fixtures.First().SeasonWeek;
            var leagueEnd = League.Fixtures.Last().SeasonWeek;

            if (Week >= leagueStart && Week <= leagueEnd)
            {
                SimulateMatchWeek();
            }

            this.Week += 1;
        }

        private void ResetSeason()
        {
            this.Week = 1;
            League.Fixtures.Clear();
            League.GenerateLeagueFixtures();
            foreach(FootballClub club in League.Teams)
            {
                club.ResetSeasonStats();
            }
        }

        private void SimulateMatchWeek()
        {
            var fixtureRound = League.Fixtures.Where(x => x.SeasonWeek == Week).FirstOrDefault();

            Output.WriteLine($"\nMatch Round {fixtureRound.LeagueRound}\n");

            foreach (LeagueFixture fixture in fixtureRound.LeagueRoundFixtures)
            {
                var homeTeam = fixture.HomeTeam.Name;
                var awayTeam = fixture.AwayTeam.Name;
                var result = MatchSimulation.GetMatchResult(fixture);

                Output.WriteLine($"{homeTeam} {fixture.HomeGoals} - {fixture.AwayGoals} {awayTeam}");
            }

            Output.WriteLine("\n");
        }
    }
}
