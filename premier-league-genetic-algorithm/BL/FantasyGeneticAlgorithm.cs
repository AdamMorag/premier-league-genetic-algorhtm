﻿using GAF;
using GAF.Extensions;
using GAF.Operators;
using premier_league_genetic_algorithm.BL.GeneticOperators;
using premier_league_genetic_algorithm.BL.Performance;
using premier_league_genetic_algorithm.BL.Performance.ChartJSPerformanceMonitor;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace premier_league_genetic_algorithm.BL
{
    public class FantasyGeneticAlgorithm
    {
        #region Data Members

        private Player[] players;
        private FitnessCaclulator calculator;
        private Dictionary<Role, List<Player>> groupedPlayers;
        private ChromosomeUtils chromosomeUtils;
        private PerformanceMonitor performanceMonitor;
        
        #endregion

        #region Ctor

        public FantasyGeneticAlgorithm(Player[] players)
        {
            this.players = players;
            this.groupedPlayers = players.GroupBy(p => p.element_type).ToDictionary(g => g.Key, x => x.ToList());
            this.calculator = new FitnessCaclulator(players);
            this.chromosomeUtils = new ChromosomeUtils(players, groupedPlayers);
            Console.WriteLine(DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss"));
            this.performanceMonitor = new ChartJsPerformanceMonitor(DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss"));
        } 
        
        #endregion

        #region Public Methods

        public IEnumerable<Player> FindSolution(int populationSize, int amountOfGenerations)
        {
            //create the population
            var population = new Population();

            //create the chromosomes
            for (var p = 0; p < populationSize; p++)
            {
                Chromosome chromosome = this.chromosomeUtils.GenerateRandomSolution();
                population.Solutions.Add(chromosome);
            }

            var newPopulation = this.runAlgorithem(population, amountOfGenerations);

            var topSolution = this.chromosomeUtils.ConvertChromosome(newPopulation.GetTop(1).First());

            return topSolution.OrderBy(p => p.element_type);
        }        

        #endregion

        #region Private Methods

        private Population runAlgorithem(Population population, int amountOfGenerations)
        {
            const double crossoverProbability = 0.65;
            const double mutationProbability = 0.08;
            const int elitismPercentage = 5;

            //create the genetic operators 
            var elite = new Elite(elitismPercentage);

            var crossover = new Crossover(crossoverProbability, true)
            {
                CrossoverType = CrossoverType.SinglePoint
            };

            var mutation = new SwapPlayerMutation(mutationProbability, this.players, this.groupedPlayers);

            //create the GA itself 
            var ga = new GeneticAlgorithm(population, evaluateFitness);            

            //subscribe to the GAs Generation Complete event 
            ga.OnGenerationComplete += this.getOnGenerationComplete();

            //add the operators to the ga process pipeline 
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutation);

            //run the GA 
            ga.Run(GetTerminateFunction(amountOfGenerations));

            this.performanceMonitor.SavePerformanceLog("./results");

            return ga.Population;
        }

        private double evaluateFitness(Chromosome chromsome)
        {
            var players = this.chromosomeUtils.ConvertChromosome(chromsome);
            return this.calculator.CalculateFitness(players);
        }

        private TerminateFunction GetTerminateFunction(int amountOfGenerations)
        {
            return new TerminateFunction((population, currentGeneration, currentEvaluation) =>
            {
                return currentGeneration >= amountOfGenerations;
            });
        }

        private GeneticAlgorithm.GenerationCompleteHandler getOnGenerationComplete()
        {
            return (object sender, GaEventArgs e) =>
            {
                var fitness = e.Population.MaximumFitness;

                this.performanceMonitor.LogPerformance(e);

                Console.WriteLine("Generation: {0}, Fitness: {1}, Size: {2}", e.Generation, fitness, e.Population.PopulationSize);
            };
        } 
        
        #endregion
    }
}
