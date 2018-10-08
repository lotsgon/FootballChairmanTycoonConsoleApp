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
    }
}
