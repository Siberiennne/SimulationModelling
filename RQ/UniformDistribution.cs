using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Моделирование равномерного распределения
    /// </summary>
    class UniformDistribution : Distribution
    {
        //параметры распределения
        double a;
        double b;

        RandomGenerator Generator = new RandomGenerator();

        public UniformDistribution()
        {
            a = 1;
            b = 0;
        }

        public UniformDistribution(double x, double y)
        {
            if ((x < 0) || (y < 0) || (y <= x))
                throw new Exception("Исходные данные некорректны");
            else
            {
                a = x;
                b = y;
            }
        }

        public double NextValue()
        {
            double u = Generator.NextValue();
            return a + (b - a) * u;
        }
    }
}
