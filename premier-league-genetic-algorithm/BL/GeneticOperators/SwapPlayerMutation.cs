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

        public SwapPlayerMutation(double mutationProbability, Player[] players) : base(mutationProbability)
        {
            this.players = players;
            rnd = new Random();
        }

        protected override void MutateGene(Gene gene)
        {
            gene.ObjectValue = rnd.Next(0, this.players.Count());
        }
    }
}
