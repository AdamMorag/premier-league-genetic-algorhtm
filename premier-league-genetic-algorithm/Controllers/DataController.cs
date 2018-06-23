using Newtonsoft.Json;
using premier_league_genetic_algorithm.Data;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace premier_league_genetic_algorithm.Controllers
{
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
    }
}
