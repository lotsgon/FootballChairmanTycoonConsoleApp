using System;

namespace FootballChairmanTycoonConsoleApp
{
    public static class MatchSimulation
    {
        public static MatchResult GetMatchResult(LeagueFixture fixture)
        {
            var matchScore = GetMatchScore(fixture);
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

        private static MatchScore GetMatchScore(LeagueFixture fixture)
        {
            var homeClub = fixture.HomeTeam;
            var awayClub = fixture.AwayTeam;

            homeClub.UpdateMatchDayLineUp();
            awayClub.UpdateMatchDayLineUp();

            var matchScore = new MatchScore(0, 0);
            var rand = new Random();

            var homeRate = (int)((homeClub.OverallLineUpRating / 10) + (homeClub.Manager.OverallRating / 10) + (homeClub.Morale / 2)) / 3;
            var awayRate = (int)((awayClub.OverallLineUpRating / 10) + (awayClub.Manager.OverallRating / 10) + (awayClub.Morale / 2)) / 3;

            if (homeRate > awayRate)
            {
                matchScore.HomeGoals += Math.Clamp((int)(Math.Floor((Math.Abs(rand.Next(0, homeRate + 2) - rand.Next(0, homeRate))) * (((rand.NextDouble() + rand.NextDouble()) * ((0.1f + 0.8f) + 0.3f))))), 0, 15);
                matchScore.AwayGoals += Math.Clamp((int)(Math.Floor((Math.Abs(rand.Next(0, awayRate) - rand.Next(0, awayRate))) * (((rand.NextDouble() + rand.NextDouble()) * ((0.1f + 0.5f) + 0.3f))))), 0, 15);
            }
            else if (awayRate > homeRate)
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
