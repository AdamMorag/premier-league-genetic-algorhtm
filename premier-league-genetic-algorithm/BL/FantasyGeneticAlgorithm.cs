using GAF;
using GAF.Extensions;
using GAF.Operators;
using premier_league_genetic_algorithm.BL.GeneticOperators;
using premier_league_genetic_algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

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

        public IEnumerable<Player> FindSolution(int populationSize, int amountOfGenerations)
        {
            const double crossoverProbability = 0.65;
            const double mutationProbability = 0.08;
            const int elitismPercentage = 5;

            //create the population
            var population = new Population();

            Random rnd = new Random();

            //create the chromosomes
            for (var p = 0; p < populationSize; p++)
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

            var mutation = new SwapPlayerMutation(mutationProbability, this.players, this.groupedPlayers);

            //create the GA itself 
            var ga = new GeneticAlgorithm(population, evaluateFitness);

            Chart chart = createNewChart();

            //subscribe to the GAs Generation Complete event 
            ga.OnGenerationComplete += this.getOnGenerationComplete(chart);

            //add the operators to the ga process pipeline 
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutation);

            //run the GA 
            ga.Run(GetTerminateFunction(amountOfGenerations));

            var topSolution = this.getPlayersFromChromosome(ga.Population.GetTop(1).First());

            var lastPoint = chart.Series["fitness"].Points.Last();
            lastPoint.Label = lastPoint.YValues[0].ToString();

            chart.SaveImage("./results/fitness.png", System.Drawing.Imaging.ImageFormat.Png);

            return topSolution.OrderBy(p => p.element_type);
        }

        private Chart createNewChart()
        {
            Chart chart = new Chart();
            chart.Size = new System.Drawing.Size(640, 320);
            chart.ChartAreas.Add("ChartArea1");
            chart.Legends.Add("legend1");
            chart.ChartAreas["ChartArea1"].AxisX.Title = "Generations";
            chart.ChartAreas["ChartArea1"].AxisY.Title = "Fitness";


            chart.Series.Add("fitness");            
            chart.Series["fitness"].LegendText = "Fitness";
            chart.Series["fitness"].ChartType = SeriesChartType.Spline;            
            return chart;
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

        private double evaluateFitness(Chromosome chromsome)
        {
            var players = this.getPlayersFromChromosome(chromsome);
            return this.calculator.CalculateFitness(players);
        }

        private TerminateFunction GetTerminateFunction(int amountOfGenerations)
        {
            return new TerminateFunction((population, currentGeneration, currentEvaluation) =>
            {
                return currentGeneration >= amountOfGenerations;
            });
        }

        private GeneticAlgorithm.GenerationCompleteHandler getOnGenerationComplete(Chart chart)
        {
            return (object sender, GaEventArgs e) => 
            {
                var fittest = e.Population.GetTop(1)[0];
                var fitness = evaluateFitness(fittest);

                chart.Series["fitness"].Points.AddXY(e.Generation, fitness);
                Console.WriteLine("Generation: {0}, Fitness: {1}, Size: {2}", e.Generation, fitness, e.Population.PopulationSize);
            };
        }
    }
}
