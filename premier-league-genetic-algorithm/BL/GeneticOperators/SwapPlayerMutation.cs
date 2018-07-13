using GAF;
using GAF.Operators;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL.GeneticOperators
{
    public class SwapPlayerMutation : MutateBase
    {
        private Player[] players;
        private Random rnd;
        private Dictionary<Role, List<Player>> groupedPlayers;

        public SwapPlayerMutation(double mutationProbability, Player[] players, Dictionary<Role, List<Player>> groupedPlayers) : base(mutationProbability)
        {
            this.players = players;
            this.groupedPlayers = groupedPlayers;
            rnd = new Random();
        }

        protected override void MutateGene(Gene gene)
        {
            var player = this.players[(int)gene.ObjectValue];
            var rndPlayerIndexForRole = rnd.Next(0, this.groupedPlayers[player.element_type].Count);
            var newPlayer = this.groupedPlayers[player.element_type].ElementAt(rndPlayerIndexForRole);
            var newPlayerRealIndex = Array.IndexOf(this.players, newPlayer);
            gene.ObjectValue = newPlayerRealIndex;
        }
    }
}
