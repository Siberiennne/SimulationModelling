using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Объект данного класса реализует источник заявок, 
    /// порождающий входящий поток заявок
    /// </summary>
    class Source<T> : Element where T : Distribution
    {
        //распределение входного потока
        T thread;

        public Source(T thread)
        {
            this.thread = thread;
        }

        public override void Accept(Call call, double timer, Journal journal, Statistic statistic, Element source)
        {
            throw new System.InvalidOperationException("Источник заявок не может принимать заявки");
        }

        //передача заявки маршрутизатору
        public override void ProcessEvent(Event CurrentEvent, double timer, Journal journal, Statistic statistic, Element NextElement)
        {
            Call call = CurrentEvent.GetCall();

           // journal.Delete(CurrentEvent);

            NextElement.Accept(call, timer, journal, statistic, this);
        }

        //генерация события поступления заявки в систему
        public override void GenerateEvent(Call call, double timer, Journal journal)
        {
            double time = timer + thread.NextValue();     

            Event FutureEvent = new Event(call,this, time);

            journal.Add(FutureEvent);
        }
    }
}
