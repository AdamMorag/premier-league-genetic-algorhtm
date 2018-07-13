using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using premier_league_genetic_algorithm.Models;

namespace premier_league_genetic_algorithm.BL.Constraints.SoftConstraints
{
    public class TotalPointsConstraint : SoftConstraint
    {
        private int minTotalPoints;
        private int maxTotalPoints;

        public TotalPointsConstraint(float weight, int minTotalPoints, int maxTotalPoints) : base(weight)
        {
            this.minTotalPoints = minTotalPoints;
            this.maxTotalPoints = maxTotalPoints;
        }

        protected override double CalculateRawFitness(IEnumerable<Player> players)
        {
            var totalPointsSum = players.Sum(p => p.total_points);

            return MathUtils.NormalizeData(totalPointsSum, minTotalPoints, maxTotalPoints);
        }
    }
}
