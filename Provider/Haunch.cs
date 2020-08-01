using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider
{
    public class Haunch
    {
        public static List<double> Dw(double [] Aspan, DataTable DThaunch, List<double> Lx)
        {
            int numinsup = Aspan.GetLength(0) - 1;
            int nhaunch = numinsup * 3 + numinsup - 1 + 2;
            double[] Ahaunch = new double[nhaunch];

            //Calculate Ahaunch
            Ahaunch[0] = Aspan[0] - Convert.ToDouble(DThaunch.Rows[0][1]) / 2 - Convert.ToDouble(DThaunch.Rows[0][0]);            
            Ahaunch[nhaunch - 1] = Aspan[Aspan.GetLength(0) - 1] - Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][1]) / 2 - Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][2]);            

            for (int i = 0; i < numinsup; i++)
            {
                Ahaunch[1 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][0]);                
                Ahaunch[2 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][1]);                
                Ahaunch[3 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][2]); 
            }

            if (numinsup > 1)
                for (int i = 0; i < numinsup - 1; i++)
                {
                    Ahaunch[4 + i * 4] = Aspan[1 + i] - Convert.ToDouble(DThaunch.Rows[i][1]) / 2 - Convert.ToDouble(DThaunch.Rows[i][2]) - Convert.ToDouble(DThaunch.Rows[i + 1][1]) / 2 - Convert.ToDouble(DThaunch.Rows[i + 1][0]);                   
                }

            double[] b = new double[Ahaunch.GetLength(0) + 1];

            b[0] = 0;
            for (int i = 1; i < Ahaunch.GetLength(0) + 1; i++)
            {
                b[i] = 0;
                for (int j = 0; j < i; j++)
                    b[i] = Ahaunch[j] + b[i];
            }

            //List of cumulate
            List<double> S = new List<double>(b);


            List<int> index = new List<int>(); 
            foreach (double x1 in Lx)
            {
                if (x1 == S.Max())
                    index.Add(S.Count - 2);
                else
                index.Add(S.FindLastIndex(p => p <= x1));
            }
                

            List<double> Dw = new List<double>(new double [index.Count]);
            for (int i = 0; i < index.Count; i++)
            {
                if (index[i] == 0)
                    Dw[i] = Convert.ToDouble(DThaunch.Rows[0][3]);
                else if (index[i] == nhaunch - 1)
                    Dw[i] = Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][5]);
                else if ((index[i] - 1) % 4 == 0)
                {
                    double A = (Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][4]) - Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][3])) / Math.Pow(Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][0]),2);
                    Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][3]) + A * Math.Pow(Lx[i] - S[index[i]],2) ;
                }                    
                else if ((index[i] - 2) % 4 == 0)
                    Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 2) / 4][4]);
                else if ((index[i] - 3) % 4 == 0)
                {
                    double A = (Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][4]) - Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][5])) / Math.Pow(Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][2]),2);
                    Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][5]) + A * Math.Pow(-Lx[i] + S[index[i]+1], 2);
                }
                else if ((index[i] - 4) % 4 == 0)
                    Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 4) / 4][5]);
            }
           
            return Dw;
        }

        public static double Depth(double H1, double H2, double L1, double x)
        {
            double A = (H2 - H1) / L1 / L1;
            return H1 + A * x * x;
        }
    }
}
