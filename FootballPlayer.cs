namespace FootballChairmanTycoonConsoleApp
{
    public class FootballPlayer
    {

        public int ID { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string FullName { get; private set; }
        public string ShortName { get; private set; }
        public int Age { get; private set; }
        public string Nationality { get; private set; }
        public PlayerPosition Position { get; private set; }
        public int OverallRating { get; private set; }
        public int PotentialRating { get; private set; }
        public int CurrentClubID { get; private set; }
        public int Value { get; private set; }
        public int Wage { get; private set; }
        public int Reputation { get; private set; }

        public FootballPlayer(int playerID, string playerFirstName, string playerSurname, int playerAge, PlayerPosition playerPosition, int playerOverallRating, int playerPotentialRating, int playerCurrentClubID, int playerValue, int playerWage)
        {
            this.ID = playerID;
            this.FirstName = playerFirstName;
            this.Surname = playerSurname;
            this.FullName = playerFirstName + ' ' + playerSurname;
            this.ShortName = playerFirstName[0] + ". " + playerSurname;
            this.Age = playerAge;
            this.Position = playerPosition;
            this.OverallRating = playerOverallRating;
            this.PotentialRating = playerPotentialRating;
            this.CurrentClubID = playerCurrentClubID;
            this.Value = playerValue;
            this.Wage = playerWage;
        }

    }
}