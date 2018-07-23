using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RQ
{
    /// <summary>
    /// Объекты этого класса генерируют экспоненциально распределенные случайные величины с параметром param
    /// </summary>
    class ExpDistribution : Distribution
    {
        double param;
        RandomGenerator Generator = new RandomGenerator();

        public ExpDistribution()
        {
            param = 1;
        }

        public ExpDistribution(double y)
        {
            param = y;
        }

        public double NextValue()
        {
            double alfa = Generator.NextValue();
            return (-Math.Log(alfa) / param);
        }
    }
}
