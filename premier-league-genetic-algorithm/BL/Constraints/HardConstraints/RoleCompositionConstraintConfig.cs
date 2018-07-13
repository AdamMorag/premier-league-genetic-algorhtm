using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL.Constraints.HardConstraints
{
    public class RoleCompositionConstraintConfig
    {
        public Dictionary<Role, int> config;

        public RoleCompositionConstraintConfig(int numOfGoalkeepers, int numOfDefenders,
            int numOfMidfielders, int numOfForwards)
        {
            this.config = new Dictionary<Role, int>()
            {
                { Role.Goalkeeper, numOfGoalkeepers },
                { Role.Defender, numOfDefenders },
                { Role.Midfielder, numOfMidfielders },
                { Role.Forward, numOfForwards }
            };
        }
    }
}
