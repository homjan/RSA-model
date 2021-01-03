using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace поиск_скорости_с_классами
{
    class UseZedgraph
    {
        private double[] XX;
        private double[] sloj_T1;
        private  GraphPane pane;
        ZedGraphControl zedgraph;
      

        public UseZedgraph(ZedGraphControl zedd){
        
            this.zedgraph = zedd;
            pane = zedd.GraphPane;
            
            }
        /// <summary>
        /// Очистить панель
        /// </summary>
        public void clearGraph() {
            pane.CurveList.Clear();
        }

        /// <summary>
        /// Построить график
        /// </summary>
        /// <param name="xxx">Массив координат х</param>
        /// <param name="sloj">Массив координат y</param>
        /// <param name="color">Цвет графика</param>
        /// <param name="heading">Заглавие</param>
        public void makeGraph(double[] xxx, double[] sloj, Color color, String heading) {
            XX = xxx;
            sloj_T1 = sloj;

            PointPairList f1_list = new PointPairList();
            for (int i = 0; i < XX.Length; i++)
            {
                f1_list.Add(XX[i], sloj_T1[i]);
            }

         LineItem myCurve = pane.AddCurve(heading, f1_list, color, SymbolType.None);       
             pane.Title.Text = " ";
   
        }

        /// <summary>
        /// Установить оси
        /// </summary>
        /// <param name="Xaxis">Ось Х</param>
        /// <param name="Yaxis">Ось Y</param>
        public void install_pane(String Xaxis, String Yaxis) {
            pane.Title.Text = " ";
            pane.XAxis.Title.Text = Xaxis;
            pane.YAxis.Title.Text = Yaxis;
        }

        /// <summary>
        /// Записать обезразмерянные данные в файл
        /// </summary>
        /// <param name="xxx">Массив координат х</param>
        /// <param name="sloj">Массив координат y</param>
        /// <param name="x_0">Обезразмеривание</param>
        /// <param name="adres">Имя файла</param>
        public void putDataInFile(double[] xxx, double[] sloj, double x_0, String adres) {
            StreamWriter file = new StreamWriter(adres,true);       //ofstream
          
            for (int i = 0; i < xxx.Length; i++) {
                if (i % 100 == 0)
                {
                    file.Write(Math.Round(sloj[i] / x_0, 6) + "\t");
                    if (i == xxx.Length - 1)
                    {
                        file.Write(Math.Round(sloj[i] / x_0, 6));
                    }
                }
            }
            file.WriteLine();
            file.Close();
        }
        /// <summary>
        /// Очистить файл
        /// </summary>
        /// <param name="adres"></param>
        public void clearDataINFile(String adres) {
            StreamWriter file = new StreamWriter(adres, false);       //ofstream
            file.Close();
        }

        /// <summary>
        /// Обновить график
        /// </summary>
        public void resetGraph() {

            zedgraph.AxisChange();           
            zedgraph.Invalidate();
        }

        /// <summary>
        /// Сохранить график
        /// </summary>
        public void saveGraph() {
            zedgraph.SaveAsBitmap();
        }

        /// <summary>
        /// Очистить график
        /// </summary>
        public void clearAll() {

            pane.CurveList.Clear();

            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.MaxAuto = true;

            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;

            zedgraph.AxisChange();
            zedgraph.Invalidate();

        }




    }
}
