using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using premier_league_genetic_algorithm.Models;

namespace premier_league_genetic_algorithm.BL.Constraints.HardConstraints
{

    public class TeamCompositionConstraint : HardConstraint
    {
        private readonly int MAX_PLAYERS_FROM_EACH_TEAM;

        public TeamCompositionConstraint(int maxPlayersFromEachTeam)
        {
            MAX_PLAYERS_FROM_EACH_TEAM = maxPlayersFromEachTeam;
        }

        public override bool ValidateSolution(IEnumerable<Player> players)
        {
            return !players.GroupBy(p => p.team_code)
                .Any(teamGroup => teamGroup.Count() > MAX_PLAYERS_FROM_EACH_TEAM);
        }
    }
}
