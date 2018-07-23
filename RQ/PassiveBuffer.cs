using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Объект реализует пассивный буфер
    /// из которого блок обслуживащих приборов
    /// в нужный момент забирает заявки
    /// </summary>
    class PassiveBuffer : Buffer
    {
        Queue <Call> Pas_Buffer = new Queue<Call>();
        int limit;
        Element server;

        public PassiveBuffer(int limit,Element server)
        {
            this.limit = limit;
            this.server = server;
        }

        public override void GenerateEvent(Call call,double timer,Journal journal)
        {
            throw new System.InvalidOperationException("Время обслуживания генерирует блок обслуживаюжих приборов");
        }

        //извлечение события из буфера
        public void GetCall(double timer, Journal journal, Statistic statistic, Element source)

        {
            Call call = Pas_Buffer.Dequeue();

            server.Accept(call,timer,journal,statistic,this);
        }

        //проверка, пуст ли буфер
        public override Boolean EmptyBuffer()
        {
            if (Pas_Buffer.Count() == 0)
                return true;
            else
                return false;
        }

        public override void Accept(Call call, double timer, Journal journal, Statistic statistic, Element source)
        {
            if (limit>0)
            {

            if (Pas_Buffer.Count<limit)

                //добавление события в буфер
                Pas_Buffer.Enqueue(call);
            }
                //limit=-1 означает бесконечную очередь
            if (limit==-1)

                Pas_Buffer.Enqueue(call);
            //если limit=0 буфер не принимает заявки
        }

        //событие обработки заявки
        public override void ProcessEvent(Event CurrentEvent, double timer, Journal journal, Statistic statistic, Element NextElement)
        {
            throw new System.InvalidOperationException("Буфер не может самостоятельно перемещать заявки в системе");
        }
    }
}
