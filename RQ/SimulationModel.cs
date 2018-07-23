using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Объект этого класса реализует основной 
    /// алгоритм имитационного моделирования
    /// </summary>
    class SimulationModel<T, D1, D2>
        where T : Distribution
        where D1 : Distribution
        where D2 : Distribution
    {
        //таймер модельного времени
        double Timer;

        //матожидание числа заявок в ИПВ 
        double[ ] Mean;

        //ковариация числа заявок в ИПВ 
        double[,] CoVariance;

        //число заявок
        int xmax;

        //распределение входного потока
        T thread;
        //распределение закона
        //обработки заявок на 1 фазе
        D1 distrib1;

        //распределение закона
        //обработки заявок на 2 фазе
        D2 distrib2;

        //объект журнала
        Journal journal = new Journal();
        //объект для сбора статистики
        Statistic statistic;

        //объект источника заявок
        Source<T> source;
        
        //маршрутизатор
        Router<T, D1, D2> router;

        //итератор пробегает по числу заявок
        int iterator;

        public SimulationModel(int x, T thread, D1 distrib1, D2 distrib2, double sigma1, double sigma2, double prob)
        {
            this.thread = thread;
            this.distrib1 = distrib1;
            this.distrib2 = distrib2;
            source = new Source<T>(thread);      
            router = new Router<T, D1, D2> (prob, distrib1, distrib2, sigma1, sigma2);
            statistic = new Statistic();
            xmax = x;
            Timer = 0;
            iterator = 0;
            Mean=new double[2];
            CoVariance = new double[2, 2];
        }

        public double[] GetMean()
        {
            return Mean;
        }

        public double[,] GetCovariance()
        {
            return CoVariance;
        }
      
        public int BufferCount(int i)
        {
            return statistic.Count(i);
        }

        public int BufferCount()
        {
            return statistic.Count();
        }

        //распределение вероятностей пребывания системы в определенном состоянии
        public double[] SystemProbability()
        {
            double[] probability = new double[4];

            for (int i = 0; i < 4; i++)

                probability[i] = statistic.SystemProbability(i);

            return probability;
        }

        //распределение вероятностей пребывания ИПВ в определенном состоянии
        public double[] BufferProbability(int buffer)
        {
            int count;

            count = statistic.Count(buffer);

            double[] probability = new double[count];

            for (int i = 0; i < count; i++)

                probability[i] = statistic.BufferProbability(buffer, i);

            return probability;
        }

        public double[] BufferProbability()
        {
            return statistic.ReturnBufferProbability();
        }

        public List<int[]> BufferState()
        {
            return statistic.ReturnBufferState();
        }

        public double TotalTime()
        {
            return statistic.GetTotalTime();
        }

        public void OnInilization()
        {
            Call call = new Call(Timer);
            //генерация первого события
            source.GenerateEvent(call, Timer, journal);
        }

        public void DoStep()
        {
            //установка таймера
            Timer = journal.NextEvent().GetTime();

            //извлечение из журнала ближайшего события
            Event NextEvent = journal.NextEvent();

            if (NextEvent.IsArrived(source))//событие поступления заявки
            {
                //генерация будущего события поступления заявки
                if (iterator < xmax - 1)
                {
                    Call call = new Call(Timer);
                    source.GenerateEvent(call, Timer, journal);
                }

                iterator++;        
            }

                NextEvent.Process(journal, statistic, router);
        }

        public Boolean IsDone()
        {
            //когда все заявки поступят на обслуживание 
            if (iterator == xmax)
                return true;
            else
                return false;
        }

        public void Run()
        {

            this.OnInilization();

            while (!this.IsDone())
            {
                this.DoStep();
            }
            this.OnFinalization();
        }

        public void OnFinalization()
        {
            //установка общего времени моделирования
            statistic.SetTotalTime(Timer);

            //router.SetLastInterval(Timer, statistic);

            //вычисление матожидания и ковариации числа заявок в ИПВ
            Mean = statistic.GetMean();
            CoVariance = statistic.GetCovariance();
        }
    }
}
