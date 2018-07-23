using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Детерминированное распределение
    /// </summary>
    class DetermineDistribution : Distribution
    {
        double param;

        public DetermineDistribution()
        {
            param = 1;
        }

        public DetermineDistribution(double y)
        {
            if (y < 0)
                throw new Exception("Исходные данные некорректны");
            else
                param = y;
        }

        public double NextValue()
        {
            return param;
        }
    }
}
