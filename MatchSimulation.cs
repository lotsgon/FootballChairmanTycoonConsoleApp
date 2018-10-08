using System;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp
{
    public static class MatchSimulation
    {
        public static MatchResult GetMatchResult(LeagueFixture fixture)
        {
            var homeClubOvr = fixture.HomeTeam.ClubSquad.Select(x => x.PlayerOverallRating).Average();
            var awayClubOvr = fixture.AwayTeam.ClubSquad.Select(x => x.PlayerOverallRating).Average();

            var matchScore = GetMatchScore(homeClubOvr, awayClubOvr);
            fixture.SetFixtureResult(matchScore.HomeGoals, matchScore.AwayGoals);

            SetFixtureResultRelatedStatistics(fixture);

            if(matchScore.HomeGoals > matchScore.AwayGoals)
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

            if (homeOvr > awayOvr)
            {
                matchScore.HomeGoals += rand.Next(0, 8);
                matchScore.AwayGoals += rand.Next(0, 5);
            }
            else if (awayOvr > homeOvr)
            {
                matchScore.HomeGoals += rand.Next(0, 4);
                matchScore.AwayGoals += rand.Next(0, 7);
            }
            else
            {
                matchScore.HomeGoals += rand.Next(0, 5);
                matchScore.AwayGoals += rand.Next(0, 5);
            }

            return matchScore;
        }

        private static void SetFixtureResultRelatedStatistics(LeagueFixture fixture)
        {
            var homeTeamStats = fixture.HomeTeam.ClubStatistics;
            var awayTeamStats = fixture.AwayTeam.ClubStatistics;

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
