using GAF;
using Newtonsoft.Json;
using premier_league_genetic_algorithm.BL;
using premier_league_genetic_algorithm.BL.Performance.ChartJSPerformanceMonitor;
using premier_league_genetic_algorithm.Data;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace premier_league_genetic_algorithm.Controllers
{
    [RoutePrefix("Data")]
    public class DataController : ApiController
    {
        [Route("GetRoleDistribution")]
        public Dictionary<Role, int> GetRoleDistribution()
        {
            return PlayerDataBase.Players.GroupBy(p => p.element_type)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Count());
        }
        
        [Route("GetPlayerByName/{name}")]
        public Player GetPlayerByName(string name)
        {
            return PlayerDataBase.Players.FirstOrDefault(p => name.Equals(p.web_name));
        }

        [Route("GetTeamDistribution")]
        public Dictionary<int,IEnumerable<string>> GetTeamDistribution()
        {
            return PlayerDataBase.Players.GroupBy(p => p.team).OrderBy(kvp => kvp.Key)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Select(p => p.web_name));
        }

        [Route("GetPerformanceData")]
        public ChartData GetPerformance()
        {
            ChartData finalData = new ChartData()
            {
                labels = new int[] { },
                datasets = new Dataset[] {}
            };

            HashSet<int> allLabels = new HashSet<int>();
            List<Dataset> allDataSets = new List<Dataset>();

            ChartData tempData;

            foreach (var file in Directory.GetFiles(@".\results", "results_*.json", SearchOption.TopDirectoryOnly))
            {
                using (FileStream stream = new FileStream(file, FileMode.Open))
                {
                    StreamReader reader = new StreamReader(stream);

                    tempData = JsonConvert.DeserializeObject<ChartData>(reader.ReadToEnd());

                    foreach (var label in tempData.labels)
                    {
                        allLabels.Add(label);
                    }

                    allDataSets.Add(tempData.datasets[0]);
                    allDataSets.Add(tempData.datasets[1]);
                }
            }

            finalData.labels = allLabels.ToArray<int>();
            finalData.datasets = allDataSets.ToArray<Dataset>();

            return finalData;
        }

        [Route("GetPerformanceReport")]
        public HttpResponseMessage GetPerformanceReport()
        {
            var path = @".\BL\Performance\ChartJSPerformanceMonitor\PerformanceChart.html";
            var response = new HttpResponseMessage();
            response.Content = new StringContent(File.ReadAllText(path));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
