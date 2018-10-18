using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballChairmanTycoonConsoleApp
{
    public static class TakeoverSimulation
    {
        public static void SimulateTakeovers(List<FootballClub> clubList, List<FootballChairman> chairmanList)
        {
            var takeoverList = clubList.Where(x => x.ID != 0 && x.Chairman.Happiness < 40);

            if (takeoverList.FirstOrDefault() == null)
            {
                return;
            }

            foreach (FootballClub club in takeoverList)
            {
                var rand = new Random().Next(0, 2);

                for (int i = 0; i < rand; i++)
                {
                    ChairmanTakeover(club, chairmanList);
                }
            }
        }

        private static void ChairmanTakeover(FootballClub club, List<FootballChairman> chairmanList)
        {
            var value = club.Value;

            var rand = new Random().Next(0, chairmanList.Count - 1);

            var targetChairman = chairmanList.Where(x => x.CurrentClub == null).ElementAtOrDefault(rand);

            if (targetChairman == null)
            {
                return;
            }

            if (club.Value * 1.2 < targetChairman.PersonalFortune)
            {
                chairmanList.Add(club.Chairman);
                club.UpdateMoneyAndValue(-club.Money / 4);
                club.Chairman.UpdatePersonalFinance(club.Value + (club.Money / 4));
                club.Chairman.UpdateCurrentClub(null);

                targetChairman.UpdateCurrentClub(club);
                club.UpdateMoneyAndValue((int)(targetChairman.PersonalFortune * 0.25));
                targetChairman.UpdatePersonalFinance(-club.Value);
                chairmanList.Remove(targetChairman);

                club.UpdateChairman(targetChairman);
            }

        }

        public static void ShowTakeovers(List<FootballChairman> chairmanList)
        {
            var chairmanMoved = chairmanList.Where(x => x.JustMoved);

            foreach (FootballChairman chairman in chairmanMoved)
            {
                Console.WriteLine("{0,30}{1,30}{2,10}{3,10}{4,10}{5,10}{6,10}",
                              chairman.ShortName,
                              chairman.Happiness,
                              chairman.CurrentClub != null ? chairman.CurrentClub.Name : "Free Agent",
                              chairman.PreviousClub != null ? chairman.PreviousClub.Name : "Free Agent",
                              chairman.Type,
                              chairman.OverallRating,
                              chairman.JustMoved);
            }
        }
    }
}
