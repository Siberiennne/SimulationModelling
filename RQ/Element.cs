using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Объект этого класса - 
    /// любой элемент системы,
    /// способный принимать заявки 
    /// и/или генерировать и обрабатывать 
    /// связанные с заявками события 
    /// </summary>
    abstract class Element
    {
        abstract public void Accept(Call call, double timer, Journal journal, Statistic statistic, Element source);
        abstract public void ProcessEvent(Event CurrentEvent, double timer, Journal journal, Statistic statistic, Element NextElement);
        abstract public void GenerateEvent(Call call,double timer,Journal journal);
    }
}
