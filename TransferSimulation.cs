using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp
{
    public static class TransferSimulation
    {
        public static void SimulateTransferDay(List<FootballClub> clubList, List<FootballPlayer> playerList)
        {
            foreach (FootballClub club in clubList.Where(x=> x.ID != 0))
            {
                var rand = new Random().Next(0, 3);

                for (int i = 0; i < rand; i++)
                {
                    BuyPlayer(club, clubList, playerList);
                }
            }
        }

        private static void BuyPlayer(FootballClub club, List<FootballClub> clubList, List<FootballPlayer> playerList)
        {
            // update to 200 if player overall goes to 200
            var clubRep = (club.Reputation / 100) + 5;
            var transferBudget = club.Money * 0.75;

            var rand = new Random().Next(0, 300);

            var targetPlayer = playerList.Where(x => club != x.CurrentClub && x.OverallRating < clubRep && !x.JustMoved).ElementAtOrDefault(rand);

            if (targetPlayer == null)
            {
                return;
            }

            var sellingClub = targetPlayer.CurrentClub;

            if (targetPlayer.CurrentClub.ID == 0)
            {
                targetPlayer.UpdateCurrentClub(club);

                club.UpdateValue(targetPlayer.Value / 2);
                club.Squad.Add(targetPlayer);

                sellingClub.Squad.Remove(targetPlayer);
                return;
            }

            if (sellingClub.Squad.Count <= sellingClub.SquadMinimum)
            {
                return;
            }

            var validGK = sellingClub.Squad.Count(x => x.Position == PlayerPosition.GK) <= 2;
            var validDF = sellingClub.Squad.Count(x => x.Position == PlayerPosition.RB || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.LB) <= 5;
            var validMF = sellingClub.Squad.Count(x => x.Position == PlayerPosition.RM || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.LM) <= 6;
            var validST = sellingClub.Squad.Count(x => x.Position == PlayerPosition.ST) <= 3;

            if (!validGK || !validDF || !validMF || !validST)
            {
                return;
            }

            int targetPlayerValue = (int)(targetPlayer.Value * 1.2);

            if (targetPlayerValue < transferBudget)
            {
                targetPlayer.UpdateCurrentClub(club);

                club.UpdateMoneyAndValue(-targetPlayerValue);
                club.Squad.Add(targetPlayer);

                sellingClub.UpdateMoneyAndValue(targetPlayerValue);
                sellingClub.Squad.Remove(targetPlayer);
            }

        }

        public static void ShowWeeklyTransfers(List<FootballPlayer> playerList)
        {
            var playersMoved = playerList.Where(x => x.JustMoved);

            foreach (FootballPlayer player in playersMoved)
            {
                Console.WriteLine("{0,30}{1,30}{2,10}{3,10}{4,10}{5,10}{6,10}",
                              player.ShortName,
                              player.CurrentClub.Name,
                              player.PreviousClub.Name,
                              player.Position,
                              player.Value,
                              player.OverallRating,
                              player.JustMoved);
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

        public static void UpdateSquadsForTransferWindow(List<FootballPlayer> playerList)
        {
            foreach (FootballPlayer player in playerList.Where(x => x.JustMoved))
            {
                player.UpdateJustMoved();
            }
        }
    }
}
