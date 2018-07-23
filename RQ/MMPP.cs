using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RQ
{
    /// <summary>
    /// MMPP-поток
    /// </summary>
    class MMPP : Distribution
    {
        Matrix Q,lyambda;

        RandomGenerator generator = new RandomGenerator();

        //таймер принимает значение момента сгенерированного события потока
        double timer;
        //время до следующего перехода цепи в другое состояние
        double t;
        //массив финальных вероятностей
        double[] pi;
        //массив переходных вероятностей
        double[] p;
        //текущее состояние цепи
        int k;

        public MMPP()
        {
            double[,] matr = new double[3, 3];

            double [] vec = new double[3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == j)
                        matr[i, j] = -0.5;
                    else
                        matr[i, j] = 0.25;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                vec[i] = 1;
            }
            Q = new Matrix(3, 3, matr);

            lyambda = new Matrix(vec);

            timer = t = 0;

            pi = this.FinalProbability();

            p = pi;

            //вычисление начального состояния цепи Маркова
            k = this.Condition(pi);

            double a = generator.NextValue();

            //вычисление интервала времени, в течение которого
            //цепь не изменит состояния
            double tau = Math.Log(a) / Q[k, k];

            t += tau;
        }

        public MMPP(Matrix Q, Matrix lyambda)
        {
            this.Q = Q;

            this.lyambda = lyambda;

            if (!this.IsRight(Q, lyambda))

                throw new Exception("Исходные данные некорректны");

            timer = t = 0;

            pi = this.FinalProbability();

            p = pi;

            //вычисление начального состояния цепи Маркова
            k = this.Condition(pi);

            double a = generator.NextValue();

            //вычисление интервала времени, в течение которого
            //цепь не изменит состояния
            double tau = Math.Log(a) / Q[k, k];

            t += tau;
        }

        //определение состояния цепи по массиву вероятностей
        public int Condition(double[] p)
        {
            double a = generator.NextValue();

            int i = 0;

            a = a - p[i];

            while (a >= 0)
            {
                i++;
                a = a - p[i]; 
            }
            return i;
        }
        //расчет финальных вероятностей
        public double[] FinalProbability()
        {
            Matrix Q1 = Q;

            int i;

            int n = Q.RowNumber();

            double[] p = new double[n+1];

            for (i = 0; i < n; i++)

                p[i] = 0;

            p[n] = 1;

            Matrix v = new Matrix(p);

            Matrix M = Q.ExpendedMatrix();

            Matrix M1 = M*M.Transposition();

            Matrix M2 = M1.Inverse();

            Matrix M3 =  M.Transposition() * M2;

            Matrix pi1 = v * M3;

            double[] pi = new double[n];

            for (i = 0; i < n; i++)

                pi[i] = pi1[0, i];

            return pi;
        }

        public double NextValue()
        {
            int n = Q.RowNumber();

            double a, result = 0, tau;

            //если истекло время пребывания цепи в k-ом состоянии,
            //считаются переходные вероятности, и по ним
            //определяется новое состояние
            if (timer > t)
            {
                for (int i = 0; i < n; i++)
                {
                    p[i] = -(Q[k, i]) / (Q[k, k]);

                    if (i == k)

                        p[i] = 0;
                }
                k = this.Condition(p);

                a = generator.NextValue();

                tau = Math.Log(a) / Q[k, k];

                t += tau;
            }
            a = generator.NextValue();

            result = -Math.Log(a) / lyambda[0, k];

            timer += result;

            return result;
        }
        //проверка правильности введенных матрицы инфинитизимальных характеристик и вектора интенсивностей
        public Boolean IsRight(Matrix Q, Matrix lyambda)
        {
            //матрица должна быть квадратной
            //а число строк вектора совпадать
            //с размерностью матрицы
            if ((Q.RowNumber() != Q.ColumnNumber()) || (lyambda.ColumnNumber() != Q.ColumnNumber()))

                return false;
            //сумма элементов по строке матрицы
            //должна равняться 0
            else
            {
                int i, j;
                double sum, s;

                for (i = 0; i < Q.RowNumber(); i++)
                {
                    s = sum = 0;

                    for (j = 0; j < Q.RowNumber(); j++)
                    {
                        if (i == j)
                            s = Q[i, j];
                        else
                            sum += Q[i, j];
                    }

                    if (s != -sum)
                        break;
                    //элементы вектора должны быть положительны
                    if (lyambda[0, i] < 0)
                        break;
                }
                if (i != Q.RowNumber())
                    return false;

                else
                    return true;
            }
        }
    }
}
