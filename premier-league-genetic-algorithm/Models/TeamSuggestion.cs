using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.Models
{
    public class TeamSuggestion
    {
        [JsonProperty(Order = 3)]
        public IEnumerable<PlayerSimple> Players { get; set; }

        [JsonProperty(Order = 0)]
        public double Cost { get; set; }

        [JsonProperty(Order = 1)]
        public double IctIndex { get; set; }

        [JsonProperty(Order = 2)]
        public double TotalPoints { get; set; }

        [JsonProperty(Order = 4)]
        public Dictionary<int, int> Teams { get; set; }
    }
}
