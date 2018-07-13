using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using premier_league_genetic_algorithm.Models;

namespace premier_league_genetic_algorithm.BL.Constraints.SoftConstraints
{
    public class PointsPerGameConstraint : SoftConstraint
    {
        private double minPointsPerGame;
        private double maxPointsPerGame;

        public PointsPerGameConstraint(float weight, double minPointsPerGame, double maxPointsPerGame) : base(weight)
        {
            this.minPointsPerGame = minPointsPerGame;
            this.maxPointsPerGame = maxPointsPerGame;
        }

        protected override double CalculateRawFitness(IEnumerable<Player> players)
        {
            var pointsPerGameSum = players.Sum(p => p.points_per_game);
            return MathUtils.NormalizeData(pointsPerGameSum, minPointsPerGame, maxPointsPerGame);
        }
    }
}
