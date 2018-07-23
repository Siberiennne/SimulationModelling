using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{   
    /// <summary>
    /// Объект реализует блок обслуживающих приборов (узел) – 
    /// некоторое количество собранных вместе устройств, 
    /// занимающихся обработкой (обслуживанием) заявок
    /// </summary>
    class ServerBlock<D> : Element where D : Distribution
    {
        ActiveBuffer buffer;

        //распределение обработки заявок
        D distrib;

        //счётчик числа занятых приборов
        int count;

        //номер соответствующего узла
        int node;


        public ServerBlock(D distrib, double sigma, int node)
        {
            this.distrib = distrib;
            buffer = new ActiveBuffer(sigma, this, node);
            count = 0;
            this.node = node;
        }

        //генерация события окончания обслуживания
        public override void GenerateEvent(Call call, double timer,Journal journal)
        {
            double time = timer + distrib.NextValue();

            Event FutureEvent = new Event(call, this, time);

            journal.Add(FutureEvent);
        }

        //проверка занятости прибора
        public Boolean IsFree()
        {
            if (count == 1)
                return false;
            else
                return true;
        }

        public override void Accept(Call call, double timer, Journal journal, Statistic statistic, Element source)
        {
            //если все приборы заняты, заявка помещается в буфер
            if (!this.IsFree())
            {
                buffer.Accept(call, timer, journal, statistic, this);
            }
            else
            {
                //если данная заявка пришла из ИПВ
                //она удаляется из него
                if (buffer.Contain(call))
                {
                    buffer.Remove(call, timer, statistic);
                }

                //конец интервала предыдущего состояния
                //становится началом текущего
                statistic.ChangeInterval();
                //конец текущего интервала устанавливается по таймеру
                statistic.SetInterval(timer);
                //добавляется длина данного интервала и число обрабатываемых заявок
                statistic.AddInterval(node, count);

                count++;

                //заявка поступает на обслуживание  
                this.GenerateEvent(call, timer, journal);
            }
        }

        //функция реализует обработку события окончания обслуживания
        public override void ProcessEvent(Event CurrentEvent, double timer, Journal journal, Statistic statistic, Element NextElement)
        {
            //конец интервала предыдущего состояния
            //становится началом текущего
            statistic.ChangeInterval();
            //конец текущего интервала устанавливается по таймеру
            statistic.SetInterval(timer);
            //добавляется длина данного интервала и число обрабатываемых заявок
            statistic.AddInterval(node,count);

            count--;

            Call call = CurrentEvent.GetCall();

            //заявка передается на другой узел
            if (this.node == 1)

                NextElement.Accept(call, timer, journal, statistic, this);
            // journal.Delete(CurrentEvent);
        }

        public void SetLastIntervalForBuffer(double timer, Statistic statistic)
        {
            buffer.SetLastInterval(timer, statistic);
        }
        public int ReturnNode()
        {
            return this.node;
        }


    }

}
