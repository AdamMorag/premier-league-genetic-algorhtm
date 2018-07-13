using GAF;
using GAF.Extensions;
using GAF.Operators;
using premier_league_genetic_algorithm.BL.GeneticOperators;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.BL
{
    public class FantasyGeneticAlgorithm
    {
        private Player[] players;
        private FitnessCaclulator calculator;
        private Dictionary<Role, List<Player>> groupedPlayers;

        public FantasyGeneticAlgorithm(Player[] players)
        {
            this.players = players;
            this.groupedPlayers = players.GroupBy(p => p.element_type).ToDictionary(g => g.Key, x => x.ToList());
            this.calculator = new FitnessCaclulator(players);
        }

        public IEnumerable<Player> FindSolution()
        {
            const double crossoverProbability = 0.65;
            const double mutationProbability = 0.08;
            const int elitismPercentage = 5;

            //create the population
            var population = new Population();

            Random rnd = new Random();

            //create the chromosomes
            for (var p = 0; p < 1000; p++)
            {
                Chromosome chromosome = createRandomChromosome(rnd);
                population.Solutions.Add(chromosome);
            }


            //create the genetic operators 
            var elite = new Elite(elitismPercentage);

            var crossover = new Crossover(crossoverProbability, true)
            {
                CrossoverType = CrossoverType.SinglePoint
            };

            var mutation = new SwapPlayerMutation(mutationProbability, this.players);

            //create the GA itself 
            var ga = new GeneticAlgorithm(population, evaluateFitness);

            //subscribe to the GAs Generation Complete event 
            ga.OnGenerationComplete += ga_OnGenerationComplete;

            //add the operators to the ga process pipeline 
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutation);

            //run the GA 
            ga.Run(TerminateAlgorithm);

            var topSolution = this.getPlayersFromChromosome(ga.Population.GetTop(1).First());

            this.calculator.PrintFitnessDetails(topSolution);

            return topSolution.OrderBy(p => p.element_type);
        }

        private Chromosome createRandomChromosome(Random rnd)
        {
            var chromosome = new Chromosome();

            // Goalkeepers
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Goalkeeper)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Goalkeeper)));

            // Defenders
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Defender)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Defender)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Defender)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Defender)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Defender)));

            // Midfielders
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Midfielder)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Midfielder)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Midfielder)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Midfielder)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Midfielder)));            

            // Forwards
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Forward)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Forward)));
            chromosome.Genes.Add(new Gene(getRandomPlayerIndex(rnd, Role.Forward)));

            //chromosome.Genes.ShuffleFast();
            return chromosome;
        }

        private Player getRandomPlayer(Random rnd, Role role)
        {
            var rndPlayerIndexForRole = rnd.Next(0, this.groupedPlayers[role].Count);
            return this.groupedPlayers[role].ElementAt(rndPlayerIndexForRole);
        }

        private int getRandomPlayerIndex(Random rnd, Role role)
        {
            var player = this.getRandomPlayer(rnd, role);
            return Array.IndexOf(this.players, player);
        }

        private IEnumerable<Player> getPlayersFromChromosome(Chromosome chromsome)
        {
            return chromsome.Genes.Select(g => this.players[(int)g.ObjectValue]);

        }

        private void ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            var fitness = evaluateFitness(fittest);
            Console.WriteLine("Generation: {0}, Fitness: {1}", e.Generation, fitness);
        }

        private double evaluateFitness(Chromosome chromsome)
        {
            var players = this.getPlayersFromChromosome(chromsome);
            return this.calculator.CalculateFitness(players);
        }

        public bool TerminateAlgorithm(Population population,
        int currentGeneration, long currentEvaluation)
        {
            return currentGeneration > 100;
        }
    }
}
