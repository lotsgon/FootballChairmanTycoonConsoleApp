﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FootballChairmanTycoonConsoleApp
{
    public class FootballClub
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string LongName { get; private set; }
        public string SixLetterName { get; private set; }
        public int LeagueID { get; private set; }
        public string Nation { get; private set; }
        public string City { get; private set; }
        public int YearFounded { get; private set; }
        public string Status { get; private set; }
        public string HomeStadium { get; private set; }
        public Vector3 TeamColour { get; private set; }
        public int HomeStadiumCapacity { get; private set; }
        public int Reputation { get; private set; }
        public long Money { get; private set; }
        public long Value { get; private set; }
        public int Morale { get; private set; }
        public List<FootballPlayer> Squad { get; private set; } = new List<FootballPlayer>();
        public ClubStatistics Statistics { get; private set; } = new ClubStatistics();
        public FootballManager Manager { get; private set; }
        public List<FootballPlayer> OverallLineUp { get; private set; } = new List<FootballPlayer>();
        public int OverallLineUpRating { get; private set; }
        public int SquadMinimum { get; private set; } = 16;
        public FootballChairman Chairman { get; private set; }
        public int BoardHappiness { get; private set; }

        public FootballClub(int iD, string name, string longName, string sixLetterName, string city, int leagueID, string nation, int yearFounded, string status, string homeStadium, Vector3 teamColour, int homeStadiumCapacity, List<FootballPlayer> squad, FootballManager manager, FootballChairman chairman)
        {
            this.ID = iD;
            this.Name = name;
            this.LongName = longName;
            this.SixLetterName = sixLetterName;
            this.City = city;
            this.LeagueID = leagueID;
            this.Nation = nation;
            this.YearFounded = yearFounded;
            this.Status = status;
            this.HomeStadium = homeStadium;
            this.TeamColour = teamColour;
            this.HomeStadiumCapacity = homeStadiumCapacity;
            this.Squad = squad;
            RegisterSquadToClub(this.Squad);
            this.Reputation = (int)Math.Min(Math.Round(this.HomeStadiumCapacity * 0.11f, 0), 10000);
            this.Money = this.HomeStadiumCapacity * 7000;
            this.Value = (this.Reputation * 75000) + this.Money;
            this.Morale = 10;
            this.BoardHappiness = 80;
            this.Manager = manager;
            this.Manager?.SetCurrentClub(this);
            this.Chairman = chairman;
            this.Chairman?.SetCurrentClub(this);
        }

        private void RegisterSquadToClub(List<FootballPlayer> squad)
        {
            foreach (FootballPlayer player in squad)
            {
                player.SetCurrentClub(this);
            }
        }

        public void UpdateSquadList(List<FootballPlayer> squad)
        {
            this.Squad = squad;
        }

        public void UpdateReputation(int value)
        {
            var updatedValue = (int)Math.Round(value * 0.11f, 0);

            Math.Max(Math.Min(this.Reputation += updatedValue, 10000),0);
        }

        public void UpdateManager(FootballManager manager)
        {
            this.Manager = manager;
            this.BoardHappiness = 80;
        }

        public void UpdateBoardHappiness(int value)
        {
            Math.Max(Math.Min(this.BoardHappiness += value, 100),0);
        }

        public void UpdateMorale(int value)
        {
            Math.Max(Math.Min(this.Morale += value, 20), 0);
        }

        public void UpdateChairman(FootballChairman chairman)
        {
            this.Chairman = chairman;
        }

        public void UpdateMoneyAndValue(long amount)
        {
            this.Value += amount;
            this.Money += amount;
        }

        public void UpdateValue(long amount)
        {
            this.Value += amount;
        }

        public void UpdateMoney(long amount)
        {
            this.Money += amount;
        }

        public void UpdateMatchDayLineUp()
        {
            OverallLineUp.Clear();

            var goal = this.Squad.Where(x => x.Position == PlayerPosition.GK).OrderByDescending(x => x.OverallRating).FirstOrDefault();

            var defence = this.Squad.Where(x => x.Position == PlayerPosition.RB || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.CB).OrderByDescending(x => x.OverallRating);

            for (int i = 0; i < this.Manager.Formation.Defence - 1; i++)
            {
                OverallLineUp.Add(defence.ElementAtOrDefault(i));
            }

            var midfield = this.Squad.Where(x => x.Position == PlayerPosition.RM || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.CM).OrderByDescending(x => x.OverallRating);

            for (int i = 0; i < this.Manager.Formation.Midfield - 1; i++)
            {
                OverallLineUp.Add(defence.ElementAtOrDefault(i));
            }

            var attack = this.Squad.Where(x => x.Position == PlayerPosition.ST).OrderByDescending(x => x.OverallRating);

            for (int i = 0; i < this.Manager.Formation.Attack - 1; i++)
            {
                OverallLineUp.Add(defence.ElementAtOrDefault(i));
            }

            OverallLineUpRating = (int)OverallLineUp.Select(x => x.OverallRating).Average();
        }

        public void ShowSquadList()
        {
            var orderedSquad = Squad.OrderByDescending(x => x.Position).ThenBy(x => x.OverallRating).ThenBy(x => x.Age);

            Console.WriteLine($"{Name}\n");
            Console.WriteLine("{0,0}{1,10}{2,20}{3,10}",
                              "Pos",
                              "Age",
                              "Name",
                              "Ovr");

            foreach (FootballPlayer player in orderedSquad)
            {
                Console.WriteLine("{0,0} {1,10}{2,20}{3,10}",
                              player.Position,
                              player.Age,
                              player.ShortName,
                              player.OverallRating);
            }
        }

        public void ResetSeasonStats()
        {
            this.Statistics.Points = 0;
            this.Statistics.GoalsAgainst = 0;
            this.Statistics.GoalsFor = 0;
            this.Statistics.MatchesDrew = 0;
            this.Statistics.MatchesLost = 0;
            this.Statistics.MatchesWon = 0;
            this.Statistics.MatchesPlayed = 0;
            this.Statistics.UpdateGoalDifference(); ;
        }

    }
}
