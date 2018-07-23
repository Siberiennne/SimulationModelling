using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Threading;
using System.IO;

namespace RQ
{
    public partial class Form1 : Form
    {

        static int BufferCount1;
        static int BufferCount2;
        static int BufferCount;

        //распределение вероятностей состояний системы
      static double[] SystemProbability = new double[2] { 0, 1 };

      //маргинальные распределения вероятностей числа заявок в ИПВ
       static double[] BufferProbability1;
       static double[] BufferProbability2;

       static double[] BufferProbability;
       static List<int[]> BufferState;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;
            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            label31.Visible = false;
            label32.Visible = false;
            label33.Visible = false;
            label34.Visible = false;
            label35.Visible = false;
            //label36.Visible = false;
            //label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            label41.Visible = false;
            label42.Visible = false;
            label43.Visible = false;
            label44.Visible = false;
            label51.Visible = false;
            label52.Visible = false;
            label53.Visible = false;
            label54.Visible = false;
            label55.Visible = false;
            label56.Visible = false;
            label57.Visible = false;

            textBox7.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            textBox14.Visible = false;
            textBox15.Visible = false;
            textBox16.Visible = false;
            textBox17.Visible = false;
            textBox18.Visible = false;
            textBox19.Visible = false;
            textBox20.Visible = false;
            textBox21.Visible = false;
            textBox22.Visible = false;
            textBox23.Visible = false;
            textBox24.Visible = false;
            textBox25.Visible = false;
            textBox26.Visible = false;
            textBox27.Visible = false;
            //textBox28.Visible = false;
            //textBox29.Visible = false;
            textBox30.Visible = false;
            textBox31.Visible = false;
            textBox32.Visible = false;
            textBox33.Visible = false;
            textBox34.Visible = false;
            textBox35.Visible = false;
            textBox36.Visible = false;
            textBox40.Visible = false;
            textBox41.Visible = false;
            textBox42.Visible = false;
            textBox43.Visible = false;
            textBox44.Visible = false;

            label7.Text = "ИПВ1";
            label8.Text = "ИПВ2";
            label11.Text = "ИПВ1";
            label12.Text = "ИПВ2";
            label49.Text = "ИПВ1";
            label50.Text = "ИПВ2";

           // groupBox8.Enabled = false;
            //groupBox9.Enabled = false;

           // numericUpDown1.Maximum = 1;
            //numericUpDown2.Maximum = 1;
            numericUpDown3.Maximum = 1;

            //numericUpDown1.Minimum = 0;
           // numericUpDown2.Minimum = 0;
            numericUpDown3.Minimum = 0;

           // numericUpDown1.Increment = 0.001M;
           // numericUpDown2.Increment = 0.001M;
            numericUpDown3.Increment = 0.001M;

         //   numericUpDown1.DecimalPlaces = 3;
          //  numericUpDown2.DecimalPlaces = 3;
            numericUpDown3.DecimalPlaces = 3;

            Graph();
        }

        private void Graph()
        {
            // Получим панель для рисования
           // GraphPane pane1 = zedGraphControl2.GraphPane;

            GraphPane pane1 = zedGraphControl3.GraphPane;

            GraphPane pane2 = zedGraphControl4.GraphPane;

            // Количество состояний будет отображаться с периодом 1
            pane1.XAxis.Scale.MajorStep = 1.0;

            pane2.XAxis.Scale.MajorStep = 1.0;

            // Устанавливаем интересующий нас интервал по осям
            pane1.XAxis.Scale.Min = 0;
            pane1.YAxis.Scale.Max = 1;
            pane1.YAxis.Scale.Min = 0;

            pane2.XAxis.Scale.Min = 0;
            pane2.YAxis.Scale.Max = 1;
            pane2.YAxis.Scale.Min = 0;

            // По оси Y установим автоматический подбор масштаба
            pane1.YAxis.Scale.MinAuto = true;
            pane1.YAxis.Scale.MaxAuto = true;
            pane2.YAxis.Scale.MinAuto = true;
            pane2.YAxis.Scale.MaxAuto = true;

            // Установим значение параметра IsBoundedRanges как true.
            // Это означает, что при автоматическом подборе масштаба
            // нужно учитывать только видимый интервал графика
            pane1.IsBoundedRanges = true;
            pane2.IsBoundedRanges = true;

            pane1.Title.Text = "Распределение числа заявок в ИПВ 1";
            pane1.XAxis.Title.Text = "Число заявок";
            pane1.YAxis.Title.Text = "Вероятность";
            pane2.Title.Text = "Распределение числа заявок в ИПВ 2";
            pane2.XAxis.Title.Text = "Число заявок";
            pane2.YAxis.Title.Text = "Вероятность";

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane1.CurveList.Clear();

            pane2.CurveList.Clear();

            // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
            zedGraphControl3.AxisChange();

            zedGraphControl4.AxisChange();

            // Обновляем график
            zedGraphControl3.Invalidate();

            zedGraphControl4.Invalidate();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.TextLength == 0) || (textBox2.TextLength == 0) || (textBox3.TextLength == 0) || (textBox4.TextLength == 0))
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.TextLength == 0) || (textBox2.TextLength == 0) || (textBox3.TextLength == 0) || (textBox4.TextLength == 0))
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))//число
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                //можно ввести не больше одной запятой
                //запятая не может быть первым символом
                if ((textBox1.Text.IndexOf(",") != -1 || textBox1.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox2.Text.IndexOf(",") != -1 || textBox2.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.TextLength == 0) || (textBox2.TextLength == 0) || (textBox3.TextLength == 0) || (textBox4.TextLength == 0))
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //label6.Visible = true;
            //label7.Visible = true;
           // label8.Visible = true;
            //label13.Visible = true;
           // label14.Visible = true;

           // textBox5.Clear();
            //textBox6.Clear();
            //textBox39.Clear();
            //textBox45.Clear();
           // textBox46.Clear();

            ExpDistribution exp_thread = new ExpDistribution();
            ExpDistribution exp_distrib1 = new ExpDistribution();
            ExpDistribution exp_distrib2 = new ExpDistribution();

            GammaDistribution gamma_thread = new GammaDistribution();
            GammaDistribution gamma_distrib1 = new GammaDistribution();
            GammaDistribution gamma_distrib2 = new GammaDistribution();

            BetaDistribution beta_thread = new BetaDistribution();
            BetaDistribution beta_distrib1 = new BetaDistribution();
            BetaDistribution beta_distrib2 = new BetaDistribution();

            UniformDistribution unif_thread = new UniformDistribution();
            UniformDistribution unif_distrib1 = new UniformDistribution();
            UniformDistribution unif_distrib2 = new UniformDistribution();

            DetermineDistribution deter_thread = new DetermineDistribution();
            DetermineDistribution deter_distrib1 = new DetermineDistribution();
            DetermineDistribution deter_distrib2 = new DetermineDistribution();

            MMPP mmpp_thread = new MMPP();

            double lyambda;
            double mu;
            double alpha;
            double beta;
            double param;
            double sigma1;
            double sigma2;

            string lyambda1,Q;

            //число заявок
            int xmax = Convert.ToInt32(textBox3.Text);

            //параметры распределения ИПВ на 1 и 2 фазах
            sigma1 =  Convert.ToDouble(textBox4.Text);

            sigma2 = Convert.ToDouble(textBox38.Text);

            //вероятность потери заявки
            double prob = Convert.ToDouble(numericUpDown3.Value);

            //создается пуассоновский поток с заданным параметром
            if (radioButton1.Checked == true)
            {
                lyambda = Convert.ToDouble(textBox1.Text);
                exp_thread = new ExpDistribution(lyambda);
            }

            //создается MMPP-поток 
            if (radioButton2.Checked == true)
            {
                lyambda1 = Convert.ToString(textBox28.Text);
                Q = Convert.ToString(textBox29.Text);

                Matrix QQ = new Matrix(Q);
                Matrix lyambda2 = new Matrix(lyambda1);

                mmpp_thread = new MMPP(QQ, lyambda2);

            }
            //создается рекуррентный поток с заданным распределением
            if (radioButton3.Checked == true)
            {
                //гамма
                if (radioButton14.Checked == true)
                {
                    alpha = Convert.ToDouble(textBox21.Text);
                    beta = Convert.ToDouble(textBox23.Text);

                    gamma_thread = new GammaDistribution(alpha, beta);
                }
                //бета
                if (radioButton15.Checked == true)
                {
                    alpha = Convert.ToDouble(textBox22.Text);
                    beta = Convert.ToDouble(textBox24.Text);

                    beta_thread = new BetaDistribution(alpha, beta);
                }
                //равномерное
                if (radioButton16.Checked == true)
                {
                    alpha = Convert.ToDouble(textBox25.Text);
                    beta = Convert.ToDouble(textBox26.Text);

                    unif_thread = new UniformDistribution(alpha, beta);
                }
                //детерминированное
                if (radioButton17.Checked == true)
                {
                    param = Convert.ToDouble(textBox27.Text);

                    deter_thread = new DetermineDistribution(param);
                }
            }
            //создание класса, отвечающего 
            //за распределения времени 
            //обслуживания на 1 фазе

            //экспоненциальное
            if (radioButton4.Checked == true)
            {
                mu = Convert.ToDouble(textBox2.Text);
                exp_distrib1 = new ExpDistribution(mu);
            }
            //гамма
            if (radioButton10.Checked == true)
            {
                alpha = Convert.ToDouble(textBox14.Text);
                beta = Convert.ToDouble(textBox15.Text);

                gamma_distrib1 = new GammaDistribution(alpha, beta);
            }
            //бета
            if (radioButton11.Checked == true)
            {
                alpha = Convert.ToDouble(textBox16.Text);
                beta = Convert.ToDouble(textBox17.Text);

                beta_distrib1 = new BetaDistribution(alpha, beta);
            }
            //равномерное
            if (radioButton12.Checked == true)
            {
                alpha = Convert.ToDouble(textBox18.Text);
                beta = Convert.ToDouble(textBox19.Text);

                unif_distrib1 = new UniformDistribution(alpha, beta);
            }
            //детерминированное
            if (radioButton13.Checked == true)
            {
                param = Convert.ToDouble(textBox20.Text);

                deter_distrib1 = new DetermineDistribution(param);
            }

            //создание класса, отвечающего 
            //за распределения времени 
            //обслуживания на 2 фазе

            //экспоненциальное
                if (radioButton22.Checked == true)
                {
                    mu = Convert.ToDouble(textBox37.Text);
                    exp_distrib2 = new ExpDistribution(mu);
                }
            //гамма
                if (radioButton21.Checked == true)
                {
                    alpha = Convert.ToDouble(textBox36.Text);
                    beta = Convert.ToDouble(textBox35.Text);

                    gamma_distrib2 = new GammaDistribution(alpha, beta);
                }
            //бета
                if (radioButton20.Checked == true)
                {
                    alpha = Convert.ToDouble(textBox34.Text);
                    beta = Convert.ToDouble(textBox33.Text);

                    beta_distrib2 = new BetaDistribution(alpha, beta);
                }
            //равномерное
                if (radioButton19.Checked == true)
                {
                    alpha = Convert.ToDouble(textBox32.Text);
                    beta = Convert.ToDouble(textBox31.Text);

                    unif_distrib2 = new UniformDistribution(alpha, beta);
                }
            //детерминированное
                if (radioButton18.Checked == true)
                {
                    param = Convert.ToDouble(textBox30.Text);

                    deter_distrib2 = new DetermineDistribution(param);
                }

                
            //создается объект универсального класса
            //куда передаются соответствующие типы        

                if (radioButton1.Checked) 
                {
                    if (radioButton4.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<ExpDistribution, ExpDistribution, ExpDistribution> model = new SimulationModel<ExpDistribution, ExpDistribution, ExpDistribution>(xmax, exp_thread, exp_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<ExpDistribution, ExpDistribution, GammaDistribution> model = new SimulationModel<ExpDistribution, ExpDistribution, GammaDistribution>(xmax, exp_thread, exp_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<ExpDistribution, ExpDistribution, BetaDistribution> model = new SimulationModel<ExpDistribution, ExpDistribution, BetaDistribution>(xmax, exp_thread, exp_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<ExpDistribution, ExpDistribution, UniformDistribution> model = new SimulationModel<ExpDistribution, ExpDistribution, UniformDistribution>(xmax, exp_thread, exp_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<ExpDistribution, ExpDistribution, DetermineDistribution> model = new SimulationModel<ExpDistribution, ExpDistribution, DetermineDistribution>(xmax, exp_thread, exp_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                    if (radioButton10.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<ExpDistribution, GammaDistribution, ExpDistribution> model = new SimulationModel<ExpDistribution, GammaDistribution, ExpDistribution>(xmax, exp_thread, gamma_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<ExpDistribution, GammaDistribution, GammaDistribution> model = new SimulationModel<ExpDistribution, GammaDistribution, GammaDistribution>(xmax, exp_thread, gamma_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<ExpDistribution, GammaDistribution, BetaDistribution> model = new SimulationModel<ExpDistribution, GammaDistribution, BetaDistribution>(xmax, exp_thread, gamma_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<ExpDistribution, GammaDistribution, UniformDistribution> model = new SimulationModel<ExpDistribution, GammaDistribution, UniformDistribution>(xmax, exp_thread, gamma_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<ExpDistribution, GammaDistribution, DetermineDistribution> model = new SimulationModel<ExpDistribution, GammaDistribution, DetermineDistribution>(xmax, exp_thread, gamma_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                    if (radioButton11.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<ExpDistribution, BetaDistribution, ExpDistribution> model = new SimulationModel<ExpDistribution, BetaDistribution, ExpDistribution>(xmax, exp_thread, beta_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<ExpDistribution, BetaDistribution, GammaDistribution> model = new SimulationModel<ExpDistribution, BetaDistribution, GammaDistribution>(xmax, exp_thread, beta_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<ExpDistribution, BetaDistribution, BetaDistribution> model = new SimulationModel<ExpDistribution, BetaDistribution, BetaDistribution>(xmax, exp_thread, beta_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<ExpDistribution, BetaDistribution, UniformDistribution> model = new SimulationModel<ExpDistribution, BetaDistribution, UniformDistribution>(xmax, exp_thread, beta_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<ExpDistribution, BetaDistribution, DetermineDistribution> model = new SimulationModel<ExpDistribution, BetaDistribution, DetermineDistribution>(xmax, exp_thread, beta_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                    if (radioButton12.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<ExpDistribution, UniformDistribution, ExpDistribution> model = new SimulationModel<ExpDistribution, UniformDistribution, ExpDistribution>(xmax, exp_thread, unif_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<ExpDistribution, UniformDistribution, GammaDistribution> model = new SimulationModel<ExpDistribution, UniformDistribution, GammaDistribution>(xmax, exp_thread, unif_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<ExpDistribution, UniformDistribution, BetaDistribution> model = new SimulationModel<ExpDistribution, UniformDistribution, BetaDistribution>(xmax, exp_thread, unif_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<ExpDistribution, UniformDistribution, UniformDistribution> model = new SimulationModel<ExpDistribution, UniformDistribution, UniformDistribution>(xmax, exp_thread, unif_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<ExpDistribution, UniformDistribution, DetermineDistribution> model = new SimulationModel<ExpDistribution, UniformDistribution, DetermineDistribution>(xmax, exp_thread, unif_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                    if (radioButton13.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<ExpDistribution, DetermineDistribution, ExpDistribution> model = new SimulationModel<ExpDistribution, DetermineDistribution, ExpDistribution>(xmax, exp_thread, deter_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<ExpDistribution, DetermineDistribution, GammaDistribution> model = new SimulationModel<ExpDistribution, DetermineDistribution, GammaDistribution>(xmax, exp_thread, deter_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<ExpDistribution, DetermineDistribution, BetaDistribution> model = new SimulationModel<ExpDistribution, DetermineDistribution, BetaDistribution>(xmax, exp_thread, deter_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<ExpDistribution, DetermineDistribution, UniformDistribution> model = new SimulationModel<ExpDistribution, DetermineDistribution, UniformDistribution>(xmax, exp_thread, deter_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<ExpDistribution, DetermineDistribution, DetermineDistribution> model = new SimulationModel<ExpDistribution, DetermineDistribution, DetermineDistribution>(xmax, exp_thread, deter_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                }
                if (radioButton2.Checked)
                {
                    if (radioButton4.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<MMPP, ExpDistribution, ExpDistribution> model = new SimulationModel<MMPP, ExpDistribution, ExpDistribution>(xmax, mmpp_thread, exp_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<MMPP, ExpDistribution, GammaDistribution> model = new SimulationModel<MMPP, ExpDistribution, GammaDistribution>(xmax, mmpp_thread, exp_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<MMPP, ExpDistribution, BetaDistribution> model = new SimulationModel<MMPP, ExpDistribution, BetaDistribution>(xmax, mmpp_thread, exp_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<MMPP, ExpDistribution, UniformDistribution> model = new SimulationModel<MMPP, ExpDistribution, UniformDistribution>(xmax, mmpp_thread, exp_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<MMPP, ExpDistribution, DetermineDistribution> model = new SimulationModel<MMPP, ExpDistribution, DetermineDistribution>(xmax, mmpp_thread, exp_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                    if (radioButton10.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<MMPP, GammaDistribution, ExpDistribution> model = new SimulationModel<MMPP, GammaDistribution, ExpDistribution>(xmax, mmpp_thread, gamma_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<MMPP, GammaDistribution, GammaDistribution> model = new SimulationModel<MMPP, GammaDistribution, GammaDistribution>(xmax, mmpp_thread, gamma_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<MMPP, GammaDistribution, BetaDistribution> model = new SimulationModel<MMPP, GammaDistribution, BetaDistribution>(xmax, mmpp_thread, gamma_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<MMPP, GammaDistribution, UniformDistribution> model = new SimulationModel<MMPP, GammaDistribution, UniformDistribution>(xmax, mmpp_thread, gamma_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<MMPP, GammaDistribution, DetermineDistribution> model = new SimulationModel<MMPP, GammaDistribution, DetermineDistribution>(xmax, mmpp_thread, gamma_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                    if (radioButton11.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<MMPP, BetaDistribution, ExpDistribution> model = new SimulationModel<MMPP, BetaDistribution, ExpDistribution>(xmax, mmpp_thread, beta_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<MMPP, BetaDistribution, GammaDistribution> model = new SimulationModel<MMPP, BetaDistribution, GammaDistribution>(xmax, mmpp_thread, beta_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<MMPP, BetaDistribution, BetaDistribution> model = new SimulationModel<MMPP, BetaDistribution, BetaDistribution>(xmax, mmpp_thread, beta_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<MMPP, BetaDistribution, UniformDistribution> model = new SimulationModel<MMPP, BetaDistribution, UniformDistribution>(xmax, mmpp_thread, beta_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<MMPP, BetaDistribution, DetermineDistribution> model = new SimulationModel<MMPP, BetaDistribution, DetermineDistribution>(xmax, mmpp_thread, beta_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                    if (radioButton12.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<MMPP, UniformDistribution, ExpDistribution> model = new SimulationModel<MMPP, UniformDistribution, ExpDistribution>(xmax, mmpp_thread, unif_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<MMPP, UniformDistribution, GammaDistribution> model = new SimulationModel<MMPP, UniformDistribution, GammaDistribution>(xmax, mmpp_thread, unif_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<MMPP, UniformDistribution, BetaDistribution> model = new SimulationModel<MMPP, UniformDistribution, BetaDistribution>(xmax, mmpp_thread, unif_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<MMPP, UniformDistribution, UniformDistribution> model = new SimulationModel<MMPP, UniformDistribution, UniformDistribution>(xmax, mmpp_thread, unif_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<MMPP, UniformDistribution, DetermineDistribution> model = new SimulationModel<MMPP, UniformDistribution, DetermineDistribution>(xmax, mmpp_thread, unif_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                    if (radioButton13.Checked)
                    {
                        if (radioButton22.Checked)
                        {
                            SimulationModel<MMPP, DetermineDistribution, ExpDistribution> model = new SimulationModel<MMPP, DetermineDistribution, ExpDistribution>(xmax, mmpp_thread, deter_distrib1, exp_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton21.Checked)
                        {
                            SimulationModel<MMPP, DetermineDistribution, GammaDistribution> model = new SimulationModel<MMPP, DetermineDistribution, GammaDistribution>(xmax, mmpp_thread, deter_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton20.Checked)
                        {
                            SimulationModel<MMPP, DetermineDistribution, BetaDistribution> model = new SimulationModel<MMPP, DetermineDistribution, BetaDistribution>(xmax, mmpp_thread, deter_distrib1, beta_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton19.Checked)
                        {
                            SimulationModel<MMPP, DetermineDistribution, UniformDistribution> model = new SimulationModel<MMPP, DetermineDistribution, UniformDistribution>(xmax, mmpp_thread, deter_distrib1, unif_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                        if (radioButton18.Checked)
                        {
                            SimulationModel<MMPP, DetermineDistribution, DetermineDistribution> model = new SimulationModel<MMPP, DetermineDistribution, DetermineDistribution>(xmax, mmpp_thread, deter_distrib1, deter_distrib2, sigma1, sigma2, prob);
                            this.ModelResult(model);
                        }
                    }
                }
                if (radioButton3.Checked)
                {
                    if (radioButton14.Checked)
                    {
                        if (radioButton4.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<GammaDistribution, ExpDistribution, ExpDistribution> model = new SimulationModel<GammaDistribution, ExpDistribution, ExpDistribution>(xmax, gamma_thread, exp_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<GammaDistribution, ExpDistribution, GammaDistribution> model = new SimulationModel<GammaDistribution, ExpDistribution, GammaDistribution>(xmax, gamma_thread, exp_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<GammaDistribution, ExpDistribution, BetaDistribution> model = new SimulationModel<GammaDistribution, ExpDistribution, BetaDistribution>(xmax, gamma_thread, exp_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<GammaDistribution, ExpDistribution, UniformDistribution> model = new SimulationModel<GammaDistribution, ExpDistribution, UniformDistribution>(xmax, gamma_thread, exp_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<GammaDistribution, ExpDistribution, DetermineDistribution> model = new SimulationModel<GammaDistribution, ExpDistribution, DetermineDistribution>(xmax, gamma_thread, exp_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton10.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<GammaDistribution, GammaDistribution, ExpDistribution> model = new SimulationModel<GammaDistribution, GammaDistribution, ExpDistribution>(xmax, gamma_thread, gamma_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<GammaDistribution, GammaDistribution, GammaDistribution> model = new SimulationModel<GammaDistribution, GammaDistribution, GammaDistribution>(xmax, gamma_thread, gamma_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<GammaDistribution, GammaDistribution, BetaDistribution> model = new SimulationModel<GammaDistribution, GammaDistribution, BetaDistribution>(xmax, gamma_thread, gamma_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<GammaDistribution, GammaDistribution, UniformDistribution> model = new SimulationModel<GammaDistribution, GammaDistribution, UniformDistribution>(xmax, gamma_thread, gamma_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<GammaDistribution, GammaDistribution, DetermineDistribution> model = new SimulationModel<GammaDistribution, GammaDistribution, DetermineDistribution>(xmax, gamma_thread, gamma_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton11.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<GammaDistribution, BetaDistribution, ExpDistribution> model = new SimulationModel<GammaDistribution, BetaDistribution, ExpDistribution>(xmax, gamma_thread, beta_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<GammaDistribution, BetaDistribution, GammaDistribution> model = new SimulationModel<GammaDistribution, BetaDistribution, GammaDistribution>(xmax, gamma_thread, beta_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<GammaDistribution, BetaDistribution, BetaDistribution> model = new SimulationModel<GammaDistribution, BetaDistribution, BetaDistribution>(xmax, gamma_thread, beta_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<GammaDistribution, BetaDistribution, UniformDistribution> model = new SimulationModel<GammaDistribution, BetaDistribution, UniformDistribution>(xmax, gamma_thread, beta_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<GammaDistribution, BetaDistribution, DetermineDistribution> model = new SimulationModel<GammaDistribution, BetaDistribution, DetermineDistribution>(xmax, gamma_thread, beta_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton12.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<GammaDistribution, UniformDistribution, ExpDistribution> model = new SimulationModel<GammaDistribution, UniformDistribution, ExpDistribution>(xmax, gamma_thread, unif_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<GammaDistribution, UniformDistribution, GammaDistribution> model = new SimulationModel<GammaDistribution, UniformDistribution, GammaDistribution>(xmax, gamma_thread, unif_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<GammaDistribution, UniformDistribution, BetaDistribution> model = new SimulationModel<GammaDistribution, UniformDistribution, BetaDistribution>(xmax, gamma_thread, unif_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<GammaDistribution, UniformDistribution, UniformDistribution> model = new SimulationModel<GammaDistribution, UniformDistribution, UniformDistribution>(xmax, gamma_thread, unif_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<GammaDistribution, UniformDistribution, DetermineDistribution> model = new SimulationModel<GammaDistribution, UniformDistribution, DetermineDistribution>(xmax, gamma_thread, unif_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton13.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<GammaDistribution, DetermineDistribution, ExpDistribution> model = new SimulationModel<GammaDistribution, DetermineDistribution, ExpDistribution>(xmax, gamma_thread, deter_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<GammaDistribution, DetermineDistribution, GammaDistribution> model = new SimulationModel<GammaDistribution, DetermineDistribution, GammaDistribution>(xmax, gamma_thread, deter_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<GammaDistribution, DetermineDistribution, BetaDistribution> model = new SimulationModel<GammaDistribution, DetermineDistribution, BetaDistribution>(xmax, gamma_thread, deter_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<GammaDistribution, DetermineDistribution, UniformDistribution> model = new SimulationModel<GammaDistribution, DetermineDistribution, UniformDistribution>(xmax, gamma_thread, deter_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<GammaDistribution, DetermineDistribution, DetermineDistribution> model = new SimulationModel<GammaDistribution, DetermineDistribution, DetermineDistribution>(xmax, gamma_thread, deter_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                    }
                    if (radioButton15.Checked)
                    {
                        if (radioButton4.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<BetaDistribution, ExpDistribution, ExpDistribution> model = new SimulationModel<BetaDistribution, ExpDistribution, ExpDistribution>(xmax, beta_thread, exp_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<BetaDistribution, ExpDistribution, GammaDistribution> model = new SimulationModel<BetaDistribution, ExpDistribution, GammaDistribution>(xmax, beta_thread, exp_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<BetaDistribution, ExpDistribution, BetaDistribution> model = new SimulationModel<BetaDistribution, ExpDistribution, BetaDistribution>(xmax, beta_thread, exp_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<BetaDistribution, ExpDistribution, UniformDistribution> model = new SimulationModel<BetaDistribution, ExpDistribution, UniformDistribution>(xmax, beta_thread, exp_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<BetaDistribution, ExpDistribution, DetermineDistribution> model = new SimulationModel<BetaDistribution, ExpDistribution, DetermineDistribution>(xmax, beta_thread, exp_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton10.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<BetaDistribution, GammaDistribution, ExpDistribution> model = new SimulationModel<BetaDistribution, GammaDistribution, ExpDistribution>(xmax, beta_thread, gamma_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<BetaDistribution, GammaDistribution, GammaDistribution> model = new SimulationModel<BetaDistribution, GammaDistribution, GammaDistribution>(xmax, beta_thread, gamma_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<BetaDistribution, GammaDistribution, BetaDistribution> model = new SimulationModel<BetaDistribution, GammaDistribution, BetaDistribution>(xmax, beta_thread, gamma_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<BetaDistribution, GammaDistribution, UniformDistribution> model = new SimulationModel<BetaDistribution, GammaDistribution, UniformDistribution>(xmax, beta_thread, gamma_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<BetaDistribution, GammaDistribution, DetermineDistribution> model = new SimulationModel<BetaDistribution, GammaDistribution, DetermineDistribution>(xmax, beta_thread, gamma_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton11.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<BetaDistribution, BetaDistribution, ExpDistribution> model = new SimulationModel<BetaDistribution, BetaDistribution, ExpDistribution>(xmax, beta_thread, beta_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<BetaDistribution, BetaDistribution, GammaDistribution> model = new SimulationModel<BetaDistribution, BetaDistribution, GammaDistribution>(xmax, beta_thread, beta_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<BetaDistribution, BetaDistribution, BetaDistribution> model = new SimulationModel<BetaDistribution, BetaDistribution, BetaDistribution>(xmax, beta_thread, beta_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<BetaDistribution, BetaDistribution, UniformDistribution> model = new SimulationModel<BetaDistribution, BetaDistribution, UniformDistribution>(xmax, beta_thread, beta_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<BetaDistribution, BetaDistribution, DetermineDistribution> model = new SimulationModel<BetaDistribution, BetaDistribution, DetermineDistribution>(xmax, beta_thread, beta_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton12.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<BetaDistribution, UniformDistribution, ExpDistribution> model = new SimulationModel<BetaDistribution, UniformDistribution, ExpDistribution>(xmax, beta_thread, unif_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<BetaDistribution, UniformDistribution, GammaDistribution> model = new SimulationModel<BetaDistribution, UniformDistribution, GammaDistribution>(xmax, beta_thread, unif_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<BetaDistribution, UniformDistribution, BetaDistribution> model = new SimulationModel<BetaDistribution, UniformDistribution, BetaDistribution>(xmax, beta_thread, unif_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<BetaDistribution, UniformDistribution, UniformDistribution> model = new SimulationModel<BetaDistribution, UniformDistribution, UniformDistribution>(xmax, beta_thread, unif_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<BetaDistribution, UniformDistribution, DetermineDistribution> model = new SimulationModel<BetaDistribution, UniformDistribution, DetermineDistribution>(xmax, beta_thread, unif_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton13.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<BetaDistribution, DetermineDistribution, ExpDistribution> model = new SimulationModel<BetaDistribution, DetermineDistribution, ExpDistribution>(xmax, beta_thread, deter_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<BetaDistribution, DetermineDistribution, GammaDistribution> model = new SimulationModel<BetaDistribution, DetermineDistribution, GammaDistribution>(xmax, beta_thread, deter_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<BetaDistribution, DetermineDistribution, BetaDistribution> model = new SimulationModel<BetaDistribution, DetermineDistribution, BetaDistribution>(xmax, beta_thread, deter_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<BetaDistribution, DetermineDistribution, UniformDistribution> model = new SimulationModel<BetaDistribution, DetermineDistribution, UniformDistribution>(xmax, beta_thread, deter_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<BetaDistribution, DetermineDistribution, DetermineDistribution> model = new SimulationModel<BetaDistribution, DetermineDistribution, DetermineDistribution>(xmax, beta_thread, deter_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                    }
                    if (radioButton16.Checked)
                    {
                        if (radioButton4.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<UniformDistribution, ExpDistribution, ExpDistribution> model = new SimulationModel<UniformDistribution, ExpDistribution, ExpDistribution>(xmax, unif_thread, exp_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<UniformDistribution, ExpDistribution, GammaDistribution> model = new SimulationModel<UniformDistribution, ExpDistribution, GammaDistribution>(xmax, unif_thread, exp_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<UniformDistribution, ExpDistribution, BetaDistribution> model = new SimulationModel<UniformDistribution, ExpDistribution, BetaDistribution>(xmax, unif_thread, exp_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<UniformDistribution, ExpDistribution, UniformDistribution> model = new SimulationModel<UniformDistribution, ExpDistribution, UniformDistribution>(xmax, unif_thread, exp_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<UniformDistribution, ExpDistribution, DetermineDistribution> model = new SimulationModel<UniformDistribution, ExpDistribution, DetermineDistribution>(xmax, unif_thread, exp_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton10.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<UniformDistribution, GammaDistribution, ExpDistribution> model = new SimulationModel<UniformDistribution, GammaDistribution, ExpDistribution>(xmax, unif_thread, gamma_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<UniformDistribution, GammaDistribution, GammaDistribution> model = new SimulationModel<UniformDistribution, GammaDistribution, GammaDistribution>(xmax, unif_thread, gamma_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<UniformDistribution, GammaDistribution, BetaDistribution> model = new SimulationModel<UniformDistribution, GammaDistribution, BetaDistribution>(xmax, unif_thread, gamma_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<UniformDistribution, GammaDistribution, UniformDistribution> model = new SimulationModel<UniformDistribution, GammaDistribution, UniformDistribution>(xmax, unif_thread, gamma_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<UniformDistribution, GammaDistribution, DetermineDistribution> model = new SimulationModel<UniformDistribution, GammaDistribution, DetermineDistribution>(xmax, unif_thread, gamma_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton11.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<UniformDistribution, BetaDistribution, ExpDistribution> model = new SimulationModel<UniformDistribution, BetaDistribution, ExpDistribution>(xmax, unif_thread, beta_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<UniformDistribution, BetaDistribution, GammaDistribution> model = new SimulationModel<UniformDistribution, BetaDistribution, GammaDistribution>(xmax, unif_thread, beta_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<UniformDistribution, BetaDistribution, BetaDistribution> model = new SimulationModel<UniformDistribution, BetaDistribution, BetaDistribution>(xmax, unif_thread, beta_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<UniformDistribution, BetaDistribution, UniformDistribution> model = new SimulationModel<UniformDistribution, BetaDistribution, UniformDistribution>(xmax, unif_thread, beta_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<UniformDistribution, BetaDistribution, DetermineDistribution> model = new SimulationModel<UniformDistribution, BetaDistribution, DetermineDistribution>(xmax, unif_thread, beta_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton12.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<UniformDistribution, UniformDistribution, ExpDistribution> model = new SimulationModel<UniformDistribution, UniformDistribution, ExpDistribution>(xmax, unif_thread, unif_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<UniformDistribution, UniformDistribution, GammaDistribution> model = new SimulationModel<UniformDistribution, UniformDistribution, GammaDistribution>(xmax,unif_thread, unif_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<UniformDistribution, UniformDistribution, BetaDistribution> model = new SimulationModel<UniformDistribution, UniformDistribution, BetaDistribution>(xmax, unif_thread, unif_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<UniformDistribution, UniformDistribution, UniformDistribution> model = new SimulationModel<UniformDistribution, UniformDistribution, UniformDistribution>(xmax, unif_thread, unif_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<UniformDistribution, UniformDistribution, DetermineDistribution> model = new SimulationModel<UniformDistribution, UniformDistribution, DetermineDistribution>(xmax, unif_thread, unif_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton13.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<UniformDistribution, DetermineDistribution, ExpDistribution> model = new SimulationModel<UniformDistribution, DetermineDistribution, ExpDistribution>(xmax, unif_thread, deter_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<UniformDistribution, DetermineDistribution, GammaDistribution> model = new SimulationModel<UniformDistribution, DetermineDistribution, GammaDistribution>(xmax, unif_thread, deter_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<UniformDistribution, DetermineDistribution, BetaDistribution> model = new SimulationModel<UniformDistribution, DetermineDistribution, BetaDistribution>(xmax, unif_thread, deter_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<UniformDistribution, DetermineDistribution, UniformDistribution> model = new SimulationModel<UniformDistribution, DetermineDistribution, UniformDistribution>(xmax, unif_thread, deter_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<UniformDistribution, DetermineDistribution, DetermineDistribution> model = new SimulationModel<UniformDistribution, DetermineDistribution, DetermineDistribution>(xmax, unif_thread, deter_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                    }
                    if (radioButton17.Checked)
                    {
                        if (radioButton4.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<DetermineDistribution, ExpDistribution, ExpDistribution> model = new SimulationModel<DetermineDistribution, ExpDistribution, ExpDistribution>(xmax, deter_thread, exp_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<DetermineDistribution, ExpDistribution, GammaDistribution> model = new SimulationModel<DetermineDistribution, ExpDistribution, GammaDistribution>(xmax, deter_thread, exp_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<DetermineDistribution, ExpDistribution, BetaDistribution> model = new SimulationModel<DetermineDistribution, ExpDistribution, BetaDistribution>(xmax, deter_thread, exp_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<DetermineDistribution, ExpDistribution, UniformDistribution> model = new SimulationModel<DetermineDistribution, ExpDistribution, UniformDistribution>(xmax, deter_thread, exp_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<DetermineDistribution, ExpDistribution, DetermineDistribution> model = new SimulationModel<DetermineDistribution, ExpDistribution, DetermineDistribution>(xmax, deter_thread, exp_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton10.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<DetermineDistribution, GammaDistribution, ExpDistribution> model = new SimulationModel<DetermineDistribution, GammaDistribution, ExpDistribution>(xmax, deter_thread, gamma_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<DetermineDistribution, GammaDistribution, GammaDistribution> model = new SimulationModel<DetermineDistribution, GammaDistribution, GammaDistribution>(xmax, deter_thread, gamma_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<DetermineDistribution, GammaDistribution, BetaDistribution> model = new SimulationModel<DetermineDistribution, GammaDistribution, BetaDistribution>(xmax, deter_thread, gamma_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<DetermineDistribution, GammaDistribution, UniformDistribution> model = new SimulationModel<DetermineDistribution, GammaDistribution, UniformDistribution>(xmax, deter_thread, gamma_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<DetermineDistribution, GammaDistribution, DetermineDistribution> model = new SimulationModel<DetermineDistribution, GammaDistribution, DetermineDistribution>(xmax, deter_thread, gamma_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton11.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<DetermineDistribution, BetaDistribution, ExpDistribution> model = new SimulationModel<DetermineDistribution, BetaDistribution, ExpDistribution>(xmax, deter_thread, beta_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<DetermineDistribution, BetaDistribution, GammaDistribution> model = new SimulationModel<DetermineDistribution, BetaDistribution, GammaDistribution>(xmax, deter_thread, beta_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<DetermineDistribution, BetaDistribution, BetaDistribution> model = new SimulationModel<DetermineDistribution, BetaDistribution, BetaDistribution>(xmax, deter_thread, beta_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<DetermineDistribution, BetaDistribution, UniformDistribution> model = new SimulationModel<DetermineDistribution, BetaDistribution, UniformDistribution>(xmax, deter_thread, beta_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<DetermineDistribution, BetaDistribution, DetermineDistribution> model = new SimulationModel<DetermineDistribution, BetaDistribution, DetermineDistribution>(xmax, deter_thread, beta_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton12.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<DetermineDistribution, UniformDistribution, ExpDistribution> model = new SimulationModel<DetermineDistribution, UniformDistribution, ExpDistribution>(xmax, deter_thread, unif_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<DetermineDistribution, UniformDistribution, GammaDistribution> model = new SimulationModel<DetermineDistribution, UniformDistribution, GammaDistribution>(xmax, deter_thread, unif_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<DetermineDistribution, UniformDistribution, BetaDistribution> model = new SimulationModel<DetermineDistribution, UniformDistribution, BetaDistribution>(xmax, deter_thread, unif_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<DetermineDistribution, UniformDistribution, UniformDistribution> model = new SimulationModel<DetermineDistribution, UniformDistribution, UniformDistribution>(xmax, deter_thread, unif_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<DetermineDistribution, UniformDistribution, DetermineDistribution> model = new SimulationModel<DetermineDistribution, UniformDistribution, DetermineDistribution>(xmax, deter_thread, unif_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                        if (radioButton13.Checked)
                        {
                            if (radioButton22.Checked)
                            {
                                SimulationModel<DetermineDistribution, DetermineDistribution, ExpDistribution> model = new SimulationModel<DetermineDistribution, DetermineDistribution, ExpDistribution>(xmax, deter_thread, deter_distrib1, exp_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton21.Checked)
                            {
                                SimulationModel<DetermineDistribution, DetermineDistribution, GammaDistribution> model = new SimulationModel<DetermineDistribution, DetermineDistribution, GammaDistribution>(xmax, deter_thread, deter_distrib1, gamma_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton20.Checked)
                            {
                                SimulationModel<DetermineDistribution, DetermineDistribution, BetaDistribution> model = new SimulationModel<DetermineDistribution, DetermineDistribution, BetaDistribution>(xmax, deter_thread, deter_distrib1, beta_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton19.Checked)
                            {
                                SimulationModel<DetermineDistribution, DetermineDistribution, UniformDistribution> model = new SimulationModel<DetermineDistribution, DetermineDistribution, UniformDistribution>(xmax, deter_thread, deter_distrib1, unif_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                            if (radioButton18.Checked)
                            {
                                SimulationModel<DetermineDistribution, DetermineDistribution, DetermineDistribution> model = new SimulationModel<DetermineDistribution, DetermineDistribution, DetermineDistribution>(xmax, deter_thread, deter_distrib1, deter_distrib2, sigma1, sigma2, prob);
                                this.ModelResult(model);
                            }
                        }
                    }
                }
            // Получим панель для рисования
            GraphPane pane1 = zedGraphControl3.GraphPane;

            GraphPane pane2 = zedGraphControl4.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane1.CurveList.Clear();

            pane2.CurveList.Clear();

            // распределение числа заявок в ИПВ 1
            PointPairList list1 = new PointPairList();
            //распределение числа заявок в ИПВ 2
            PointPairList list2 = new PointPairList();
   
            // Заполняем список точек

            for (int x = 0; x < BufferCount1; x++)
            {
                // добавим в список точку
                list1.Add(x, BufferProbability1[x]);
            }
            for (int x = 0; x < BufferCount2; x++)
            {
                // добавим в список точку
                list2.Add(x, BufferProbability2[x]);
            }
            LineItem myCurve1 = pane1.AddCurve("", list1, Color.Black, SymbolType.None);

            LineItem myCurve2 = pane2.AddCurve("", list2, Color.Black, SymbolType.None);

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphControl3.AxisChange();

            zedGraphControl4.AxisChange();

            // Обновляем график
            zedGraphControl3.Invalidate();

            zedGraphControl4.Invalidate();

            int[] arr = new int[2];
            String[] str = new String[4];
            int index = 0;
            double Prob;
            //есть ли в предыдущей вероятности знаки после запятой
            Boolean flag=false;
            String S, s;
            int found = 0;
            int k;

            if ((!File.Exists("ИПВ1.txt")) && (!File.Exists("ИПВ2.txt")) &&
                (!File.Exists("Двумерное распределение.txt")) && (!File.Exists("Результаты моделирования.txt")) &&
                (!File.Exists("Стационарное распределение.txt")))
            {
                File.Create("ИПВ1.txt").Close();
                File.Create("ИПВ2.txt").Close();
                File.Create("Стационарное распределение.txt").Close();
                File.Create("Двумерное распределение.txt").Close();
                File.Create("Результаты моделирования.txt").Close();
            }

            StreamWriter sw1 = new StreamWriter("ИПВ1.txt");
            for (int x = 0; x < BufferCount1; x++)
            {
                if (x < 10)
                    sw1.WriteLine(x + "    " + BufferProbability1[x].ToString("0.###"));
                if ((x >= 10) & (x < 100))
                    sw1.WriteLine(x + "   " + BufferProbability1[x].ToString("0.###"));
                if (x >= 100)
                    sw1.WriteLine(x + "  " + BufferProbability1[x].ToString("0.###"));
            }
            sw1.Close();

            StreamWriter sw2 = new StreamWriter("ИПВ2.txt");
            for (int x = 0; x < BufferCount2; x++)
            {
                if (x < 10)
                    sw2.WriteLine(x + "    " + BufferProbability2[x].ToString("0.###"));
                if ((x >= 10) & (x < 100))
                    sw2.WriteLine(x + "   " + BufferProbability2[x].ToString("0.###"));
                if (x >= 100)
                    sw2.WriteLine(x + "  " + BufferProbability2[x].ToString("0.###"));
            }
            sw2.Close();

            str[0] = label13.Text;
            str[1] = label15.Text;
            str[2] = label14.Text;
            str[3] = label16.Text;

            StreamWriter sw3 = new StreamWriter("Результаты моделирования.txt");
            sw3.WriteLine("Общее время моделирования:" + " " + label4.Text);
            sw3.WriteLine();
            sw3.WriteLine("Матрица ковариации:");
            sw3.WriteLine("       "+"ИПВ1"+"    "+"ИПВ2");
            sw3.Write("ИПВ1" + "  ");

            for (int i = 0; i < 4; i++)
            {
                if (i == 2)
                {
                    sw3.WriteLine();
                    sw3.Write("ИПВ2" + "  ");
                }
                sw3.Write(str[i]);
                S = str[i];
                //находим индекс разделителя
                found = S.IndexOf(",");
                //выделяем подстроку после запятой
                s = S.Substring(found + 1);
                //считаем число знаков после запятой
                k = s.Length;
                //если их меньше 4, добавляем пробелы
                for (int z = k; z < 4; z++)
                    sw3.Write(" ");
                sw3.Write("  ");
            }
            sw3.WriteLine();
            sw3.WriteLine("Вектор матожиданий:");
            sw3.WriteLine("ИПВ1" + "  " + label51.Text);
            sw3.WriteLine("ИПВ2" + "  " + label57.Text);
            sw3.Close();

            StreamWriter sw4 = new StreamWriter("Двумерное распределение.txt");
            sw4.Write("   ИПВ2");
            sw4.WriteLine();
            sw4.Write("           ");

          //  for (int i = 0; i < BufferCount2; i++)
                //sw4.Write(i + "            ");
            
            for (int i = 0; i < BufferCount1; i++)
            {
                sw4.WriteLine();
                for (int j = 0; j < BufferCount2; j++)
                {
                    arr[0] = i;
                    arr[1] = j;

                    Prob = 0;

                    //поиск индекса, соответсвующего значениям
                    //i и j числа заявок в каждом ИПВ
                    index = BufferState.FindIndex(x =>
                    {
                        if (x.Length != arr.Length) return false;
                        for (int z = 0; z < arr.Length; z++)
                            if (x[z] != arr[z])
                                return false;
                        return true;
                    });

                    //если нашлись такие значения
                    if (index != -1)

                        Prob = BufferProbability[index];
                    /*
                    //если находимся в начале строки
                    if (j == 0)
                    {
                        if (i == 0)
                            sw4.Write("И " + i + "    ");
                        if (i == 1)
                            sw4.Write("П " + i + "    ");
                        if (i == 2)
                            sw4.Write("В " + i + "    ");
                        if (i == 3)
                            sw4.Write("1 " + i + "    ");
                        if (i > 3)
                        {
                            if (i < 10)
                                sw4.Write("  " + i + "    ");

                            if ((i >= 10) & (i < 100))
                                sw4.Write("  " + i + "   ");

                            if (i >= 100)
                                sw4.Write("  " + i + "  ");
                        }
                    }*/
                    //если предыдущее число не содержало знаков после запятой
                    //добавляем пробелы
                    if ((flag) && (j > 0))
                        sw4.Write("    ");

                    if (Prob == 0)
                    {
                    
                        sw4.Write("    " + 0 + "    ");
                        flag = true;
                    }
                    else
                    {
                        //переводим вероятность к текстовый формат
                        S = Prob.ToString("0.###");
                        //находим индекс разделителя
                        found = S.IndexOf(",");

                        if (found == -1)
                        {
                          
                            sw4.Write("    " + S + "    ");
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            //выделяем подстроку после запятой
                            s = S.Substring(found + 1);
                            //считаем число знаков после запятой
                            k = s.Length;
                            sw4.Write(S + "    ");
                            //если их меньше 7, добавляем пробелы
                            for (int z = k; z < 7; z++)
                                sw4.Write(" ");
                        }
                    }
                }
            }
        /*
            if (BufferCount1 <= 3)
            {
                sw4.WriteLine();
                if (BufferCount1 == 0)
                {
                    sw4.WriteLine("И");
                    sw4.WriteLine("П");
                    sw4.WriteLine("В");
                    sw4.WriteLine("1");
                }
                if (BufferCount1 == 1)
                {
                    sw4.WriteLine("П");
                    sw4.WriteLine("В");
                    sw4.WriteLine("1");
                }
                if (BufferCount1 == 2)
                {
                    sw4.WriteLine("В");
                    sw4.WriteLine("1");
                }
                if (BufferCount1 == 3)
                {
                    sw4.WriteLine("1");
                }
            }
          */
            sw4.Close();

            StreamWriter sw5 = new StreamWriter("Стационарное распределение.txt");

            sw5.WriteLine(SystemProbability[0].ToString("0.###") + " "+SystemProbability[1].ToString("0.###"));
            sw5.WriteLine(SystemProbability[2].ToString("0.###") + " "+SystemProbability[3].ToString("0.###"));

            sw5.Close();

        }

        private void ModelResult<T, D1, D2>(SimulationModel<T, D1, D2> model)
            where T : Distribution
            where D1 : Distribution
            where D2 : Distribution
        {
            model.Run();

            SystemProbability = model.SystemProbability();

            BufferProbability1 = model.BufferProbability(1);

            BufferProbability2 = model.BufferProbability(2);

            BufferState = model.BufferState();

            BufferProbability = model.BufferProbability();

            BufferCount = model.BufferCount();
            BufferCount1 = model.BufferCount(1);
            BufferCount2 = model.BufferCount(2);

            double[] Mean = model.GetMean();
            double[,] CoVariance = model.GetCovariance();
            double TotalTime = Math.Round(model.TotalTime(), 3);           

            label4.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            label16.Visible = true;
            label51.Visible = true;
            label57.Visible = true;

            label4.Text = TotalTime.ToString();

            label13.Text = Convert.ToString(CoVariance[0, 0]);
            label14.Text = Convert.ToString(CoVariance[1, 0]);
            label15.Text = Convert.ToString(CoVariance[0, 1]);
            label16.Text = Convert.ToString(CoVariance[1, 1]);

            label51.Text = Convert.ToString(Mean[0]);
            label57.Text = Convert.ToString(Mean[1]);

            //dataGridView1.Rows.Add("ИПВ1", CoVariance[0, 0], CoVariance[0, 1]);
            //dataGridView1.Rows.Add("ИПВ2", CoVariance[1, 0], CoVariance[1, 1]);

           // dataGridView2.Rows.Add("ИПВ1",Mean[0]);
            //dataGridView2.Rows.Add("ИПВ2",Mean[1]);


        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.TextLength == 0) || (textBox2.TextLength == 0) || (textBox3.TextLength == 0) || (textBox4.TextLength == 0))
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox4.Text.IndexOf(",") != -1 || textBox4.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }
        /*
        private void button3_Click(object sender, EventArgs e)
        {

            GraphPane pane1 = zedGraphControl2.GraphPane;

            //если уже добавлен график, он удаляется
            if (pane1.CurveList.Count > 1)

                pane1.CurveList.RemoveAt(1);

            if (radioButton6.Checked == true)
            {
             //детерминированное распределение
                PointPairList list3 = new PointPairList();

                double[] prob = new double[2];

                prob[0] = Convert.ToDouble(numericUpDown1.Value);

                prob[1] = Convert.ToDouble(numericUpDown2.Value);

                for (int i = 0; i < 2; i++)
                    list3.Add(i, prob[i]);

                if ((prob[0] + prob[1]) == 1)
                {
                    LineItem myCurve3 = pane1.AddCurve("", list3, Color.Orchid, SymbolType.Star);
                }

                zedGraphControl2.Invalidate();
            }
        }
        */
        private void button2_Click(object sender, EventArgs e)
        {
            GraphPane pane1 = zedGraphControl3.GraphPane;

            //если уже добавлен график, он удаляется
            if (pane1.CurveList.Count > 1)

                pane1.CurveList.RemoveAt(1);

            if (radioButton5.Checked == true)
            {                          
                //распределение Пуассона
                PointPairList list4 = new PointPairList();

                double lyambda = Convert.ToDouble(textBox7.Text);

                PoissonDistribution Poisson = new PoissonDistribution(lyambda);

                for (int i = 0; i < BufferCount1; i++)

                    list4.Add(i,Poisson.Probability(i));

                    LineItem myCurve4 = pane1.AddCurve("", list4, Color.Coral, SymbolType.Star);

                zedGraphControl3.Invalidate();
            }
            if (radioButton7.Checked == true)
            {
                //нормальное распределение
                PointPairList list5 = new PointPairList();

                double mean = Convert.ToDouble(textBox10.Text);

                double sigma = Convert.ToDouble(textBox11.Text);

                NormalDistribution normal = new NormalDistribution(mean, sigma);

                for (double i = 0; i < BufferCount1; i+=0.01)

                    list5.Add(i, normal.FrequencyFunction(i));

                LineItem myCurve5 = pane1.AddCurve("", list5, Color.Magenta, SymbolType.None);

                zedGraphControl3.Invalidate();
            }

            if (radioButton8.Checked == true)
            {
                //геометрическое распределение
                PointPairList list6 = new PointPairList();

                double p = Convert.ToDouble(textBox12.Text);

                GeometricDistribution geom = new GeometricDistribution(p);

                for (int i = 0; i < BufferCount1; i ++)

                    list6.Add(i, geom.Probability(i));

                LineItem myCurve6 = pane1.AddCurve("", list6, Color.HotPink, SymbolType.Star);

                zedGraphControl3.Invalidate();
            }

            if (radioButton9.Checked == true)
            {
                //биномиальное распределение
                PointPairList list7 = new PointPairList();

                double p = Convert.ToDouble(textBox13.Text);

                BinomialDistribution bin = new BinomialDistribution(p, BufferCount1);

                for (int i = 0; i < BufferCount1; i++)

                    list7.Add(i, bin.Probability(i));

                LineItem myCurve7 = pane1.AddCurve("", list7, Color.RoyalBlue, SymbolType.Star);

                zedGraphControl3.Invalidate();
            }

        }


        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox7.Text.IndexOf(",") != -1 || textBox7.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox10.Text.IndexOf(",") != -1 || textBox10.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox11.Text.IndexOf(",") != -1 || textBox11.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox12.Text.IndexOf(",") != -1 || textBox12.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox13.Text.IndexOf(",") != -1 || textBox13.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox14.Text.IndexOf(",") != -1 || textBox14.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                label2.Visible = true;
                textBox2.Visible = true;
            }
            else
            {
                label2.Visible = false;
                textBox2.Visible = false;
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                label22.Visible = true;
                textBox14.Visible = true;

                label23.Visible = true;
                textBox15.Visible = true;
            }
            else
            {
                label22.Visible = false;
                textBox14.Visible = false;

                label23.Visible = false;
                textBox15.Visible = false;
            }
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked)
            {
                label24.Visible = true;
                textBox16.Visible = true;

                label25.Visible = true;
                textBox17.Visible = true;
            }
            else
            {
                label24.Visible = false;
                textBox16.Visible = false;

                label25.Visible = false;
                textBox17.Visible = false;
            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
            {
                label26.Visible = true;
                textBox18.Visible = true;

                label27.Visible = true;
                textBox19.Visible = true;
            }
            else
            {
                label26.Visible = false;
                textBox18.Visible = false;

                label27.Visible = false;
                textBox19.Visible = false;
            }
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton13.Checked)
            {
                label28.Visible = true;
                textBox20.Visible = true;
            }
            else
            {
                label28.Visible = false;
                textBox20.Visible = false;
            }
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox2.Text.IndexOf(",") != -1 || textBox2.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox14_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox14.Text.IndexOf(",") != -1 || textBox14.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox15.Text.IndexOf(",") != -1 || textBox15.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox16.Text.IndexOf(",") != -1 || textBox16.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox17.Text.IndexOf(",") != -1 || textBox17.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox18.Text.IndexOf(",") != -1 || textBox18.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox19.Text.IndexOf(",") != -1 || textBox19.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox20.Text.IndexOf(",") != -1 || textBox20.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                label17.Visible = true;
                textBox7.Visible = true;
            }
            else
            {
                label17.Visible = false;
                textBox7.Visible = false;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                label18.Visible = true;
                textBox10.Visible = true;

                label19.Visible = true;
                textBox11.Visible = true;
            }
            else
            {
                label18.Visible = false;
                textBox10.Visible = false;

                label19.Visible = false;
                textBox11.Visible = false;
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                label20.Visible = true;
                textBox12.Visible = true;
            }
            else
            {
                label20.Visible = false;
                textBox12.Visible = false;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                label21.Visible = true;
                textBox13.Visible = true;
            }
            else
            {
                label21.Visible = false;
                textBox13.Visible = false;
            }
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton14.Checked)
            {
                label29.Visible = true;
                textBox21.Visible = true;

                label31.Visible = true;
                textBox23.Visible = true;
            }
            else
            {
                label29.Visible = false;
                textBox21.Visible = false;

                label31.Visible = false;
                textBox23.Visible = false;
            }
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton15.Checked)
            {
                label30.Visible = true;
                textBox22.Visible = true;

                label32.Visible = true;
                textBox24.Visible = true;
            }
            else
            {
                label30.Visible = false;
                textBox22.Visible = false;

                label32.Visible = false;
                textBox24.Visible = false;
            }
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton16.Checked)
            {
                label33.Visible = true;
                textBox25.Visible = true;

                label34.Visible = true;
                textBox26.Visible = true;
            }
            else
            {
                label33.Visible = false;
                textBox25.Visible = false;

                label34.Visible = false;
                textBox26.Visible = false;
            }
        }

        private void radioButton17_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton17.Checked)
            {
                label35.Visible = true;
                textBox27.Visible = true;
            }
            else
            {
                label35.Visible = false;
                textBox27.Visible = false;
            }
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox21.Text.IndexOf(",") != -1 || textBox21.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox23.Text.IndexOf(",") != -1 || textBox23.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox22.Text.IndexOf(",") != -1 || textBox22.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox24.Text.IndexOf(",") != -1 || textBox24.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox25.Text.IndexOf(",") != -1 || textBox25.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox26_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox26.Text.IndexOf(",") != -1 || textBox26.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox27_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox27.Text.IndexOf(",") != -1 || textBox27.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }
        /*
        private void button5_Click(object sender, EventArgs e)
        {
            GraphPane pane1 = zedGraphControl2.GraphPane;

            //если уже добавлен график, он удаляется
            if (pane1.CurveList.Count > 1)

                pane1.CurveList.RemoveAt(1);

            // Обновим график
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
        }
*/
        private void button4_Click(object sender, EventArgs e)
        {
            GraphPane pane2 = zedGraphControl3.GraphPane;

            //если уже добавлен график, он удаляется
            if (pane2.CurveList.Count > 1)

                pane2.CurveList.RemoveAt(1);

            // Обновим график
            zedGraphControl3.AxisChange();
            zedGraphControl3.Invalidate();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (radioButton3.Checked)
            {
                groupBox8.Enabled = true;
            }
            else
            {
                groupBox8.Enabled = false;
            }
             */
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox28_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';

            if (e.KeyChar == ' ')

                return;

            if (e.KeyChar == ',')
            {
                return;
            }

            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;

            if (e.KeyChar == '.') e.KeyChar = ',';

            if (e.KeyChar == ' ')

                return;


            if (e.KeyChar == '-')
            {
                if (textBox29.Text.IndexOf('-') != -1)
                {
                    e.Handled = true;
                }
                return;
            }
            if (e.KeyChar == ',')
            {
                return;
            }

            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (radioButton2.Checked)
            {
                groupBox9.Enabled = true;
                textBox28.Visible = true;
                textBox29.Visible = true;
                //label36.Visible = true;
                //label37.Visible = true;
            }
            else
            {
                groupBox9.Enabled = false;
                textBox28.Visible = false;
                textBox29.Visible = false;
               // label36.Visible = false;
                //label37.Visible = false;
            }
             */
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label1.Visible = true;
                textBox1.Visible = true;
            }
            else
            {
                label1.Visible = false;
                textBox1.Visible = false;
            }
        }

        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton22.Checked)
            {
                label45.Visible = true;
                textBox37.Visible = true;
            }
            else
            {
                label45.Visible = false;
                textBox37.Visible = false;
            }
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton21.Checked)
            {
                label44.Visible = true;
                textBox36.Visible = true;

                label43.Visible = true;
                textBox35.Visible = true;
            }
            else
            {
                label44.Visible = false;
                textBox36.Visible = false;

                label43.Visible = false;
                textBox35.Visible = false;
            }
        }

        private void radioButton20_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton20.Checked)
            {
                label42.Visible = true;
                textBox34.Visible = true;

                label41.Visible = true;
                textBox33.Visible = true;
            }
            else
            {
                label42.Visible = false;
                textBox34.Visible = false;

                label41.Visible = false;
                textBox33.Visible = false;
            }
        }

        private void radioButton19_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton19.Checked)
            {
                label40.Visible = true;
                textBox32.Visible = true;

                label39.Visible = true;
                textBox31.Visible = true;
            }
            else
            {
                label40.Visible = false;
                textBox32.Visible = false;

                label39.Visible = false;
                textBox31.Visible = false;
            }
        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton18.Checked)
            {
                label38.Visible = true;
                textBox30.Visible = true;
            }
            else
            {
                label38.Visible = false;
                textBox30.Visible = false;
            }
        }

        private void textBox37_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox37.Text.IndexOf(",") != -1 || textBox37.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox36_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox36.Text.IndexOf(",") != -1 || textBox36.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox35_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox35.Text.IndexOf(",") != -1 || textBox35.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox34_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox34.Text.IndexOf(",") != -1 || textBox34.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox33.Text.IndexOf(",") != -1 || textBox33.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox32_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox32.Text.IndexOf(",") != -1 || textBox32.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox31.Text.IndexOf(",") != -1 || textBox31.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void textBox30_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if ((textBox30.Text.IndexOf(",") != -1 || textBox30.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            if (Char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            GraphPane pane1 = zedGraphControl4.GraphPane;

            //если уже добавлен график, он удаляется
            if (pane1.CurveList.Count > 1)

                pane1.CurveList.RemoveAt(1);

            if (radioButton26.Checked == true)
            {
                //распределение Пуассона
                PointPairList list8 = new PointPairList();

                double lyambda = Convert.ToDouble(textBox44.Text);

                PoissonDistribution Poisson = new PoissonDistribution(lyambda);

                for (int i = 0; i < BufferCount2; i++)

                    list8.Add(i, Poisson.Probability(i));

                LineItem myCurve8 = pane1.AddCurve("", list8, Color.Coral, SymbolType.Star);

                zedGraphControl4.Invalidate();
            }
            if (radioButton25.Checked == true)
            {
                //нормальное распределение
                PointPairList list9 = new PointPairList();

                double mean = Convert.ToDouble(textBox43.Text);

                double sigma = Convert.ToDouble(textBox42.Text);

                NormalDistribution normal = new NormalDistribution(mean, sigma);

                for (double i = 0; i < BufferCount2; i += 0.01)

                    list9.Add(i, normal.FrequencyFunction(i));

                LineItem myCurve9 = pane1.AddCurve("", list9, Color.Magenta, SymbolType.None);

                zedGraphControl4.Invalidate();
            }

            if (radioButton24.Checked == true)
            {
                //геометрическое распределение
                PointPairList list10 = new PointPairList();

                double p = Convert.ToDouble(textBox41.Text);

                GeometricDistribution geom = new GeometricDistribution(p);

                for (int i = 0; i < BufferCount2; i++)

                    list10.Add(i, geom.Probability(i));

                LineItem myCurve10 = pane1.AddCurve("", list10, Color.HotPink, SymbolType.Star);

                zedGraphControl4.Invalidate();
            }

            if (radioButton23.Checked == true)
            {
                //биномиальное распределение
                PointPairList list11 = new PointPairList();

                double p = Convert.ToDouble(textBox40.Text);

                BinomialDistribution bin = new BinomialDistribution(p, BufferCount1);

                for (int i = 0; i < BufferCount2; i++)

                    list11.Add(i, bin.Probability(i));

                LineItem myCurve11 = pane1.AddCurve("", list11, Color.RoyalBlue, SymbolType.Star);

                zedGraphControl4.Invalidate();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GraphPane pane2 = zedGraphControl4.GraphPane;

            //если уже добавлен график, он удаляется
            if (pane2.CurveList.Count > 1)

                pane2.CurveList.RemoveAt(1);

            // Обновим график
            zedGraphControl4.AxisChange();
            zedGraphControl4.Invalidate();
        }

        private void radioButton26_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton26.Checked)
            {
                label56.Visible = true;
                textBox44.Visible = true;
            }
            else
            {
                label56.Visible = false;
                textBox44.Visible = false;
            }
        }

        private void radioButton24_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton24.Checked)
            {
                label53.Visible = true;
                textBox41.Visible = true;
            }
            else
            {
                label53.Visible = false;
                textBox41.Visible = false;
            }
        }

        private void radioButton23_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton23.Checked)
            {
                label52.Visible = true;
                textBox40.Visible = true;
            }
            else
            {
                label52.Visible = false;
                textBox40.Visible = false;
            }
        }

        private void radioButton25_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton25.Checked)
            {
                label55.Visible = true;
                textBox43.Visible = true;

                label54.Visible = true;
                textBox42.Visible = true;
            }
            else
            {
                label55.Visible = false;
                textBox43.Visible = false;

                label54.Visible = false;
                textBox42.Visible = false;
            }
        }
    }
        
}
