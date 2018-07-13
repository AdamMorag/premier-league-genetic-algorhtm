using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL.Constraints.HardConstraints
{
    public class RoleAmount
    {
        public int Min;
        public int Max;

        public RoleAmount(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }

        public bool IsValidAmount(int amount)
        {
            return amount >= this.Min && amount <= this.Max;
        }
    }

    public class RoleCompositionConstraintConfig
    {
        public Dictionary<Role, RoleAmount> config;

        public RoleCompositionConstraintConfig(int minGoalkeepers, int maxGoalkeepers,
            int minDefenders, int maxDefenders, int minMidfielders, int maxMidfielders,
            int minForwards, int maxForwards)
        {
            this.config = new Dictionary<Role, RoleAmount>()
            {
                { Role.Goalkeeper, new RoleAmount(minGoalkeepers, maxGoalkeepers) },
                { Role.Defender, new RoleAmount(minDefenders, maxDefenders) },
                { Role.Midfielder, new RoleAmount(minMidfielders, maxMidfielders) },
                { Role.Forward, new RoleAmount(minForwards, maxForwards) }
            };
        }
    }
}
