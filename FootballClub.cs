using System.Collections.Generic;
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
        public int Morale { get; private set; }
        public List<FootballPlayer> Squad { get; private set; } = new List<FootballPlayer>();
        public ClubStatistics Statistics { get; private set; } = new ClubStatistics();

        public FootballClub(int iD, string name, string longName, string sixLetterName, string city, int leagueID, string nation, int yearFounded, string status, string homeStadium, Vector3 teamColour, int homeStadiumCapacity, List<FootballPlayer> squad)
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
            this.Reputation = 0;
            this.Morale = 10;
        }

        public void UpdateSquadList(List<FootballPlayer> squad)
        {
            this.Squad = squad;
        }

    }
}
