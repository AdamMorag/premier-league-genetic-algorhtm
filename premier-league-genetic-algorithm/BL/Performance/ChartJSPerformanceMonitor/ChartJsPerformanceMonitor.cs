using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using Newtonsoft.Json;
using System.IO;

namespace premier_league_genetic_algorithm.BL.Performance.ChartJSPerformanceMonitor
{
    public class ChartJsPerformanceMonitor : PerformanceMonitor
    {
        private Dictionary<int, Tuple<double, double>> performanceData;

        public ChartJsPerformanceMonitor()
        {
            this.performanceData = new Dictionary<int, Tuple<double, double>>();
        }

        public override void LogPerformance(GaEventArgs generationCompleteEvent)
        {
            this.performanceData[generationCompleteEvent.Generation] = 
                new Tuple<double, double>(generationCompleteEvent.Population.MaximumFitness, generationCompleteEvent.Population.AverageFitness);
        }

        public override void SavePerformanceLog(string pathToFolder)
        {
            var chartData = getChartData();

            var serializedChartData = JsonConvert.SerializeObject(chartData);            

            File.WriteAllText(Path.Combine(pathToFolder, @"results.json"), serializedChartData);

        }

        private ChartData getChartData()
        {
            return new ChartData()
            {
                labels = this.performanceData.Keys.ToArray(),
                datasets = new Dataset[]
                { new Dataset()
                    {
                        label = "Max Fitness",
                        borderColor = "#3e95cd",
                        fill = true,
                        data = this.performanceData.Values.Select(v => v.Item1).ToArray()
                    },
                    new Dataset()
                    {
                        label = "Avg Fitness",
                        borderColor = "#123411",
                        fill = true,
                        data = this.performanceData.Values.Select(v => v.Item2).ToArray()
                    }
                }
            };
        }
    }
}
