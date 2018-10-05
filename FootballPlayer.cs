namespace FootballChairmanTycoonConsoleApp
{
    public class FootballPlayer
    {

        public int PlayerID { get; private set; }
        public string PlayerFirstName { get; private set; }
        public string PlayerSurname { get; private set; }
        public string PlayerFullName { get; private set; }
        public int PlayerAge { get; private set; }
        public string PlayerNationality { get; private set; }
        public PlayerPosition PlayerPosition { get; private set; }
        public int PlayerOverallRating { get; private set; }
        public int PlayerPotentialRating { get; private set; }
        public int PlayerCurrentClubID { get; private set; }
        public int PlayerValue { get; private set; }
        public int PlayerWage { get; private set; }

        public FootballPlayer(int playerID, string playerFirstName, string playerSurname, int playerAge, PlayerPosition playerPosition, int playerOverallRating, int playerPotentialRating, int playerCurrentClubID, int playerValue, int playerWage)
        {
            this.PlayerID = playerID;
            this.PlayerFirstName = playerFirstName;
            this.PlayerSurname = playerSurname;
            this.PlayerFullName = playerFirstName + ' ' + playerSurname;
            this.PlayerAge = playerAge;
            this.PlayerPosition = playerPosition;
            this.PlayerOverallRating = playerOverallRating;
            this.PlayerPotentialRating = playerPotentialRating;
            this.PlayerCurrentClubID = playerCurrentClubID;
            this.PlayerValue = playerValue;
            this.PlayerWage = playerWage;
        }

    }
}