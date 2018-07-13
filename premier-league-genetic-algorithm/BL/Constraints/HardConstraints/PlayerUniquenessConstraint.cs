using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using premier_league_genetic_algorithm.Models;

namespace premier_league_genetic_algorithm.BL.Constraints.HardConstraints
{
    public class PlayerUniquenessConstraint : HardConstraint
    {
        public override bool ValidateSolution(IEnumerable<Player> players)
        {
            return players.Distinct(new PlayerEqualityComparer()).Count().Equals(players.Count());
        }
    }
}
