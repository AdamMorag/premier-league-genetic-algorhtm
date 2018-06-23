using Newtonsoft.Json;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.Data
{
    public static class PlayerDataBase
    {
        public static Player[] Players;

        static PlayerDataBase()
        {
            using (StreamReader reader = new StreamReader("./Data/DataBase.json"))
            {
                var dbString = reader.ReadToEnd();

                Players = JsonConvert.DeserializeObject<Player[]>(dbString);                
            }
        }
    }
}
