using System.Collections.Generic;

namespace FootballChairmanTycoonConsoleApp
{
    public class FootballClub
    {
        public int ClubID { get; private set; }
        public string ClubName { get; private set; }
        public string ClubNameAbreviation { get; private set; }
        public string LeagueId { get; private set; }
        public string ClubLocation { get; private set; }
        public string ClubStadium { get; private set; }
        public string ClubStadiumCapacity { get; private set; }
        public List<FootballPlayer> ClubSquad { get; private set; } = new List<FootballPlayer>();

        public FootballClub(int clubID, string clubName, string clubNameAbreviation, List<FootballPlayer> clubSquad)
        {
            this.ClubID = clubID;
            this.ClubName = clubName;
            this.ClubNameAbreviation = clubNameAbreviation;
            this.ClubSquad = clubSquad;
        }

        public void UpdateSquadList(List<FootballPlayer> clubSquad)
        {
            this.ClubSquad = clubSquad;
        }

    }
}
