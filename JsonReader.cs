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

        public static List<FootballClub> ReadJsonClubsFile()
        {
            return JsonConvert.DeserializeObject<List<FootballClub>>(File.ReadAllText(@"..\\..\..\JsonData\EnglishFakeClubsWithSquads1.json"));
        }
    }
} 

