using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Нормальное распределение с указанными матожиданием и дисперсией
    /// </summary>
    class NormalDistribution
    {
        double mean;
        double sigma;

        public NormalDistribution()
        {
            mean = 0;
            sigma = 1;
        }

        public NormalDistribution(double a, double b)
        {
            mean = a;
            sigma = b;         
        }

        public double FrequencyFunction(double x)
        {
            return ((1 / (sigma * Math.Sqrt(2 * Math.PI))) * Math.Exp(-Math.Pow(((x - mean) / sigma), 2)/2));
        }
    }
}
