using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using premier_league_genetic_algorithm.Models;

namespace premier_league_genetic_algorithm.BL.Constraints.SoftConstraints
{
    public class IctConstraint : SoftConstraint
    {
        private float minIctValue;
        private float maxIctValue;

        public IctConstraint(float weight, float minIctValue, float maxIctValue) : base(weight)
        {
            this.minIctValue = minIctValue;
            this.maxIctValue = maxIctValue;
        }

        protected override double CalculateRawFitness(IEnumerable<Player> players)
        {
            var ictSum = players.Sum(p => p.ict_index);

            return MathUtils.NormalizeData(ictSum, minIctValue, maxIctValue);
        }
    }
}
