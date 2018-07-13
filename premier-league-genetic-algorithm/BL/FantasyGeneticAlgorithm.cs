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

        public FantasyGeneticAlgorithm(Player[] players)
        {
            this.players = players;
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
            for (var p = 0; p < 100; p++)
            {
                var chromosome = new Chromosome();
                for (var g = 0; g < 11; g++)
                {
                    chromosome.Genes.Add(new Gene(rnd.Next(0, this.players.Count())));
                }

                chromosome.Genes.ShuffleFast();
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
            return currentGeneration > 50;
        }
    }
}
