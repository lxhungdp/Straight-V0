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
        private double[] Aspan;
        private DataTable DThaunch;
        public Haunch(double[] Aspan, DataTable DThaunch)
        {
            this.Aspan = Aspan;
            this.DThaunch = DThaunch;
        }
        
        public List<double> Point
        {
            get 
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

                return S;
            }
        }
        
        public List<double> Sta
        { get; set; }
        
        public List<double> Dw
        {
            get
            {
                List<int> index = new List<int>();
                foreach (double x1 in Sta)
                {
                    if (x1 == Point.Max())
                        index.Add(Point.Count - 2);
                    else
                        index.Add(Point.FindLastIndex(p => p <= x1));
                }


                List<double> Dw = new List<double>(new double[index.Count]);
                for (int i = 0; i < index.Count; i++)
                {
                    if (index[i] == 0)
                        Dw[i] = Convert.ToDouble(DThaunch.Rows[0][3]);
                    else if (index[i] == Point.Count - 2)
                        Dw[i] = Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][5]);
                    else if ((index[i] - 1) % 4 == 0)
                    {
                        double H = Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][4]) - Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][3]);
                        if (H == 0)
                            Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][4]);
                        else
                            Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][3]) + H / Math.Pow(Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][0]), 2) * Math.Pow(Sta[i] - Point[index[i]], 2);
                    }
                    else if ((index[i] - 2) % 4 == 0)
                        Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 2) / 4][4]);
                    else if ((index[i] - 3) % 4 == 0)
                    {
                        double H = Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][4]) - Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][5]);
                        if (H == 0)
                            Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][4]);
                        else
                            Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][5]) + H / Math.Pow(Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][2]), 2) * Math.Pow(-Sta[i] + Point[index[i] + 1], 2);
                    }
                    else if ((index[i] - 4) % 4 == 0)
                        Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 4) / 4][5]);
                }

                return Dw;
            }
        }
        
        
        //public static List<double> Dw(double [] Aspan, DataTable DThaunch, List<double> Lx)
        //{
        //    int numinsup = Aspan.GetLength(0) - 1;
        //    int nhaunch = numinsup * 3 + numinsup - 1 + 2;
        //    double[] Ahaunch = new double[nhaunch];

        //    //Calculate Ahaunch
        //    Ahaunch[0] = Aspan[0] - Convert.ToDouble(DThaunch.Rows[0][1]) / 2 - Convert.ToDouble(DThaunch.Rows[0][0]);            
        //    Ahaunch[nhaunch - 1] = Aspan[Aspan.GetLength(0) - 1] - Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][1]) / 2 - Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][2]);            

        //    for (int i = 0; i < numinsup; i++)
        //    {
        //        Ahaunch[1 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][0]);                
        //        Ahaunch[2 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][1]);                
        //        Ahaunch[3 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][2]); 
        //    }

        //    if (numinsup > 1)
        //        for (int i = 0; i < numinsup - 1; i++)
        //        {
        //            Ahaunch[4 + i * 4] = Aspan[1 + i] - Convert.ToDouble(DThaunch.Rows[i][1]) / 2 - Convert.ToDouble(DThaunch.Rows[i][2]) - Convert.ToDouble(DThaunch.Rows[i + 1][1]) / 2 - Convert.ToDouble(DThaunch.Rows[i + 1][0]);                   
        //        }

        //    double[] b = new double[Ahaunch.GetLength(0) + 1];

        //    b[0] = 0;
        //    for (int i = 1; i < Ahaunch.GetLength(0) + 1; i++)
        //    {
        //        b[i] = 0;
        //        for (int j = 0; j < i; j++)
        //            b[i] = Ahaunch[j] + b[i];
        //    }

        //    //List of cumulate
        //    List<double> S = new List<double>(b);


        //    List<int> index = new List<int>(); 
        //    foreach (double x1 in Lx)
        //    {
        //        if (x1 == S.Max())
        //            index.Add(S.Count - 2);
        //        else
        //        index.Add(S.FindLastIndex(p => p <= x1));
        //    }
                

        //    List<double> Dw = new List<double>(new double [index.Count]);
        //    for (int i = 0; i < index.Count; i++)
        //    {
        //        if (index[i] == 0)
        //            Dw[i] = Convert.ToDouble(DThaunch.Rows[0][3]);
        //        else if (index[i] == nhaunch - 1)
        //            Dw[i] = Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][5]);
        //        else if ((index[i] - 1) % 4 == 0)
        //        {
        //            double H = Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][4]) - Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][3]);
        //            if (H == 0)
        //                Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][4]);
        //            else
        //                Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][3]) + H / Math.Pow(Convert.ToDouble(DThaunch.Rows[(index[i] - 1) / 4][0]), 2) * Math.Pow(Lx[i] - S[index[i]], 2);
        //        }                    
        //        else if ((index[i] - 2) % 4 == 0)
        //            Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 2) / 4][4]);
        //        else if ((index[i] - 3) % 4 == 0)
        //        {
        //            double H = Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][4]) - Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][5]);
        //            if (H == 0)
        //                Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][4]);
        //            else
        //                Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][5]) + H / Math.Pow(Convert.ToDouble(DThaunch.Rows[(index[i] - 3) / 4][2]), 2) * Math.Pow(-Lx[i] + S[index[i]+1], 2);
        //        }
        //        else if ((index[i] - 4) % 4 == 0)
        //            Dw[i] = Convert.ToDouble(DThaunch.Rows[(index[i] - 4) / 4][5]);
        //    }
           
        //    return Dw;
        //}

        
    }
}
