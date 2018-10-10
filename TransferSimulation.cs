using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp
{
    public static class TransferSimulation
    {
        public static void SimulateTransferDay(List<FootballClub> clubList, List<FootballPlayer> playerList)
        {
            foreach(FootballClub club in clubList)
            {
                var rand = new Random().Next(0,3);

                for(int i =0; i < rand; i++)
                {
                    BuyPlayer(club, clubList, playerList);
                }
            }
        }

        private static void BuyPlayer(FootballClub club, List<FootballClub> clubList, List<FootballPlayer> playerList)
        {
            // update to 200 if player overall goes to 200
            var clubRep = (club.Reputation / 100)+5;
            var transferBudget = club.Money * 0.75;

            var rand = new Random().Next(0, 300);

            var targetPlayer = playerList.Where(x => club != x.CurrentClub && x.OverallRating < clubRep && !x.JustMoved).ElementAtOrDefault(rand);

            if (targetPlayer == null)
            {
                return;
            }

            int targetPlayerValue = (int)(targetPlayer.Value * 1.2);

            if(targetPlayerValue < transferBudget)
            {
                var sellingClub = targetPlayer.CurrentClub;

                targetPlayer.UpdateCurrentClub(club);

                club.UpdateMoneyAndValue(-targetPlayerValue);
                club.Squad.Add(targetPlayer);

                sellingClub.UpdateMoneyAndValue(targetPlayerValue);
                sellingClub.Squad.Remove(targetPlayer);
            }

        }

        private static void UpdateSquadsAfterTransfers(List<FootballClub> clubList, List<FootballPlayer> playerList)
        {
            foreach (FootballClub club in clubList)
            {
                var squadList = playerList.Where(x => x.CurrentClub.Equals(club.ID)).ToList();

                club.UpdateSquadList(squadList);
            }
        }
    }
}
