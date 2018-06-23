using Newtonsoft.Json;
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
    public class ExampleController : ApiController
    {
        [Route("HelloWorld")]
        public async Task<int> GetExample()
        {
            using (StreamReader reader = new StreamReader("./Data/DataBase.json"))
            {
                var dbString = await reader.ReadToEndAsync();

                var players = JsonConvert.DeserializeObject<GenericPlayer[]>(dbString);

                return players.Count();
            }
        }
    }
}
