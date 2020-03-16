using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;


namespace поиск_скорости_с_классами
{
     partial class Form1 : Form
    {
        public UseZedgraph usergraph;
       public UseZedgraph usergraph2;

        double sigma1;
        double s2s1;
        double t21;
        double n001;
        double I_000;
        double q_;
        double t00;
        double w_000;
        double ddd;
        double t32 = 1;

        const int Z = 20001;
        const double dt_000 = 1.00E-11; //шаг по времени                                        
        const double h_000 = 1.06E-34; // постоянная планка

        int scet_line = 0;


        public Form1()
        {
            InitializeComponent();
        }

        public void Inizialization() {
            try
            {
                sigma1 = System.Convert.ToDouble(textBox1.Text.Replace('.', ',')) * 1E-18;
                s2s1 = System.Convert.ToDouble(textBox2.Text.Replace('.', ','));
                t21 = System.Convert.ToDouble(this.textBox3.Text.Replace('.', ',')) * 100;
               n001 = System.Convert.ToDouble(this.textBox4.Text.Replace('.', ','))*1E17;  //коэффициент пропускания
                I_000 = System.Convert.ToDouble(this.textBox5.Text.Replace('.', ','));  //интенсивность
                q_ = System.Convert.ToDouble(this.textBox6.Text.Replace('.', ',')) * 100;///////// ширина
                t00 = System.Convert.ToDouble(this.textBox7.Text.Replace('.', ',')) * 100;//смещение
               
                w_000 = System.Convert.ToDouble(this.textBox8.Text.Replace('.', ','));// частота

                ddd = System.Convert.ToDouble(this.textBox9.Text.Replace('.', ','));// частота

            }
            catch
            {
                sigma1 = System.Convert.ToDouble(textBox1.Text.Replace(',', '.')) * 1E-18;
                s2s1 = System.Convert.ToDouble(textBox2.Text.Replace(',', '.'));
                t21 = System.Convert.ToDouble(this.textBox3.Text.Replace(',', '.')) * 100;
                t00 = System.Convert.ToDouble(this.textBox7.Text.Replace(',', '.')) * 100;//смещение
                q_ = System.Convert.ToDouble(this.textBox6.Text.Replace(',', '.')) * 100;///////// ширина
                n001 = System.Convert.ToDouble(this.textBox4.Text.Replace(',', '.'))*1E17;  //коэффициент пропускания
                I_000 = System.Convert.ToDouble(this.textBox5.Text.Replace(',', '.'));  //интенсивность
                w_000 = System.Convert.ToDouble(this.textBox8.Text.Replace(',', '.'));// частота

                ddd = System.Convert.ToDouble(this.textBox9.Text.Replace(',', '.'));// частота

            }
        }

        double fGausT(double t, double A0, double t00, double q_)       ////////////
        {
            double F = 0;

            if (radioButton1.Checked == true)
            {

                F = A0 * Math.Exp((-2.718) * ((t - t00) * (t - t00)) / (q_ * q_));
            }
            if (radioButton2.Checked == true)
            {

                if (t < t00)
                {
                    F = 0;
                }
                if (t >= t00)
                {

                    F = 6.67 * (A0 / (q_)) * (t - t00) * Math.Exp(-2.44 * (t - t00) / q_);// + 0.00000000000000000000000000000000000000000000000001; // треугольник (0,5 - более узкий 40 нс)

                }
            }
            // F = A0 * Math.Exp(-2 * q_ * q_ * ((t - t00) * (t - t00)));
            return F;
        }

        private void button1_Click(object sender, EventArgs e)//считать
        {
            FunctionsModel functions = new FunctionsModel();
            usergraph = new UseZedgraph(zedGraph1);
            usergraph.clearAll();
            usergraph.clearDataINFile("Интенсивность.txt");
            usergraph.clearDataINFile("Интенсивность обезразмерянная.txt");

            usergraph2 = new UseZedgraph(zedGraph2);

            Inizialization();

            double x_000 = dt_000 / (h_000 * w_000);// обезразмерка
            double A0 = I_000 * x_000;//5 8  13	  

            int nSloiDx = System.Convert.ToInt32(ddd / 0.3) + 1;
            textBox10.Text = System.Convert.ToString(nSloiDx);
            double n11 = n001 * nSloiDx;//6E17
         
            double[][] NNN = new double[4][];
            NNN[0] = new double[nSloiDx];
            NNN[1] = new double[nSloiDx];
            NNN[2] = new double[nSloiDx];
            NNN[3] = new double[nSloiDx];

            for (int i = 0; i < nSloiDx; i++) {
                NNN[0][i] = n11;
                NNN[1][i] = 0;
                NNN[2][i] = 0;
                NNN[3][i] = 0;
            }
            //    double[] XX = new double[201];
            //   double[] YY = new double[201];
            progressBar1.Minimum = 1;
            progressBar1.Maximum = nSloiDx;
            progressBar1.Value = 1;

            int number_chosen_layer1 = System.Convert.ToInt32(textBox11.Text);
            int number_chosen_layer2 = System.Convert.ToInt32(textBox12.Text); 
            int number_chosen_layer3 = System.Convert.ToInt32(textBox13.Text); 

            double[] sloj_T1 = new double[Z];
            double[] sloj_T2 = new double[Z];

            double[] AXIS_x_data = new double[Z];
            double scet_AXIS_X = 0;

            for (int g = 0; g < Z; g++)// начальные условия
            {
                sloj_T1[g] = fGausT(g, A0, t00, q_);
                AXIS_x_data[g] = scet_AXIS_X;
                scet_AXIS_X = scet_AXIS_X + 0.01;
            }
            double maximum, maximum2, maximum3;
            double t_shift_1 = functions.max_x(sloj_T1,Z), t_shift_2 = functions.max_x(sloj_T1, Z);
            double V_g1=0, V_g2=0;
            double[] V_G = new double[nSloiDx];
            maximum = functions.max_y(sloj_T1, Z);

            if (radioButton3.Checked)
            {
                makeGraphInLayer(sloj_T1, x_000, Color.Black, 0, usergraph);
            }if (radioButton4.Checked) {
                makeGraphInLayer(sloj_T1, maximum, Color.Black, 0, usergraph);
            }
         
            Model model = new Model(sloj_T1, sigma1, sigma1 * s2s1, t21, t32, NNN);

            for (int i = 0; i < nSloiDx; i++) {
                
                progressBar1.Value = i+1;

                //     model.shiftLayer(model.Runge_Kutta_all(i));

                model.shiftLayer(model.Intensity_transfer_1(i));

                //Сбрасываем в файл
                usergraph.putDataInFile(AXIS_x_data, model.getSloi1(), x_000, "Интенсивность.txt");
                maximum3 = functions.max_y(model.getSloi1(), Z);
                usergraph.putDataInFile(AXIS_x_data, model.getSloi1(), maximum3, "Интенсивность обезразмерянная.txt");


                if (radioButton3.Checked)
                {
                    if (i == number_chosen_layer1)
                    {
                        makeGraphInLayer(model.getSloi1(), x_000, Color.Red, number_chosen_layer1, usergraph);
                        usergraph.install_pane("t (нс)", "I, (Вт/см2)");
                    }

                    if (i == number_chosen_layer2)
                    {
                        makeGraphInLayer(model.getSloi1(), x_000, Color.Blue, number_chosen_layer2, usergraph);
                        usergraph.install_pane("t (нс)", "I, (Вт/см2)");
                    }

                    if (i == number_chosen_layer3)
                    {
                        makeGraphInLayer(model.getSloi1(), x_000, Color.Green, number_chosen_layer3, usergraph);
                        usergraph.install_pane("t (нс)", "I, (Вт/см2)");
                    }
                }
                if (radioButton4.Checked)
                {
                    if (i == number_chosen_layer1)
                    {
                        maximum2 = functions.max_y(model.getSloi1(), Z);
                        makeGraphInLayer(model.getSloi1(), maximum2, Color.Red, number_chosen_layer1, usergraph);
                        usergraph.install_pane("t (нс)", "I/I0");
                    }

                    if (i == number_chosen_layer2)
                    {
                        maximum2 = functions.max_y(model.getSloi1(), Z);
                        makeGraphInLayer(model.getSloi1(), maximum2, Color.Blue, number_chosen_layer2, usergraph);
                        usergraph.install_pane("t (нс)", "I/I0");
                    }

                    if (i == number_chosen_layer3)
                    {
                        maximum2 = functions.max_y(model.getSloi1(), Z);
                        makeGraphInLayer(model.getSloi1(), maximum2, Color.Green, number_chosen_layer3, usergraph);
                        usergraph.install_pane("t (нс)", "I/I0");
                    }
                }

                ////////////////////////
               // t_shift_1 = functions.max_x(model.getSloi1(),Z);

                V_g1 = V_g2;
                V_g2 = functions.groupVelosity(functions.max_x(model.getSloi1(), Z));
                  // V_G[i] = functions.groupVelosityMomentum(V_g1,V_g2);
               

                if (radioButton5.Checked)//Время смещения
                {
                V_G[i] = functions.max_x(model.getSloi1(), Z);
                    usergraph2.install_pane("dx", "t max, пс");

                }
                if (radioButton6.Checked)//Групповая скорость
                {
                    V_G[i] = functions.groupVelosity(functions.max_x(model.getSloi1(), Z));
                    usergraph2.install_pane("dx", "v/c");
                }
                if (radioButton7.Checked)//Мгновенная скорость
                {
                    V_g1 = V_g2;
                    V_g2 = functions.max_x(model.getSloi1(), Z);
                    V_G[i] = functions.groupVelosityMomentum(V_g1, V_g2);
                    usergraph2.install_pane("dx", "v/c");
                }
                if (radioButton8.Checked)//Коэффициент пропускания
                {
                    V_G[i] = functions.max_y(model.getSloi1(), Z)/maximum;
                    usergraph2.install_pane("dx", "T");
                }
                if (radioButton9.Checked)//Время смещения мгновенное
                {
                    t_shift_1 = t_shift_2;
                    t_shift_2 = functions.max_x(model.getSloi1(), Z);
                    V_G[i] = functions.timeShiftMomentum(t_shift_1, t_shift_2);
                    usergraph2.install_pane("dx", "время смещения");
                }

            }
            ///////////////////////////////////////////////////////////////

            if (scet_line == 3)
            {
                makeGraphInLayer2(V_G, nSloiDx, Color.Green, "Групповая скорость", usergraph2);
                scet_line++;
            }
            if (scet_line == 2)
            {
                makeGraphInLayer2(V_G, nSloiDx, Color.Blue, "Групповая скорость", usergraph2);
                scet_line++;
            }
            if (scet_line == 1)
            {
                makeGraphInLayer2(V_G, nSloiDx, Color.Red, "Групповая скорость", usergraph2);
                scet_line++;
            }
            if (scet_line == 0)
            {
                makeGraphInLayer2(V_G, nSloiDx, Color.Black, "Групповая скорость", usergraph2);
                scet_line++;
            }
           
            
            

        }

        public void makeGraphInLayer(double[] sloj, double X_delitel, Color color, int number_layer,UseZedgraph usergraph) {
            double[] sloj_T3 = sloj;
            int scetchik = 0;
            double[] XX = new double[201];
            double[] YY = new double[201];
            for (int j = 0; j < Z; j++)
            {
                if (j % 100 == 0)
                {
                    XX[scetchik] = System.Convert.ToDouble(j);
                    YY[scetchik] = (System.Convert.ToDouble(sloj_T3[j]/ X_delitel));
                    scetchik++;
                }
            }

            usergraph.makeGraph(XX, YY, color, "интенсивность на "+System.Convert.ToString(number_layer) +" слое");
            usergraph.resetGraph();

        }

        public void makeGraphInLayer2(double[] sloj, int N_elements, Color color, String name_graph, UseZedgraph usergraph2)
        {
            double[] sloj_T3 = sloj;
          
            double[] XX = new double[N_elements];
            double[] YY = new double[N_elements];
            for (int j = 0; j < N_elements; j++)
            {
               
                    XX[j] = System.Convert.ToDouble(j);
                    YY[j] = (System.Convert.ToDouble(sloj_T3[j]));
                                   
            }

            usergraph2.makeGraph(XX, YY, color, "");
            usergraph2.resetGraph();

        }

        private void button2_Click(object sender, EventArgs e)//Сохранить 1
        {
            usergraph.saveGraph();
            
        }

        private void button3_Click(object sender, EventArgs e)//Очистить
        {
            usergraph.clearAll();
            usergraph2.clearAll();
            scet_line = 0;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)//Сохранить 2
        {
            usergraph2.saveGraph();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
