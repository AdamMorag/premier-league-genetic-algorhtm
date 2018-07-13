using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using premier_league_genetic_algorithm.Models;

namespace premier_league_genetic_algorithm.BL.Constraints.HardConstraints
{
    public abstract class HardConstraint : Constraint
    {
        public override double CalculateFitness(IEnumerable<Player> players)
        {
            return Convert.ToDouble(this.ValidateSolution(players)); 
        }

        public abstract bool ValidateSolution(IEnumerable<Player> players);
    }
}
