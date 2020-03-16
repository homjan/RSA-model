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
         //   this.XX = xxx;
         //   this.sloj_T1 = sloj;
            
            this.zedgraph = zedd;
            pane = zedd.GraphPane;
            
            }
        public void clearGraph() {
            pane.CurveList.Clear();
        }

        public void makeGraph(double[] xxx, double[] sloj, Color color, String heading) {
            XX = xxx;
            sloj_T1 = sloj;

            PointPairList f1_list = new PointPairList();
            for (int i = 0; i < XX.Length; i++)
            {
                f1_list.Add(XX[i], sloj_T1[i]);
            }

      //  LineItem myCurve = pane.AddCurve(UseZedgraph.XX, sloj_T1, Color.Blue, SymbolType.None);
           LineItem myCurve = pane.AddCurve(heading, f1_list, color, SymbolType.None);
        //  LineItem myCurve = pane.AddCurve( );

             pane.Title.Text = " ";
    //        pane.XAxis.Title.Text = "t (нс)";
   //         pane.YAxis.Title.Text = "I, (Вт/см2)";


        }
        public void install_pane(String Xaxis, String Yaxis) {
            pane.Title.Text = " ";
            pane.XAxis.Title.Text = Xaxis;
            pane.YAxis.Title.Text = Yaxis;
        }

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
        public void clearDataINFile(String adres) {
            StreamWriter file = new StreamWriter(adres, false);       //ofstream
            file.Close();
        }

        public void resetGraph() {
            zedgraph.AxisChange();

            // Обновляем график
            zedgraph.Invalidate();
        }

        public void saveGraph() {
            zedgraph.SaveAsBitmap();
        }

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
