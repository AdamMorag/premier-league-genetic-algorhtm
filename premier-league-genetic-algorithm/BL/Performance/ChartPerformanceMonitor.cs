using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace premier_league_genetic_algorithm.BL.Performance
{
    public class ChartPerformanceMonitor : PerformanceMonitor
    {        
        private Chart chart;        

        public ChartPerformanceMonitor()
        {            
            this.chart = createNewChart();
        }

        public override void LogPerformance(GaEventArgs generationCompleteEvent)
        {
            var fitness = generationCompleteEvent.Population.MaximumFitness;

            chart.Series["fitness"].Points.AddXY(generationCompleteEvent.Generation, fitness);
        }

        public override void SavePerformanceLog(string pathToFolder)
        {
            var lastPoint = chart.Series["fitness"].Points.Last();
            lastPoint.Label = lastPoint.YValues[0].ToString();

            chart.SaveImage(Path.Combine(pathToFolder, "fitness.png"), System.Drawing.Imaging.ImageFormat.Png);
        }

        private Chart createNewChart()
        {
            Chart chart = new Chart();
            chart.Size = new System.Drawing.Size(640, 320);
            chart.ChartAreas.Add("ChartArea1");
            chart.Legends.Add("legend1");
            chart.ChartAreas["ChartArea1"].AxisX.Title = "Generations";
            chart.ChartAreas["ChartArea1"].AxisY.Title = "Fitness";


            chart.Series.Add("fitness");
            chart.Series["fitness"].LegendText = "Fitness";
            chart.Series["fitness"].ChartType = SeriesChartType.Spline;
            return chart;
        }
    }
}
