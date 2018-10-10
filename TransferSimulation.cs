using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballChairmanTycoonConsoleApp
{
    public static class TransferSimulation
    {
        public static void SimulateTransferDay(List<FootballClub> clubList, List<FootballPlayer> playerList)
        {
            foreach(FootballClub club in clubList)
            {
                var rand = new Random();

                for(int i =0; i < rand.Next(0,3); i++)
                {
                    BuyPlayer(club, clubList, playerList);
                }
            }

            UpdateSquadsAfterTransfers(clubList, playerList);
        }

        private static void BuyPlayer(FootballClub club, List<FootballClub> clubList, List<FootballPlayer> playerList)
        {
            // update to 200 if player overall goes to 200
            var clubRep = (club.Reputation / 100) + 5;
            var transferBudget = club.Money * 0.75;

            var targetPlayer = playerList.Where(x => club.ID != x.CurrentClubID && x.OverallRating < clubRep && !x.JustMoved).FirstOrDefault();

            if (targetPlayer == null)
            {
                return;
            }

            int targetPlayerValue = (int)(targetPlayer.Value * 1.2);

            if(targetPlayerValue < transferBudget)
            {
                var sellingClub = clubList.Where(x => x.ID == targetPlayer.CurrentClubID).First();

                targetPlayer.UpdateCurrentClub(club.ID);
                club.UpdateMoneyAndValue(-targetPlayerValue);
                sellingClub.UpdateMoneyAndValue(targetPlayerValue);
            }

        }

        private static void UpdateSquadsAfterTransfers(List<FootballClub> clubList, List<FootballPlayer> playerList)
        {
            foreach (FootballClub club in clubList)
            {
                var squadList = playerList.Where(x => x.CurrentClubID.Equals(club.ID)).ToList();

                club.UpdateSquadList(squadList);
            }
        }
    }
}
