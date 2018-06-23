using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace premier_league_genetic_algorithm.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        Goalkeeper = 1,
        Defender = 2,
        Midfielder = 3,
        Forward = 4
    }
}
