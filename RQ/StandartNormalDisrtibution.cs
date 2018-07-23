using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Генерация нормально распределенной в (0,1) случайной величины
    /// </summary>
    class StandartNormalDisrtibution : Distribution
    {
        RandomGenerator Generator = new RandomGenerator();

        public double phi(double a)
        {
            double teta, f;

            if ((0.5 < a) && (a < 1))
                teta = Math.Sqrt(-2 * Math.Log(a));
            else
                teta = Math.Sqrt(-2 * Math.Log(1 - a));

            f = ((2.515517 + 0.802853 * teta + 0.010328 * teta * teta) / (1 + 1.432788 * teta + 0.189269 * teta * teta + 0.001308 * teta * teta * teta)) - teta;

            if (a < 0.5)
                f = -f;
            return f;
        }

        public double NextValue()
        {
            double u = Generator.NextValue();
            return phi(u);
        }
    }
}
