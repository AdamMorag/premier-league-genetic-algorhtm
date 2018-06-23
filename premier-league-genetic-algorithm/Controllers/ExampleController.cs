using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace premier_league_genetic_algorithm.Controllers
{
    public class ExampleController : ApiController
    {
        [Route("HelloWorld")]
        public string GetExample()
        {
            return "hello world";
        }
    }
}
