using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL.Performance.ChartJSPerformanceMonitor
{
    public class ChartData
    {
        public int[] labels { get; set; }
        public Dataset[] datasets { get; set; }
    }

    public class Dataset
    {
        public double[] data { get; set; }
        public string label { get; set; }
        public string borderColor { get; set; }
        public bool fill { get; set; }
    }

}
