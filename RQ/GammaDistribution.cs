using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Моделирование двухпараметрического гамма-распределения
    /// </summary>
    class GammaDistribution : Distribution
    {
        //параметры распределения
        double alpha;
        double beta;

        RandomGenerator Generator = new RandomGenerator();

        //распределение gamma(1,1) является экспоненциальным со средним 1
        ExpDistribution exp = new ExpDistribution(1);

        //стандартная нормальная величина
        StandartNormalDisrtibution norm = new StandartNormalDisrtibution();

        public GammaDistribution()
        {
            alpha = 1;
            beta = 1;
        }

        public GammaDistribution(double a, double b)
        {
            alpha = a;
            beta = b;
        }

        public double NextValue()
        {
            double result = 0, x, p, y, d, c, z, u, v;

            if ((alpha > 0) && (beta > 0))
            {
                if ((alpha > 0) && (alpha < 1))
                {
                    x = (Math.E + alpha) / Math.E;

                    do
                    {
                        p = x * Generator.NextValue();
                        if (p > 1)
                            y = -Math.Log((x - p) / alpha);
                        else
                            y = Math.Pow(p, alpha - 1);
                    }

                    while (((y < 1) && (Math.Pow(Math.E, -y) < Generator.NextValue())) || ((y >= 1) && (Math.Pow(y, alpha - 1) < Generator.NextValue())));

                    result = y;
                }

                if (alpha > 1)
                {
                    d = alpha - 1 / 3;
                    c = 1 / (Math.Sqrt(9 * d));
                    do
                    {
                        z = norm.NextValue();
                        u = Generator.NextValue();
                        v = Math.Pow(1 + c * z, 3);
                    }
                    while ((z <= (-1 / c)) && (Math.Log(u) >= (0.5 * z * z + d - d * v + d * Math.Log(v))));
                    result = d * v;

                }

                if ((alpha == 1) && (beta == 1))
                    result = exp.NextValue();
            }

            return result / beta;

        }
    }
}
