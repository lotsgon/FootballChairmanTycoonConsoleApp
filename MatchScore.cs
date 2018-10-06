using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FootballChairmanTycoonConsoleApp
{
    public class MatchScore
    {
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public MatchScore(int homeGoals, int awayGoals)
        {
            this.HomeGoals = homeGoals;
            this.AwayGoals = awayGoals;
        }
    }
}
