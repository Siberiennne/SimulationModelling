using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace RQ
{
    /// <summary>
    /// Биномиальное распределение
    /// </summary>
    class BinomialDistribution
    {
        //вероятность успеха
        double p;
        //число экспериментов
        int n;

        public BinomialDistribution()
        {
            p = 0.5;
            n = 1;
        }
        public BinomialDistribution(double p, int n)
        {
            if ((p < 0) || (p > 1) || (n < 0))
                throw new Exception("Исходные данные некорректны");
            else
            {
                this.p = p;
                this.n = n;
            }
        }
        public BigInteger Factorial(int x)
        {
            BigInteger result = 1;

            for (int i = 0; i < x; i++)

                result *= (x - i);

            return result;
        }
        public double Probability(int k)
        {
            double fact = (double)(Factorial(n) / (Factorial(n - k) * Factorial(k)));

            double pp = 1, q = Math.Pow((1 - p), n);

            for (int i = 0; i < k; i++)
                pp *= p;

            for (int i = 0; i < k; i++)
                q /= (1 - p);

            return fact * pp * q;
        }
    }
}
