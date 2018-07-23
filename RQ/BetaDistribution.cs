using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Моделирование двухпараметрического бета-распределения
    /// </summary>
    class BetaDistribution : Distribution
    {
        //параметры распределения
        double alpha;
        double beta;

        GammaDistribution gamma1;
        GammaDistribution gamma2;

        public BetaDistribution()
        {
            alpha = 1;
            beta = 1;
            gamma1 = new GammaDistribution(alpha, 1);
            gamma2 = new GammaDistribution(beta, 1);
        }

        public BetaDistribution(double a, double b)
        {
            alpha = a;
            beta = b;
            gamma1 = new GammaDistribution(alpha, 1);
            gamma2 = new GammaDistribution(beta, 1);
        }

        public double NextValue()
        {
            double y1 = gamma1.NextValue();
            double y2 = gamma2.NextValue();

            return y1 / (y1 + y2);
        }
    }
}
