using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// Объект этого класса служит для сбора информации об 
    /// общей длительности работы приборов
    /// </summary>
    class Statistic
    {
        //общее время моделирования
        double Totaltime;

        //интервалы времени состояний занятости системы за весь период
        double[] SystemTimeInterval = new double[4] { 0, 0 ,0,0};
        List<int[]> SystemState = new List<int[]>();
        List<double> System = new List<double>();

        //временные интервалы состояний занятости системы
        double[] interval = new double[2];

        //временные интервалы состояний загруженности ИПВ за весь период
        double[] buf_interval = new double[2];

        //список состояний ИПВ
        List<int[ ]> BufferState = new List<int[ ]>();
        //интервалы времени состояний загруженности ИПВ
        List<double> BufferTimeInterval = new List<double>();

        List<double> BufferTimeInterval1 = new List<double>();
        List<double> BufferTimeInterval2 = new List<double>();

        int buffer1, buffer2 = 0;
        int server1, server2 = 0;
        double duration = 0;

        public Statistic()
        {
            interval[0] = 0;
            interval[1] = 0;

            buf_interval[0] = 0;
            buf_interval[1] = 0;
        }


        //state - сколько приборов занято
        //прибавляется время, в течение которого
        //сохранялось данное состояние
        public void AddInterval(int server, int state)
        {
            if (server == 1)
                SystemTimeInterval[state] += (interval[1] - interval[0]);
            if (server == 2)
                SystemTimeInterval[state+2] += (interval[1] - interval[0]);
            /*
            int k = server1 + 2*server2;

            int[] state1 = new int[2];

            state1[0] = server1;
            state1[1] = server2;

            SystemTimeInterval[k] += (interval[1] - interval[0]);
            SystemState.Add(state1);
            System.Add(interval[1] - interval[0]);
             */
        }

        public void AddBufferInterval(int buffer_node, int state, double time1, double time2)
        {
            if (buffer_node == 1)
            {
                //записывается состояние, которого раньше не было
                if (BufferTimeInterval1.Count < (state + 1))

                    BufferTimeInterval1.Add(time2 - time1);

                else

                    BufferTimeInterval1[state] += (time2 - time1);
            }
            else
            {
                //записывается состояние, которого раньше не было
                if (BufferTimeInterval2.Count < (state + 1))

                    BufferTimeInterval2.Add(time2 - time1);

                else

                    BufferTimeInterval2[state] += (time2 - time1);
            }
        }

        //buffer - сколько заявок в ИПВ
        //прибавляется время, в течение которого
        //сохранялось данное состояние
        public void AddBufferInterval(int buffer_node, int buffer)
        {
            duration = buf_interval[1] - buf_interval[0];

            //изменение состояния на первой фазе
            if (buffer_node == 1)
            {
                buffer1 = buffer;
            }
            //изменение состояния на второй фазе
            else
            {
                buffer2 = buffer;
            }

            //state[0]=сколько заявок в ИПВ1
            //state[1]=сколько заявок в ИПВ2
            int[] state = new int[2];

            state[0] = buffer1;
            state[1] = buffer2;

            int count = BufferTimeInterval.Count;
            //пробегаем по списку двумерного распределения
            int j = 0;

            while (j < count)
            {
                //если такое состояние уже было
                if ((state[0] == BufferState[j][0]) && (state[1] == BufferState[j][1]))
                {
                    //складываем длительности
                    BufferTimeInterval[j] += duration;
                    break;
                }
                else
                    j++;
            }
            //если такого состояния не было
            if (j == count)
            {
                //добавляем в общую статистику
                BufferState.Add(state);
                BufferTimeInterval.Add(duration);
            }
        }

        public void SetInterval(double time)
        {
            interval[1] = time;
        }
        public void ChangeInterval()
        {
            interval[0] = interval[1];
        }

        public void SetBufferInterval(double time)
        {
            buf_interval[1] = time;
        }
        public void ChangeBufferInterval()
        {
            buf_interval[0] = buf_interval[1];
        }

        public void SetTotalTime(double t)
        {
            Totaltime = t;
        }

        public double GetTotalTime()
        {
            return Totaltime;
        }

        public double SystemProbability(int i)
        {
            return SystemTimeInterval[i] / Totaltime;
        }

        public double BufferProbability(int buffer, int i)
        {
            if (buffer == 1)
                return BufferTimeInterval1[i] / Totaltime;
            else
                return BufferTimeInterval2[i] / Totaltime;
        }

        public double BufferProbability(int i)
        {
            return BufferTimeInterval[i] / Totaltime;
        }

        public double[] ReturnSystemProbability()
        {
            double[] prob = new double[4];

            for (int i = 0; i < 4; i++)

                prob[i] = this.SystemProbability(i);

            return prob;
        }

        public void SetBufferProbability1()
        {
            int count = BufferTimeInterval.Count;

            int state;

            //пробегаем по списку двумерного распределения
            for (int i = 0; i < count; i++)
            {
                //запоминаем число заявок в ИПВ1
                state = BufferState[i][0];

                int j = 0;

                while (j < BufferTimeInterval1.Count)
                {
                    //если такое состояние уже было
                    if (state == j)
                    {
                        //складываем длительности
                        BufferTimeInterval1[j] += BufferTimeInterval[i];
                        break;
                    }
                    else
                        j++;
                }
                //если такого состояния не было
                if (j == BufferTimeInterval1.Count)
                {
                    BufferTimeInterval1.Add(BufferTimeInterval[i]);
                }
            }
        }

        public void SetBufferProbability2()
        {
            int count = BufferTimeInterval.Count;

            int state;

            //пробегаем по списку двумерного распределения
            for (int i = 0; i < count; i++)
            {
                //запоминаем число заявок в ИПВ2
                state = BufferState[i][1];

                int j = 0;

                while (j < BufferTimeInterval2.Count)
                {
                    //если такое состояние уже было
                    if (state == j)
                    {
                        //складываем длительности
                        BufferTimeInterval2[j] += BufferTimeInterval[i];
                        break;
                    }
                    else
                        j++;
                }
                //если такого состояния не было
                if (j == BufferTimeInterval2.Count)
                {
                    BufferTimeInterval2.Add(BufferTimeInterval[i]);
                }
            }           
        }

        public double[] ReturnBufferProbability(int buffer)
        {
            int count;

            double[] prob;

            if (buffer == 1)
            {
                count = BufferTimeInterval1.Count;

                prob = new double[count];

                for (int i = 0; i < count; i++)

                    prob[i] = this.BufferProbability(1, i);

                return prob;
            }
            else
            {
                count = BufferTimeInterval2.Count;

                prob = new double[count];

                for (int i = 0; i < count; i++)

                    prob[i] = this.BufferProbability(2,i);

                return prob;
            }
        }

        //вектор матожиданий числа заявок в ИПВ 
        public double[] GetMean()
        {
            double[] Mean = new double[2] { 0, 0 };

            for (int i = 0; i < BufferTimeInterval1.Count; i++)

                Mean[0] += i * BufferProbability(1, i);

            for (int i = 0; i < BufferTimeInterval2.Count; i++)

                Mean[1] += i * BufferProbability(2, i);

            Mean[0] = Math.Round(Mean[0], 4);
            Mean[1] = Math.Round(Mean[1], 4);

            return Mean;
        }

        //матрица ковариации числа заявок в ИПВ 
        public double[,] GetCovariance()
        {
            double[,] covariance = new double[2, 2] { { 0, 0 }, { 0, 0 } };

            double cov = 0;

            int x, y;

            double mean1=0, mean2 = 0;

            for (int i = 0; i < BufferTimeInterval1.Count; i++)

                mean1 += i * BufferProbability(1, i);

            for (int i = 0; i < BufferTimeInterval2.Count; i++)

                mean2 += i * BufferProbability(2, i);

            for (int i = 0; i < BufferTimeInterval1.Count; i++)

                covariance[0, 0] += (i - mean1) * (i - mean1) * BufferProbability(1, i);

            for (int i = 0; i < BufferTimeInterval.Count; i++)
            {
                x = BufferState[i][0];
                y = BufferState[i][1];
                double BufferProb = BufferProbability(i);
                cov += x * y * BufferProb;
            }

            covariance[0, 1] = covariance[1, 0] = cov - mean1 * mean2;

            for (int i = 0; i < BufferTimeInterval2.Count; i++)

                covariance[1, 1] += (i - mean2) * (i - mean2) * BufferProbability(2, i);

            covariance[0, 0] = Math.Round(covariance[0, 0], 4);
            covariance[0, 1] = Math.Round(covariance[0, 1], 4);
            covariance[1, 0] = Math.Round(covariance[1, 0], 4);
            covariance[1, 1] = Math.Round(covariance[1, 1], 4);

            return covariance;
        }

        public List<int[]> ReturnBufferState()
        {
            return BufferState;
        }


        public double[] ReturnBufferProbability()
        {
            int count = BufferTimeInterval.Count;

            double[] BufferProbability = new double[count];

            //пробегаем по списку двумерного распределения
            for (int i = 0; i < count; i++)

                BufferProbability[i] = this.BufferProbability(i);

            return BufferProbability;
        }

        //число заявок в ИПВ на фазе № buffer
        public int Count(int buffer)
        {
            if (buffer==1)
            return BufferTimeInterval1.Count;
            else
                return BufferTimeInterval2.Count;
        }

        public int Count()
        {
            return BufferTimeInterval.Count;
        }

    }
}
