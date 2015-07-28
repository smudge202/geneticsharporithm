﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSharporithm
{
    public class GeneticAlgorithmBuilder<U>
    {
        public int Generations { get; private set; }
        public double MutationRate { get; private set; }

        public Population<U> Population { get; private set; }
        public IMutate<U> Mutator { get; private set; }
        public ICrossOver<U> CrossOver { get; private set; }
        public IFitnessEvaluator<U> FitnessEvaluator { get; private set; }
        public IChromosomeComparer<U> ChromosomeComparer { get; private set; }


        public GeneticAlgorithmBuilder<U> SetPopulation(Population<U> population)
        {
            Contract.Requires<ArgumentNullException>(population != null, "Argument cannot be null.");

            Population = population;

            return this;
        }

        public GeneticAlgorithmBuilder<U> SetGenerations(int generations)
        {
            Contract.Requires<ArgumentOutOfRangeException>(generations > 0, "Number of generations cannot be zero or negative.");

            Generations = generations;

            return this;
        }

        public GeneticAlgorithmBuilder<U> SetMutationRate(double mutationRate)
        {
            Contract.Requires<ArgumentOutOfRangeException>(mutationRate >= 0 && mutationRate <= 1, "Mutation rate must be in [0, 1]");

            MutationRate = mutationRate;

            return this;
        }

        public GeneticAlgorithmBuilder<U> SetMutator(IMutate<U> mutator)
        {
            Contract.Requires<ArgumentNullException>(mutator != null, "Argument cannot be null.");

            Mutator = mutator;

            return this;
        }

        public GeneticAlgorithmBuilder<U> SetCrossOver(ICrossOver<U> crossOver)
        {
            Contract.Requires<ArgumentNullException>(crossOver != null, "Argument cannot be null.");

            CrossOver = crossOver;

            return this;
        }

        public GeneticAlgorithmBuilder<U> SetFitnessEvaluator(IFitnessEvaluator<U> fitnessEvaluator)
        {
            Contract.Requires<ArgumentNullException>(fitnessEvaluator != null, "Argument cannot be null.");

            FitnessEvaluator = fitnessEvaluator;

            return this;
        }

        public GeneticAlgorithmBuilder<U> SetChromosomeComparer(IChromosomeComparer<U> chromosomeComparer)
        {
            Contract.Requires<ArgumentNullException>(chromosomeComparer != null, "Argument cannot be null.");

            ChromosomeComparer = chromosomeComparer;

            return this;
        }

        public GeneticAlgorithm<U> Build()
        {
            string message;

            if (!IsValid(out message))
            {
                throw new InvalidOperationException(message);
            }

            return new GeneticAlgorithm<U>(this);
        }

        private bool IsValid(out string message)
        {
            message = null;

            if (Generations <= 0)
            {
                message = "Generations must be positive";

                return false;
            }

            return true;
        }
    }
}
