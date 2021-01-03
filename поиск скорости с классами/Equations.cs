using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace поиск_скорости_с_классами
{
    class Equations
    {
        double sigma1;
        double sigma2;
        double t21;
        double t32;

        public Equations(double sigma, double sig2, double t21, double t32  ) {
            this.sigma1 = sigma;
            this.sigma2 = sig2;
            this.t21 = t21;
            this.t32 = t32;

        }

        /// <summary>
        /// Первое ДУ населенности
        /// </summary>
        /// <param name="t1">Время</param>
        /// <param name="n1">населенность 1 уровня</param>
        /// <param name="n2">населенность 2 уровня</param>
        /// <param name="n3">населенность 3 уровня</param>
        /// <param name="n4">населенность 4 уровня</param>
        /// <param name="A0">Амплитуда</param>
        /// <returns></returns>
        public double Funkcia1(double t1, double n1, double n2, double n3, double n4, double A0)
        {
            double F;
            double I;
            //I = Vozd(t1, A0);
            I = A0;
            //   F = (-1)*n1*sigma1*I + n2/t21 + n4/t41; // 1 уравнение
            F = (-1) * n1 * sigma1 * I + n2 / t21; // 1 уравнение

            return F;
        }

        /// <summary>
        /// Второе ДУ населенности
        /// </summary>
        /// <param name="t1">Время</param>
        /// <param name="n1">населенность 1 уровня</param>
        /// <param name="n2">населенность 2 уровня</param>
        /// <param name="n3">населенность 3 уровня</param>
        /// <param name="n4">населенность 4 уровня</param>
        /// <param name="A0">Амплитуда</param>
        /// <returns></returns>
        public double Funkcia2(double t1, double n1, double n2, double n3, double n4, double A0)
        {
            double F;
            double I;
            //   I = Vozd(t1, A0);
            I = A0;
            // F = n1 * sigma1*I - n2*s*I - n2/t21 + n3/t32 - n2/t24; // 2 уравнение
            F = n1 * sigma1 * I - n2 * sigma2 * I - n2 / t21 + n3 / t32; // 2 уравнение

            return F;
        }

        /// <summary>
        /// Третье ДУ населенности
        /// </summary>
        /// <param name="t1">Время</param>
        /// <param name="n1">населенность 1 уровня</param>
        /// <param name="n2">населенность 2 уровня</param>
        /// <param name="n3">населенность 3 уровня</param>
        /// <param name="n4">населенность 4 уровня</param>
        /// <param name="A0">Амплитуда</param>
        /// <returns></returns>
        public double Funkcia3(double t1, double n1, double n2, double n3, double n4, double A0)
        {
            double F;
            double I;
            // I = Vozd(t1, A0);
            I = A0;
            F = n2 * sigma2 * I - n3 / t32; // 3 уравнение

            return F;
        } 

        /// <summary>
        /// Четвертое ДУ населенности
        /// </summary>
        /// <param name="t1">Время</param>
        /// <param name="n1">населенность 1 уровня</param>
        /// <param name="n2">населенность 2 уровня</param>
        /// <param name="n3">населенность 3 уровня</param>
        /// <param name="n4">населенность 4 уровня</param>
        /// <param name="A0">Амплитуда</param>
        /// <returns></returns>
        public double Funkcia4(double t1, double n1, double n2, double n3, double n4, double A0)
        {
            double F;
            double I;
            // I = Vozd(t1, A0);
            I = A0;
            // F = n2/t24-n4/t41; // 3 уравнение
            F = 0;
            return F;
        }

        /// <summary>
        /// ДУ Интенсивности
        /// </summary>
        /// <param name="t1">Время</param>
        /// <param name="n1">населенность 1 уровня</param>
        /// <param name="n2">населенность 2 уровня</param>
        /// <param name="n3">населенность 3 уровня</param>
        /// <param name="n4">населенность 4 уровня</param>
        /// <param name="A0">Амплитуда</param>
        /// <returns></returns>
        public double Funkcia5(double t1, double n1, double n2, double n3, double n4, double A0)
        {

            double I = 0;
            //sloj_T2[o] = (-1) * 0.03 * sloj_T1[o] * (sigma1 * n11 + s2 * n21) + sloj_T1[o - 1]; //

            I = (-1) * 0.03 * A0 * (sigma1 * n1 + sigma2 * n2);

            return I;
        } //Вспомогательная




    }
}
