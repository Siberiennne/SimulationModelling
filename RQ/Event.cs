using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{

    /// <summary>
    /// Реализует специальный объект, 
    /// инкапсулирующий всю информацию, 
    /// необходимую для корректной регистрации 
    /// и обработки потока событий внутри модели
    /// </summary>
    class Event
    {
        double time;

        Element involker;

        Call call;

        public Event()
        {
            Call call = new Call();

            time = 0;

            DetermineDistribution distrib = new DetermineDistribution();

            involker = new Source<DetermineDistribution>(distrib);
        }

        public Event(Call call1,Element element, double timer)
        {
            call = call1;

            involker = element;

            time = timer;
        }

        //обработка события
        public void Process(Journal journal, Statistic statistic, Element router)
        {
            involker.ProcessEvent(this, time, journal, statistic, router);

            journal.Delete(this);
        }

        public void SetTime(double t)
        {
            time = t;
        }

        public double GetTime()
        {
            return this.time;
        }

        public Call GetCall()
        {
            return call;
        }

        //проверка на поступление заявки
        public Boolean IsArrived<T>(Source<T> source)
            where T : Distribution
        {
            if (involker.GetType() == source.GetType())
                return true;
            else
                return false;
        }
    }
}
