using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Класс реализует заявку
    /// </summary>
    class Call
    {
        //момент поступления в систему
        double TimeOfArrival;

        public Call()
        {
            TimeOfArrival = 0;
        }

        public Call(double time)
        {
            TimeOfArrival = time;
        }
    }
}
