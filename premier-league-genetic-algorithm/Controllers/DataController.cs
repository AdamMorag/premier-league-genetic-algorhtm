using Newtonsoft.Json;
using premier_league_genetic_algorithm.BL;
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

        [Route("GetSuggestion")]
        public TeamSuggestion GetSuggestion()
        {
            var players = PlayerDataBase.Players;
            var algorithm = new FantasyGeneticAlgorithm(players);            

            var teamPlayers = algorithm.FindSolution().Select(p => new PlayerSimple()
            {
                web_name = p.web_name,
                ict_index = p.ict_index,
                now_cost = p.now_cost,
                points_per_game = p.points_per_game,
                total_points = p.total_points,
                element_type = p.element_type,
                team = p.team
            }).OrderBy(p => p.element_type);

            return new TeamSuggestion()
            {
                Players = teamPlayers,
                Cost = teamPlayers.Sum(p => p.now_cost),
                IctIndex = teamPlayers.Sum(p => p.ict_index),
                TotalPoints = teamPlayers.Sum(p => p.total_points)
            };
        }
    }
}
