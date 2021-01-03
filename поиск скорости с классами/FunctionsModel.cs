using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace поиск_скорости_с_классами
{
    class FunctionsModel
    {
        public FunctionsModel()
        {
        }
        /// <summary>
        /// Поиск максимума массива
        /// </summary>
        /// <param name="T_2">массив</param>
        /// <param name="b">длина массива</param>
        /// <returns></returns>
        public double max_y(double[] T_2, int b)
        {
            double m = 0;

            for (int i = 1; i < b - 2; i++)
            {
                if (T_2[i] > m)
                {
                    m = T_2[i + 1];
                }
            }
            return m;
        }

        /// <summary>
        /// Поиск положения максимума массива
        /// </summary>
        /// <param name="T_2">массив</param>
        /// <param name="b">длина массива</param>
        /// <returns></returns>
        public int max_x(double[] T_2, int b)
        {
            int d = 0;

            for (int i = 1; i < b - 2; i++)
            {
                if (T_2[i + 1] > T_2[i])
                {
                    d = i + 1;
                }
            }
            return d;
        }

        /// <summary>
        /// Поиск положения левой полувысоты
        /// </summary>
        /// <param name="T_2">массив</param>
        /// <param name="b">длина массива</param>
        /// <param name="y">Полувысота</param>
        /// <returns></returns>
        public int max05_x1(double[] T_2, int b, double y)
        {
            int x_1 = 0;
            for (int i = 1; i < b - 2; i++)
            {
                if (T_2[i + 1] > y)
                {
                    if (T_2[i - 1] < y)
                    {
                        x_1 = i;
                    }
                }
            }
            return x_1;
        }

        /// <summary>
        /// Поиск положения правой полувысоты
        /// </summary>
        /// <param name="T_2">массив</param>
        /// <param name="b">длина массива</param>
        /// <param name="y">Полувысота</param>
        /// <returns></returns>
        public int max05_x2(double[] T_2, int b, double y)
        {
            int x_1 = 0;

            for (int i = 1; i < b - 2; i++)
            {
                if (T_2[i + 1] < y)
                {
                    if (T_2[i - 1] > y)
                    {
                        x_1 = i;
                    }
                }
            }
            return x_1;
        }

        /// <summary>
        /// Поиск обезразмерянной интенсивности
        /// </summary>
        /// <param name="sloj_T2">массив</param>
        /// <param name="Z">длина массива</param>
        /// <param name="maximum3">Обезразмеривание</param>
        /// <returns></returns>
        public double[] Intensity_00(double[] sloj_T2, int Z, double maximum3)
        {

            double[] sloj_T1_out = new double[Z];
            for (int i = 0; i < Z; i++)// Перенос значений из второго слоя в первый
            {
                sloj_T1_out[i] = sloj_T2[i] / maximum3;

            }
            return sloj_T1_out;
        }

        /// <summary>
        /// Временной сдвиг
        /// </summary>
        /// <param name="t_max1">Положение максимума падающего импульса</param>
        /// <param name="t_max2">Положение максимума прошедшего импульса</param>
        /// <returns></returns>
        public double time_shift(double t_max1, double t_max2)
        {
            double time2 = t_max2 - t_max1;
            time2 = time2 / 100;
            return time2;
        }
        /// <summary>
        /// Групповая скорость
        /// </summary>
        /// <param name="time_shift">временной сдвиг</param>
        /// <returns></returns>
        public double groupVelosity(double time_shift)
        {
            double L_ = 0.001;
            double c = 300000000;
            double v_g = (L_ / ((L_ / c) - ((-1) * time_shift * 100) / 1E11));
            return v_g / c;
        }

        /// <summary>
        /// Ширина импульса
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        public int widthPulse(int x1, int x2)
        {

            return (x2 - x1);
        }

        /// <summary>
        /// Мгновенная групповая скорость
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public double groupVelosityMomentum(double time1, double time2)
        {
            double time_shift = time2 - time1;
            double L_ = 0.001;
            double c = 300000000;
            double v_g = (L_ / ((L_ / c) - ((-1) * time_shift * 100) / 1E11));
            return v_g / c;
        }

        /// <summary>
        /// Мгновенный временной сдвиг
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public double timeShiftMomentum(double time1, double time2)
        {
            double time_shift = time2 - time1;

            return time_shift;

        }
    }
}
