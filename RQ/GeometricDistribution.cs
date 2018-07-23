using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Геометрическое распределение
    /// </summary>
    class GeometricDistribution
    {
        double p;

        public GeometricDistribution()
        {
            p = 0.5;
        }
        public GeometricDistribution(double p)
        {
            if ((p < 0) || (p > 1))
                throw new Exception("Исходные данные некорректны");

            else
                this.p = p;
        }
        public double Probability(int x)
        {
            double result = p;

            for (int i = 0; i < x; i++)
                result *= (1 - p);

            return result;
        }
    }
}
