using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.Models
{
    public class PlayerSimple
    {
        public string web_name { get; set; }
        public int now_cost { get; set; }
        public int total_points { get; set; }
        public float ict_index { get; set; }
        public double points_per_game { get; set; }
        public Role element_type { get; set; }
        public int team { get; set; }
    }
}
