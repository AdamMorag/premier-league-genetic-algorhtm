using GAF;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL
{
    public class ChromosomeUtils
    {
        private Player[] players;
        private Random rnd;
        private Dictionary<Role, List<Player>> groupedPlayers;

        public ChromosomeUtils(Player[] players, Dictionary<Role, List<Player>> groupedPlayers)
        {
            this.players = players;
            this.groupedPlayers = groupedPlayers;
            this.rnd = new Random();
        }

        public IEnumerable<Player> ConvertChromosome(Chromosome chromsome)
        {
            return chromsome.Genes.Select(g => this.players[(int)g.ObjectValue]);
        }

        public Chromosome GenerateRandomSolution()
        {
            var chromosome = new Chromosome();

            // Goalkeepers
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Goalkeeper)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Goalkeeper)));

            // Defenders
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Defender)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Defender)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Defender)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Defender)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Defender)));

            // Midfielders
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Midfielder)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Midfielder)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Midfielder)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Midfielder)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Midfielder)));

            // Forwards
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Forward)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Forward)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(Role.Forward)));
            
            return chromosome;
        }

        private Player getRandomPlayer(Role role)
        {
            var rndPlayerIndexForRole = rnd.Next(0, this.groupedPlayers[role].Count);
            return this.groupedPlayers[role].ElementAt(rndPlayerIndexForRole);
        }

        private int getRandomPlayerIndex(Role role)
        {
            var player = this.getRandomPlayer(role);
            return Array.IndexOf(this.players, player);
        }
    }
}
