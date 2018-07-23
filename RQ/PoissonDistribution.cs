using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Распределение Пуассона
    /// </summary>
    class PoissonDistribution
    {
        //параметр распределения
        double lyambda;

        public PoissonDistribution()
        {
            lyambda = 1;
        }

        public PoissonDistribution(double l)
        {
            lyambda = l;
        }

        public long Factorial(int x)
        {
            int result = 1;
         
                for (int i = 0; i < x; i++)

                    result *=(x-i);

            return result;
        }

        public double Probability(int i)
        {
            return Math.Pow(lyambda, i) * Math.Exp(-lyambda) / Factorial(i);
        }
    }
}
