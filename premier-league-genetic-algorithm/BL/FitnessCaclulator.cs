using premier_league_genetic_algorithm.BL.Constraints.HardConstraints;
using premier_league_genetic_algorithm.BL.Constraints.SoftConstraints;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL
{
    public class FitnessCaclulator
    {
        private List<HardConstraint> hardConstraints;
        private List<SoftConstraint> softConstraints;        

        public FitnessCaclulator(IEnumerable<Player> players)
        {            
            hardConstraints = initializeHardConstraints();
            softConstraints = initializeSoftConstraints(players);
        }

        public double CalculateFitness(IEnumerable<Player> players)
        {
            if (this.hardConstraints.Any(c => !c.ValidateSolution(players)))
                return 0;

            var fitness = this.softConstraints.Sum(c => c.CalculateFitness(players));

            if (fitness > 1)
                fitness = 1;

            if (fitness < 0)
                fitness = 0;

            return fitness;
        }


        public void PrintFitnessDetails(IEnumerable<Player> players)
        {
            this.hardConstraints.ForEach(c => Console.WriteLine("Constraint:{0}, Result:{1}", c.GetType().Name, c.ValidateSolution(players)));
            this.softConstraints.ForEach(c => Console.WriteLine("Constraint:{0}, Result:{1}", c.GetType().Name, c.CalculateFitness(players)));
        }

        private List<HardConstraint> initializeHardConstraints()
        {
            var config = new RoleCompositionConstraintConfig(2, 5, 5, 3);
            var roleComposition = new RoleCompositionConstraint(config);
            var teamComposition = new TeamCompositionConstraint(2);
            var teamCost = new TeamCostConstraint(1000);
            var playerUniqueness = new PlayerUniquenessConstraint();

            return new List<HardConstraint>() { roleComposition, teamComposition, teamCost, playerUniqueness };
        }

        private List<SoftConstraint> initializeSoftConstraints(IEnumerable<Player> players)
        {
            var playersIct = players.Select(p => Convert.ToDouble(p.ict_index));            
            var ictConstraint = new IctConstraint(0.25f, (float)minTeamValue(playersIct), (float)maxTeamValue(playersIct));

            var playersPointsPerGame = players.Select(p => p.points_per_game);
            var pointsPerGameConstraint = new PointsPerGameConstraint(0.25f, minTeamValue(playersPointsPerGame), maxTeamValue(playersPointsPerGame));

            var playersTotalPoints = players.Select(p => Convert.ToDouble(p.total_points));
            var totalPointsConstaint = new TotalPointsConstraint(0.5f, Convert.ToInt32(minTeamValue(playersTotalPoints)), Convert.ToInt32(maxTeamValue(playersTotalPoints)));

            return new List<SoftConstraint>() { ictConstraint, pointsPerGameConstraint, totalPointsConstaint };
        }

        private double maxTeamValue(IEnumerable<double> values)
        {
            return values.OrderByDescending(v => v).Take(15).Sum();
        }

        private double minTeamValue(IEnumerable<double> values)
        {
            return values.OrderBy(v => v).Take(15).Sum();
        }
    }
}
