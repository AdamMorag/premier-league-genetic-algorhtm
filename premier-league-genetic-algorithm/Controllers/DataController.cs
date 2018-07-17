using GAF;
using Newtonsoft.Json;
using premier_league_genetic_algorithm.BL;
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
        public object GetPerformance()
        {
            using (FileStream stream = new FileStream(@".\results\results.json", FileMode.Open))
            {
                StreamReader reader = new StreamReader(stream);

                return JsonConvert.DeserializeObject(reader.ReadToEnd());
            }
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
