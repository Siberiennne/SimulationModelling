using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace RQ
{
    /// <summary>
    /// Объект реализует датчик случайных чисел
    /// </summary>
    class RandomGenerator
    {
        Random rnd;

        public RandomGenerator()
        {

            Thread.Sleep(100);

            rnd = new Random();
        }

        public double NextValue()
        {
            return rnd.NextDouble();
        }
    }
}
