using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using premier_league_genetic_algorithm.Models;

namespace premier_league_genetic_algorithm.BL.Constraints.HardConstraints
{
    public class RoleCompositionConstraint : HardConstraint
    {
        private RoleCompositionConstraintConfig config;

        public RoleCompositionConstraint(RoleCompositionConstraintConfig config)
        {
            this.config = config;
        }

        public override bool ValidateSolution(IEnumerable<Player> players)
        {
            return players.GroupBy(p => p.element_type)
                .Any(roleGroup => validateRoleCount(roleGroup));
        }

        private bool validateRoleCount(IGrouping<Role, Player> roleGroup)
        {
            var roleCount = roleGroup.Count();

            return !(roleCount < this.config.config[roleGroup.Key].Min ||
                roleCount > this.config.config[roleGroup.Key].Max);
        }
    }
}
