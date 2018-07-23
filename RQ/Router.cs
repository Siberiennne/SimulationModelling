using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Объект реализует маршрутизатор
    /// который управляет разделением
    /// входящего потока по узлам сети 
    /// </summary>
    class Router<T, D1, D2> : Element
        where T : Distribution
        where D1 : Distribution
        where D2 : Distribution
    {
        //вероятность потери заявки
        double prob;
        //генерируемая вероятность
        double a;

        //объект блока обслуживающих приборов на 1 фазе
        ServerBlock<D1> server1;
        //объект блока обслуживающих приборов на 2 фазе
        ServerBlock<D2> server2;

        RandomGenerator generator = new RandomGenerator();

        public Router(double prob, D1 distrib1, D2 distrib2, double sigma1, double sigma2)
        {
            this.prob = prob;
            server1 = new ServerBlock<D1>(distrib1, sigma1, 1);
            server2 = new ServerBlock<D2>(distrib2, sigma2, 2);
        }

        //генерация вероятности события перехода заявки на вторую фазу
        public override void GenerateEvent(Call call, double timer, Journal journal)
        {
            a = generator.NextValue();
        }

        public override void Accept(Call call, double timer, Journal journal, Statistic statistic, Element source)
        {
            //если заявка только поступила в систему
            //она обрабатывается прибором на 1 фазе
            if (source is Source<T>)
            {
                server1.Accept(call, timer, journal, statistic, this);
                return;
            }
            //если заявка уже прошла 1 фазу
            //она обрабатывается прибором на 2 фазе
            //или покидает систему с вероятностью prob
            if (source.GetType() == server1.GetType())
            {
                //генерация вероятности события перехода заявки на вторую фазу
               // this.GenerateEvent(call, timer, journal);
                server2.Accept(call, timer, journal, statistic, this);
              // if (a > prob)
              //  {
               //     server2.Accept(call, timer, journal, statistic, this);
              //  }
                return;
            }
        }

        //генерация события поступления заявки в систему
        public override void ProcessEvent(Event CurrentEvent, double timer, Journal journal, Statistic statistic, Element NextElement)
        {
            throw new System.InvalidOperationException("Маршрутизатор не может сам обрабатывать заявки");
        }

        public void SetLastInterval(double timer, Statistic statistic)
        {
            server1.SetLastIntervalForBuffer(timer, statistic);
            server2.SetLastIntervalForBuffer(timer, statistic);
        }
    }
}
