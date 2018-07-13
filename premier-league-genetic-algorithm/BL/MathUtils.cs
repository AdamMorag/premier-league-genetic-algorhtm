using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL
{
    public static class MathUtils
    {
        public static double NormalizeData(double valueToNormalize, double minValueRange, double maxValueRange)
        {
            return (valueToNormalize - minValueRange) / (maxValueRange - minValueRange);
        }
    }
}
