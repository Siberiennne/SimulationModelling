using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RQ
{
    class Matrix
    {
        int n,//число строк
            k;//число столбцов

        double[,] matr;

        public Matrix(int n, int k, double[,] matr)
        {
            this.n = n;
            this.k = k;
            this.matr = matr;
        }

        public Matrix(double[] array)
        {
            this.n = 1;
            this.k = array.Length;

            double[,] matr1 = new double[1, k + 1];

            for (int i = 0; i < k; i++)
                matr1[0, i] = array[i];

            this.matr = matr1;
        }

        public Matrix(string line)
        {
            //список значений
            List<double> value = new List<double>();

            int nn = 0;
            int kk = 0;

            string line1 = line;

            int c = 0;

            int l = line1.Length;

            //подсчет числа строк в матрице
            while (c != l)
            {
                c = line1.IndexOf("\r\n");

                nn++;
                if (c == -1)
                    break;
                line1 = line1.Remove(0, c + 2);
            }
            n = nn;

            //подсчет числа столбцов
            line1 = line;

            c = line1.IndexOf("\r\n");

            if (c == -1)
            {
                l = line1.Length;

                for (int i = 0; i < l; i++)
                {
                    if (line1[i] == ' ')

                        kk++;
                }
                k = kk + 1;
            }
            else
            {
                line1 = line1.Remove(c);

                l = line1.Length;

                for (int i = 0; i < l; i++)
                {
                    if (line1[i] == ' ')

                        kk++;
                }
                k = kk + 1;
            }

            Regex r = new Regex(@"\-?\d+(\,\d{0,})?");

            MatchCollection m = r.Matches(line);

            for (int i = 0; i < m.Count; i++)

                value.Add(double.Parse(m[i].Value));

            matr = new double[n, k];

            int v = 0;

            while (v < value.Count)
            {
                for (int i = 0; i < n; i++)

                    for (int j = 0; j < k; j++)
                    {
                        matr[i, j] = value[v];
                        v++;
                    }
            }
        }
        //перегрузка оператора [,]
        public double this[int i, int j]
        {
            get 
            {
                return matr[i, j];
            }
            set
            {
                matr[i, j] = value; 
            }
        }
        //перегрузка оператора умножения для матриц
        public static Matrix operator *(Matrix M1, Matrix M2)
        {
            if (M1.ColumnNumber() != M2.RowNumber())

                throw new Exception("Матрицы нельзя перемножить");

            else
            {
                int nn=M1.RowNumber();

                int kk=M2.ColumnNumber();

                double[,] matr = new double[nn, kk];

                for (int i = 0; i < nn; i++)
                {
                    for (int j = 0; j < kk; j++)

                        matr[i, j] = 0;
                }

                for (int i = 0; i < nn; i++)
                {
                    for (int j = 0; j < kk; j++)
                    {
                        for (int s = 0; s < M2.RowNumber(); s++)

                            matr[i, j] += M1[i, s] * M2[s, j];
                    }
                }

                Matrix M = new Matrix(nn, kk, matr);

                return M;
            }
        }

        //число столбцов
        public int ColumnNumber()
        {
            return k;
        }
        //число строк
        public int RowNumber()
        {
            return n;
        }

        //нахождение минора элемента матрицы М на s-й строке и l-м столбце
        public Matrix Minor(Matrix M, int s, int l)
        {
            if ((s >= M.RowNumber()) || l >= M.ColumnNumber())

                throw new Exception("Индекс за пределами диапазона");

            else
            {
                double[,] result = new double[M.RowNumber() - 1, M.ColumnNumber() - 1];

                for (int i = 0; i < s; i++)
                {
                    for (int j = 0; j < l; j++)

                        result[i, j] = this[i, j];

                    for (int j = l + 1; j < M.ColumnNumber(); j++)

                        result[i, j - 1] = this[i, j];
                }
                for (int i = s + 1; i < M.RowNumber(); i++)
                {
                    for (int j = 0; j < l; j++)
                    {
                        result[i - 1, j] = this[i, j];
                    }
                    for (int j = l + 1; j < M.ColumnNumber(); j++)

                        result[i - 1, j - 1] = this[i, j];
                }

                Matrix Min = new Matrix(M.RowNumber() - 1, M.ColumnNumber() - 1, result);

                return Min;
            }
        }
        //вычисление определителя
        public double Determinant()
        {
            if (n != k)
                throw new Exception("Матрица должна быть квадратной");
            else
            {
                double det = 0;

                if (n == 0)
                    return 0;

                if (n == 1)
                    return this[0, 0];

                if (n == 2)
                    return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];

                if (n > 2)
                {
                    for (int i = 0; i < n; i++)

                        det += this[i, 0] * Minor(this,i, 0).Determinant() * Math.Pow(-1, i);
                }

                return det;
            }
        }

        //транспонирование матриц
        public Matrix Transposition()
        {
            double[,] matr1 = new double[k, n];

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matr1[i, j] = this[j, i];
                }
            }
            Matrix T = new Matrix(k, n, matr1);

            return T;
        }

        //вычисление обратной матрицы
        public Matrix Inverse()
        {
            double det = this.Determinant();

            if ((det==0)||(n!=k))

                throw new Exception("Невозможно инвертировать матрицу");

            int i, j, s;

            double[,] matr1 = new double[n, n];

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    matr1[i, j] = this[i, j];
                }
            }

            double[,] matr2 = new double[n, n];

            double temp;

            for (i = 0; i < n; i++)

                for (j = 0; j < n; j++)
                {
                    matr2[i, j] = 0.0;

                    if (i == j)

                        matr2[i, j] = 1.0;
                }

            for (s = 0; s < n; s++)
            {
                temp = matr1[s, s];

                for (j = 0; j < n; j++)
                {
                    matr1[s, j] /= temp;
                    matr2[s, j] /= temp;
                }

                for (i = s + 1; i < n; i++)
                {
                    temp = matr1[i, s];

                    for (j = 0; j < n; j++)
                    {
                        matr1[i, j] -= matr1[s, j] * temp;
                        matr2[i, j] -= matr2[s, j] * temp;
                    }
                }
            }

            for (s = n - 1; s > 0; s--)
            {
                for (i = s - 1; i >= 0; i--)
                {
                    temp = matr1[i, s];

                    for (j = 0; j < n; j++)
                    {
                        matr1[i, j] -= matr1[s, j] * temp;
                        matr2[i, j] -= matr2[s, j] * temp;
                    }
                }
            }

            for (i = 0; i < n; i++)

                for (j = 0; j < n; j++)

                    matr1[i, j] = matr2[i, j];

            Matrix I = new Matrix(n, n, matr1);

            return I;
        }

       
        public Matrix ExpendedMatrix ()
        {
            int kk = this.k;

            int nn = this.n;

            double[,] matr1 = new double[nn, kk+1];

            for (int i = 0; i < nn; i++)
            {
                for (int j = 0; j < kk; j++)
                {
                    matr1[i, j] = this[i, j];
                }       
            }

            for (int i = 0; i < n; i++)
            {
                matr1[i, k] = 1;
            }    

            Matrix M = new Matrix(nn, kk+1, matr1);

            return M;
        }
    }
}
