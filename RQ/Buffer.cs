using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{

    /// <summary>
    /// Объект реализует буфер - 
    /// встроенный в блок обслуживающих 
    /// приборов накопитель заявок, которые
    /// в настоящий момент времени не могут быть 
    /// обслужены по каким-либо причинам
    /// </summary>
    abstract class Buffer : Element
    {
        abstract public Boolean EmptyBuffer();
    }
}
