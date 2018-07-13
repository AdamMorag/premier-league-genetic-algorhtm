using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL.Constraints
{
    public abstract class Constraint
    {
        public abstract double CalculateFitness(IEnumerable<Player> players);
    }
}
