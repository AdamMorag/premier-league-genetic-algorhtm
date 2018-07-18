using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;

namespace premier_league_genetic_algorithm.BL.Performance.ChartJSPerformanceMonitor
{
    public class ChartJsPerformanceMonitor : PerformanceMonitor
    {
        private Dictionary<int, Tuple<double, double>> performanceData;
        private string runId;
        private Random randomGen;

        public ChartJsPerformanceMonitor(string runId)
        {
            this.performanceData = new Dictionary<int, Tuple<double, double>>();
            this.runId = runId;
            this.randomGen = new Random();
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

            File.WriteAllText(Path.Combine(pathToFolder, string.Format(@"results_{0}.json", this.runId)), serializedChartData);

        }

        private ChartData getChartData()
        {
            var color1 = getRandomColor();
            var color2 = getRandomColor();

            return new ChartData()
            {
                labels = this.performanceData.Keys.ToArray(),
                datasets = new Dataset[]
                { new Dataset()
                    {
                        label = string.Format("{0}-Max Fitness", this.runId),
                        borderColor = color1,
                        fill = true,
                        data = this.performanceData.Values.Select(v => v.Item1).ToArray()
                    },
                    new Dataset()
                    {
                        label = string.Format("{0}-Avg Fitness", this.runId),
                        borderColor = color2,
                        fill = true,
                        data = this.performanceData.Values.Select(v => v.Item2).ToArray()
                    }
                }
            };
        }

        private string getRandomColor()
        {
            return String.Format("#{0:X6}", randomGen.Next(0x1000000));
            /*KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            return Color.FromKnownColor(randomColorName);*/
        }
    }
}
