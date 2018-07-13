using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.Models
{
    public class TeamSuggestion
    {
        public IEnumerable<PlayerSimple> Players { get; set; }
        public double Cost { get; set; }
        public double IctIndex { get; set; }
        public double TotalPoints { get; set; }
    }
}
