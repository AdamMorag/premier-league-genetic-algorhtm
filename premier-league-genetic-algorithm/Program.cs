using MicroService4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace premier_league_genetic_algorithm
{
    class Program
    {
        static void Main(string[] args)
        {                 
            var microservice = new MicroService();                        
            microservice.Run(args);
        }
    }
}
