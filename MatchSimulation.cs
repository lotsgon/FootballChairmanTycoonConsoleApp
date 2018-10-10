using System;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp
{
    public static class MatchSimulation
    {
        public static MatchResult GetMatchResult(LeagueFixture fixture)
        {
            var homeClubOvr = fixture.HomeTeam.Squad.Select(x => x.OverallRating).Average();
            var awayClubOvr = fixture.AwayTeam.Squad.Select(x => x.OverallRating).Average();

            var matchScore = GetMatchScore(homeClubOvr, awayClubOvr);
            fixture.SetFixtureResult(matchScore.HomeGoals, matchScore.AwayGoals);

            SetFixtureResultRelatedStatistics(fixture);

            if (matchScore.HomeGoals > matchScore.AwayGoals)
            {
                return MatchResult.Win;
            }
            else if (matchScore.AwayGoals > matchScore.HomeGoals)
            {
                return MatchResult.Lose;
            }

            return MatchResult.Draw;
        }

        private static MatchScore GetMatchScore(double homeOvr, double awayOvr)
        {
            var matchScore = new MatchScore(0, 0);
            var rand = new Random();

            var homeRate = (int)homeOvr / 10;
            var awayRate = (int)awayOvr / 10;

            if (homeOvr > awayOvr)
            {
                matchScore.HomeGoals += Math.Clamp((int)(Math.Floor((Math.Abs(rand.Next(0, homeRate+2) - rand.Next(0, homeRate))) * (((rand.NextDouble() + rand.NextDouble()) * ((0.1f + 0.8f) + 0.3f))))), 0, 15);
                matchScore.AwayGoals += Math.Clamp((int)(Math.Floor((Math.Abs(rand.Next(0, awayRate) - rand.Next(0, awayRate))) * (((rand.NextDouble() + rand.NextDouble()) * ((0.1f + 0.5f) + 0.3f))))), 0, 15);
            }
            else if (awayOvr > homeOvr)
            {
                matchScore.HomeGoals += Math.Clamp((int)(Math.Floor((Math.Abs(rand.Next(0, homeRate) - rand.Next(0, homeRate))) * (((rand.NextDouble() + rand.NextDouble()) * ((0.1f + 0.4f) + 0.3f))))), 0, 15);
                matchScore.AwayGoals += Math.Clamp((int)(Math.Floor((Math.Abs(rand.Next(0, awayRate) - rand.Next(0, awayRate))) * (((rand.NextDouble() + rand.NextDouble()) * ((0.1f + 0.7f) + 0.3f))))), 0, 15);
            }
            else
            {
                matchScore.HomeGoals += Math.Clamp((int)(Math.Floor((Math.Abs(rand.Next(0, homeRate) - rand.Next(0, homeRate))) * (((rand.NextDouble() + rand.NextDouble()) * ((0.1f + 0.6f) + 0.3f))))), 0, 15);
                matchScore.AwayGoals += Math.Clamp((int)(Math.Floor((Math.Abs(rand.Next(0, awayRate) - rand.Next(0, awayRate))) * (((rand.NextDouble() + rand.NextDouble()) * ((0.1f + 0.6f) + 0.3f))))), 0, 15);
            }

            return matchScore;
        }

        private static void SetFixtureResultRelatedStatistics(LeagueFixture fixture)
        {
            var homeTeamStats = fixture.HomeTeam.Statistics;
            var awayTeamStats = fixture.AwayTeam.Statistics;

            homeTeamStats.MatchesPlayed += 1;
            homeTeamStats.GoalsFor += fixture.HomeGoals;
            homeTeamStats.GoalsAgainst += fixture.AwayGoals;
            homeTeamStats.UpdateGoalDifference();

            awayTeamStats.MatchesPlayed += 1;
            awayTeamStats.GoalsFor += fixture.AwayGoals;
            awayTeamStats.GoalsAgainst += fixture.HomeGoals;
            awayTeamStats.UpdateGoalDifference();

            if (fixture.HomeGoals > fixture.AwayGoals)
            {
                awayTeamStats.MatchesLost += 1;

                homeTeamStats.MatchesWon += 1;
                homeTeamStats.Points += 3;
            }
            else if (fixture.AwayGoals > fixture.HomeGoals)
            {
                homeTeamStats.MatchesLost += 1;

                awayTeamStats.MatchesWon += 1;
                awayTeamStats.Points += 3;
            }
            else
            {
                homeTeamStats.MatchesDrew += 1;
                homeTeamStats.Points += 1;

                awayTeamStats.MatchesDrew += 1;
                awayTeamStats.Points += 1;
            }
        }
    }
}
