using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace поиск_скорости_с_классами
{
    class Model// : Equations
    {
        private double[] sloj_T1;
        private double[] sloj_T3;

        private int Z;
        double sigma1;
        double sigma2;
        double t21;
        double t32;

        private double n1;
       

        private double[][] N;//1 ячейка - уровень, 2 - слой по толщине

      //  private int L;

         Equations equation;

        public Model(double[] sloj1, double sigma11, double sigma12, double t211, double t322, double[][] NNN) {
            this.sloj_T1 = sloj1;
            this.sloj_T3 = sloj1;
            Z = sloj1.Length;
            this.sigma1 = sigma11;
            this.sigma2 = sigma12;
            this.t21 = t211;
            this.t32 = t322;

            equation = new Equations(sigma1, sigma2, t21, t32);
            this.N = NNN;
          //  this.L = LL;


        }

        public double[] getSloi1() {
            return sloj_T1;
        }

        public double[] Runge_Kutta_all(int x) {
            double[] sloj_T2 = new double[Z];

            double k11 = 0, k12 = 0, k13 = 0, k14 = 0;
            double k21 = 0, k22 = 0, k23 = 0, k24 = 0;
            double k31 = 0, k32 = 0, k33 = 0, k34 = 0;
            double k41 = 0, k42 = 0, k43 = 0, k44 = 0;
            double k51 = 0, k52 = 0, k53 = 0, k54 = 0;

            double h = 1; // шаг

            double t = 0;
            // счетчики
            int k = 0; // сброс в файл каждого К элемента
            int n = 10; //счетчик каждого 10 элемента массива
            int o = 1;


            sloj_T2[0] = 0.000001;                                                             // граничные условия
            sloj_T2[Z - 1] = 0.000001;
            o = 1;

            for (int T = 1; T < Z; T++) // Цикл по времени
            {
                k11 = h * equation.Funkcia1(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k21 = h * equation.Funkcia2(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k31 = h * equation.Funkcia3(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k41 = h * equation.Funkcia4(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k51 = h * equation.Funkcia5(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);

                k12 = h * equation.Funkcia1(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o] + k51 / 2);
                k22 = h * equation.Funkcia2(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o] + k51 / 2);
                k32 = h * equation.Funkcia3(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o] + k51 / 2);
                k42 = h * equation.Funkcia4(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o] + k51 / 2);
                k52 = h * equation.Funkcia5(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o] + k51 / 2);


                k13 = h * equation.Funkcia1(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o] + k52 / 2);
                k23 = h * equation.Funkcia2(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o] + k52 / 2);
                k33 = h * equation.Funkcia3(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o] + k52 / 2);
                k43 = h * equation.Funkcia4(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o] + k52 / 2);
                k53 = h * equation.Funkcia5(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o] + k52 / 2);


                k14 = h * equation.Funkcia1(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o] + k53);
                k24 = h * equation.Funkcia2(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o] + k53);
                k34 = h * equation.Funkcia3(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o] + k53);
                k44 = h * equation.Funkcia4(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o] + k53);
                k54 = h * equation.Funkcia5(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o] + k53);

                N[0][x] = N[0][x] + (k11 + 2 * k12 + 2 * k13 + k14) / 6;
                N[1][x] = N[1][x] + (k21 + 2 * k22 + 2 * k23 + k24) / 6;
                N[2][x] = N[2][x] + (k31 + 2 * k32 + 2 * k33 + k34) / 6;
                N[3][x] = N[3][x] + (k41 + 2 * k42 + 2 * k43 + k44) / 6;

                sloj_T2[o] = sloj_T1[o] + (k51 + 2 * k52 + 2 * k53 + k54) / 6;

                k11 = 0; k12 = 0; k13 = 0; k14 = 0;
                k21 = 0; k22 = 0; k23 = 0; k24 = 0;
                k31 = 0; k32 = 0; k33 = 0; k34 = 0;
                k41 = 0; k42 = 0; k43 = 0; k44 = 0;
                k51 = 0; k52 = 0; k53 = 0; k54 = 0;

                k++;
                t = t + h;

                o++;
                n++;

            }
            ///////////////////////////////////////
            return sloj_T2;
        }

        public double[] Intensity_transfer_1(int x)
        {
            double[] sloj_T2 = new double[Z];

            double k11 = 0, k12 = 0, k13 = 0, k14 = 0;
            double k21 = 0, k22 = 0, k23 = 0, k24 = 0;
            double k31 = 0, k32 = 0, k33 = 0, k34 = 0;
            double k41 = 0, k42 = 0, k43 = 0, k44 = 0;
          
            double h = 1; // шаг

            double t = 0;
            // счетчики
            int k = 0; // сброс в файл каждого К элемента
            int n = 10; //счетчик каждого 10 элемента массива
            int o = 1;


            sloj_T2[0] = 0.000001;                                                             // граничные условия
            sloj_T2[Z - 1] = 0.000001;
            o = 1;

            for (int T = 1; T < Z; T++) // Цикл по времени
            {
                k11 = h * equation.Funkcia1(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k21 = h * equation.Funkcia2(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k31 = h * equation.Funkcia3(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k41 = h * equation.Funkcia4(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
               
                k12 = h * equation.Funkcia1(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o] );
                k22 = h * equation.Funkcia2(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o]);
                k32 = h * equation.Funkcia3(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o]);
                k42 = h * equation.Funkcia4(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o]);
               

                k13 = h * equation.Funkcia1(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o]);
                k23 = h * equation.Funkcia2(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o]);
                k33 = h * equation.Funkcia3(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o]);
                k43 = h * equation.Funkcia4(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o]);
               

                k14 = h * equation.Funkcia1(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o]);
                k24 = h * equation.Funkcia2(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o]);
                k34 = h * equation.Funkcia3(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o]);
                k44 = h * equation.Funkcia4(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o]);
                
                N[0][x] = N[0][x] + (k11 + 2 * k12 + 2 * k13 + k14) / 6;
                N[1][x] = N[1][x] + (k21 + 2 * k22 + 2 * k23 + k24) / 6;
                N[2][x] = N[2][x] + (k31 + 2 * k32 + 2 * k33 + k34) / 6;
                N[3][x] = N[3][x] + (k41 + 2 * k42 + 2 * k43 + k44) / 6;

                sloj_T2[o] = (-1) * 0.03 * sloj_T1[o] * (sigma1 * N[0][x] + sigma2 * N[1][x]) + sloj_T1[o - 1]; //

                k11 = 0; k12 = 0; k13 = 0; k14 = 0;
                k21 = 0; k22 = 0; k23 = 0; k24 = 0;
                k31 = 0; k32 = 0; k33 = 0; k34 = 0;
                k41 = 0; k42 = 0; k43 = 0; k44 = 0;
             

                k++;
                t = t + h;

                o++;
                n++;

            }
            ///////////////////////////////////////
            return sloj_T2;
        }

        public double[] Intensity_transfer_2(int x)
        {
            double[] sloj_T2 = new double[Z];
            

            double k11 = 0, k12 = 0, k13 = 0, k14 = 0;
            double k21 = 0, k22 = 0, k23 = 0, k24 = 0;
            double k31 = 0, k32 = 0, k33 = 0, k34 = 0;
            double k41 = 0, k42 = 0, k43 = 0, k44 = 0;

            double h = 1; // шаг

            double t = 0;
            // счетчики
            int k = 0; // сброс в файл каждого К элемента
            int n = 10; //счетчик каждого 10 элемента массива
            int o = 1;


            sloj_T2[0] = 0.000001;                                                             // граничные условия
            sloj_T2[Z - 1] = 0.000001;
            o = 1;

            for (int T = 1; T < Z-1; T++) // Цикл по времени
            {
                if (x == 0)
                {
                     sloj_T3[o] = sloj_T1[o];
                }

                k11 = h * equation.Funkcia1(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k21 = h * equation.Funkcia2(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k31 = h * equation.Funkcia3(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);
                k41 = h * equation.Funkcia4(t, N[0][x], N[1][x], N[2][x], N[3][x], sloj_T1[o]);

                k12 = h * equation.Funkcia1(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o]);
                k22 = h * equation.Funkcia2(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o]);
                k32 = h * equation.Funkcia3(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o]);
                k42 = h * equation.Funkcia4(t + h / 2, N[0][x] + k11 / 2, N[1][x] + k21 / 2, N[2][x] + k31 / 2, N[3][x] + k41 / 2, sloj_T1[o]);


                k13 = h * equation.Funkcia1(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o]);
                k23 = h * equation.Funkcia2(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o]);
                k33 = h * equation.Funkcia3(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o]);
                k43 = h * equation.Funkcia4(t + h / 2, N[0][x] + k12 / 2, N[1][x] + k22 / 2, N[2][x] + k32 / 2, N[3][x] + k42 / 2, sloj_T1[o]);


                k14 = h * equation.Funkcia1(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o]);
                k24 = h * equation.Funkcia2(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o]);
                k34 = h * equation.Funkcia3(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o]);
                k44 = h * equation.Funkcia4(t + h, N[0][x] + k13, N[1][x] + k23, N[2][x] + k33, N[3][x] + k43, sloj_T1[o]);

                N[0][x] = N[0][x] + (k11 + 2 * k12 + 2 * k13 + k14) / 6;
                N[1][x] = N[1][x] + (k21 + 2 * k22 + 2 * k23 + k24) / 6;
                N[2][x] = N[2][x] + (k31 + 2 * k32 + 2 * k33 + k34) / 6;
                N[3][x] = N[3][x] + (k41 + 2 * k42 + 2 * k43 + k44) / 6;

                if (x == 0)
                {
                    sloj_T2[o] = (-1) * 0.03 * sloj_T1[o] * (sigma1 * N[0][x] + sigma2 * N[0][x]) + sloj_T1[o]; //
                    sloj_T3[o] = sloj_T2[o];
                }
                else {
                    sloj_T2[o] = (-1) * 0.03 * sloj_T1[o] * (sigma1 * N[0][x] + sigma2 * N[0][x]) + sloj_T3[o]- sloj_T1[o+1]/2+ sloj_T1[o-1]/2;
                    sloj_T3[o] = sloj_T1[o];
                }
                k11 = 0; k12 = 0; k13 = 0; k14 = 0;
                k21 = 0; k22 = 0; k23 = 0; k24 = 0;
                k31 = 0; k32 = 0; k33 = 0; k34 = 0;
                k41 = 0; k42 = 0; k43 = 0; k44 = 0;


                k++;
                t = t + h;

                o++;
                n++;

            }
            ///////////////////////////////////////
            return sloj_T2;
        }

        public void shiftLayer(double[] layer2 ) {

            for (int i = 0; i < Z; i++)// Перенос значений из второго слоя в первый
            {
              
                sloj_T1[i]=layer2[i];
              
            }
        }
    }
}
