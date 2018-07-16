using GAF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace premier_league_genetic_algorithm.BL.Performance
{
    public abstract class PerformanceMonitor
    {
        public abstract void LogPerformance(GaEventArgs generationCompleteEvent);

        public abstract void SavePerformanceLog(string pathToFolder);
    }
}
