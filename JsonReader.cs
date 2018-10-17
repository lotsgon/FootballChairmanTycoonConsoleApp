using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FootballChairmanTycoonConsoleApp
{
    public static class JsonReader
    {
        public static List<FootballPlayer> ReadJsonPlayersFile()
        {
            return JsonConvert.DeserializeObject<List<FootballPlayer>>(File.ReadAllText(@"..\\..\..\JsonData\Players.json"));
        }

        public static List<FootballManager> ReadJsonManagersFile()
        {
            return JsonConvert.DeserializeObject<List<FootballManager>>(File.ReadAllText(@"..\\..\..\JsonData\Managers.json"));
        }

         public static List<FootballChairman> ReadJsonChairmenFile()
        {
            return JsonConvert.DeserializeObject<List<FootballChairman>>(File.ReadAllText(@"..\\..\..\JsonData\Chairmen.json"));
        }

        public static FootballLeague ReadJsonLeaguesFile()
        {
            return JsonConvert.DeserializeObject<FootballLeague>(File.ReadAllText(@"..\\..\..\JsonData\EnglishPremierLeague.json"));
        }

        public static List<FootballClub> ReadJsonClubsFile()
        {
            var clubList = JsonConvert.DeserializeObject<List<FootballClub>>(File.ReadAllText(@"..\\..\..\JsonData\OtherClubsWithSquads.json"));
            return clubList;
        }
    }
} 

