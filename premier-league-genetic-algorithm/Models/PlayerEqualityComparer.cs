using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.Models
{
    public class PlayerEqualityComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player x, Player y)
        {
            return int.Equals(x.id, y.id);
        }

        public int GetHashCode(Player obj)
        {
            return obj.id.GetHashCode();
        }
    }
}
