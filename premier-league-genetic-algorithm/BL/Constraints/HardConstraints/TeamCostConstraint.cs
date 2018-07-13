using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using premier_league_genetic_algorithm.Models;

namespace premier_league_genetic_algorithm.BL.Constraints.HardConstraints
{
    public class TeamCostConstraint : HardConstraint
    {
        private readonly int MAX_TEAM_COST;

        public TeamCostConstraint(int maxTeamCost)
        {
            MAX_TEAM_COST = maxTeamCost;
        }

        public override bool ValidateSolution(IEnumerable<Player> players)
        {
            throw new NotImplementedException();
        }
    }
}
