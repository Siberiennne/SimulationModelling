using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Объект реализует активный буфер
    /// пытающий самостоятельно вернуть
    /// заявки на обслуживание
    /// </summary>
    class ActiveBuffer : Buffer
    {
        List<Call> Act_Buffer = new List<Call>();

        //количество заявок в ИПВ
        int buffer_count;

        //параметр распределения
        double sigma;

        ExpDistribution exp;  

        //прибор, к которому относится данный ИПВ
        Element server;

        //номер соответствующего узла
        int buffer_node;

        double[] buf_interval = new double[2];

        public ActiveBuffer(double sigma, Element server, int node)
        {
            this.sigma = sigma;

            exp = new ExpDistribution(sigma);

            buffer_count = 0;

            this.server = server;

            buffer_node = node;
        }

        //генерация события повторного обращения за обслуживанием
        public override void GenerateEvent(Call call, double timer, Journal journal)
        {
            double time = timer + exp.NextValue();

            Event FutureEvent = new Event(call, this, time);

            journal.Add(FutureEvent);
        }

        //проверка, пуст ли буфер
        public override Boolean EmptyBuffer()
        {
            if (Act_Buffer.Count() == 0)
                return true;
            else
                return false;
        }

        public override void Accept(Call call, double timer, Journal journal, Statistic statistic, Element source)
        { 
            //конец интервала предыдущего состояния
            //становится началом текущего
            statistic.ChangeBufferInterval();
            //конец текущего интервала устанавливается по таймеру
            statistic.SetBufferInterval(timer);

            //добавляется длина данного интервала и число заявок в ИПВ
            statistic.AddBufferInterval(buffer_node, buffer_count);

            buf_interval[0] = buf_interval[1];

            buf_interval[1] = timer;

            statistic.AddBufferInterval(buffer_node, buffer_count, buf_interval[0], buf_interval[1]);
                       
            //проверка, нет ли уже этой заявки в ИПВ
            if (!this.Contain(call))
            {
                Act_Buffer.Add(call);

                buffer_count++;
            }
            //генерация события повторного обращения за обслуживанием
            this.GenerateEvent(call, timer, journal);
        }

        //событие обработки заявки
        public override void ProcessEvent(Event CurrentEvent, double timer, Journal journal, Statistic statistic, Element NextElement)
        {
            Call call = CurrentEvent.GetCall();

            //journal.Delete(CurrentEvent);

            server.Accept(call, timer, journal, statistic, this);
        }
        //удаление заявки из буфера
        public void Remove(Call call, double timer, Statistic statistic)
        {
            //конец интервала предыдущего состояния
            //становится началом текущего
            statistic.ChangeBufferInterval();
            //конец текущего интервала устанавливается по таймеру
            statistic.SetBufferInterval(timer);

            //добавляется длина данного интервала и число заявок в ИПВ
            statistic.AddBufferInterval(buffer_node, buffer_count);

            //добавляется статистика для маргинальных распределений
            buf_interval[0] = buf_interval[1];

            buf_interval[1] = timer;

            statistic.AddBufferInterval(buffer_node, buffer_count, buf_interval[0], buf_interval[1]);
          
            buffer_count--;
             
            Act_Buffer.Remove(call);

        }
        //проверка, содержится ли данная заявка в буфере
        public Boolean Contain(Call call)
        {
            return Act_Buffer.Contains(call);
        }

        //в статистку заносится длительность 
        //последнего состояния ИПВ на момент
        //окончания моделирования

        public void SetLastInterval(double timer, Statistic statistic)
        {
            statistic.ChangeBufferInterval();

            statistic.SetBufferInterval(timer);

            statistic.AddBufferInterval(buffer_node, buffer_count);
        }

    }

}
