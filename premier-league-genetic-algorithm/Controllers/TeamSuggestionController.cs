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
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace premier_league_genetic_algorithm.Controllers
{
    [RoutePrefix("TeamSuggestion")]
    public class TeamSuggestionController : ApiController
    {        
        [Route("GetSuggestion")]
        public TeamSuggestion GetSuggestion([FromUri]int populationSize, [FromUri]int amountOfGenerations)
        {            

            var players = PlayerDataBase.Players;
            var algorithm = new FantasyGeneticAlgorithm(players);

            return convertSolution(algorithm.FindSolution(populationSize, amountOfGenerations));
        }        

        private TeamSuggestion convertSolution(IEnumerable<Player> players)
        {
            var teamPlayers = players.Select(p => new PlayerSimple()
            {
                web_name = p.web_name,
                ict_index = p.ict_index,
                now_cost = p.now_cost,
                points_per_game = p.points_per_game,
                total_points = p.total_points,
                element_type = p.element_type,
                team = p.team
            }).OrderBy(p => p.element_type).ThenBy(p => p.web_name);

            return new TeamSuggestion()
            {
                Players = teamPlayers,
                Cost = teamPlayers.Sum(p => p.now_cost),
                IctIndex = teamPlayers.Sum(p => p.ict_index),
                TotalPoints = teamPlayers.Sum(p => p.total_points),
                Teams = teamPlayers.GroupBy(p => p.team).ToDictionary(g => g.Key, g => g.Count())
            };
        }
    }
}
