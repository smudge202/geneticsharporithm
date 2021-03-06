﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSharporithm
{
    public interface IFitnessEvaluator<T>
    {
        double ComputeFitness(T genes);
    }
}
