﻿using GeneticSharporithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSharporithmProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // trying to make the word "abracadabra"

            var evaluator = new FitnessEvaluator("abracadabra");
            var random = new Random();
            var generator = new Generator(random);

            const int Total = 100;

            var population = new Population<string>(Total, generator, evaluator);

            var fitnessDict = new Dictionary<Chromosome<string>, double>();

            var mutator = new Mutator();

            var crossOver = new SinglePointCrossOver(random, evaluator);

            var selector = new Selector(evaluator, 10, Total);

            var killer = new ChromosomeKiller(5);

            var solution = new Solution();

            var geneticAlgorithm = new GeneticAlgorithmBuilder<string>()
                .SetPopulation(population)
                .SetFitnessEvaluator(evaluator)
                .SetGenerations(150000)
                .SetMutationRate(0.05)
                .SetMutator(mutator)
                .SetCrossOver(crossOver)
                .SetSelection(selector)
                .SetKiller(killer)
                .SetSolution(solution)
                .Build();

            geneticAlgorithm.BeforeRun += GeneticAlgorithm_BeforeRun;

            geneticAlgorithm.AfterRun += GeneticAlgorithm_AfterRun;

            geneticAlgorithm.OnSolution += GeneticAlgorithm_OnSolution;

            geneticAlgorithm.Run();

        }

        private static void GeneticAlgorithm_OnSolution(GeneticAlgorithm<string> sender, EventArgs e)
        {

        }

        private static void GeneticAlgorithm_BeforeRun<V>(GeneticAlgorithm<V> geneticAlgorithm, GeneticEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void GeneticAlgorithm_AfterRun<V>(GeneticAlgorithm<V> geneticAlgorithm, GeneticEventArgs e)
        {
            var chromosomes = geneticAlgorithm.Population.Chromosomes;

            var average = 0d;

            foreach (var chromosome in chromosomes)
            {
                average += chromosome.Fitness;
            }

            average /= chromosomes.Count;

            var output = $"Generation:({e.Generation}), Best: ({chromosomes[0].Genes}, {chromosomes[0].Fitness}), Average fitness: {average}";

            Console.WriteLine(output);
        }
    }
}
