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
        
        public double[,] Harray
        {
            get
            {
                int numinsup = Aspan.GetLength(0) - 1;
                int nhaunch = numinsup * 3 + numinsup - 1 + 2;
                double[,] Ahaunch = new double[2,nhaunch];

                Ahaunch[0,0] = Aspan[0] - Convert.ToDouble(DThaunch.Rows[0][1]) / 2 - Convert.ToDouble(DThaunch.Rows[0][0]);
                Ahaunch[1, 0] = 0;

                Ahaunch[0,nhaunch - 1] = Aspan[Aspan.GetLength(0) - 1] - Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][1]) / 2 - Convert.ToDouble(DThaunch.Rows[DThaunch.Rows.Count - 1][2]);
                Ahaunch[1, nhaunch - 1] = 0;

                for (int i = 0; i < numinsup; i++)
                {
                    Ahaunch[0, 1 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][0]);
                    Ahaunch[1, 1 + i * 4] = 1;
                    
                    Ahaunch[0, 2 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][1]);
                    Ahaunch[1, 2 + i * 4] = 0;

                    Ahaunch[0,3 + i * 4] = Convert.ToDouble(DThaunch.Rows[i][2]);
                    Ahaunch[1, 3 + i * 4] = 1;
                }

                if (numinsup > 1)
                    for (int i = 0; i < numinsup - 1; i++)
                    {
                        Ahaunch[0, 4 + i * 4] = Aspan[1 + i] - Convert.ToDouble(DThaunch.Rows[i][1]) / 2 - Convert.ToDouble(DThaunch.Rows[i][2]) - Convert.ToDouble(DThaunch.Rows[i + 1][1]) / 2 - Convert.ToDouble(DThaunch.Rows[i + 1][0]);
                        Ahaunch[1, 4 + i * 4] = 0;
                    }


                return Ahaunch;
            }
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
        
        //Closed box section
        public static double[,] Closedbox(double[] Aspan, DataTable CBox)
        {
            int numinsup = Aspan.GetLength(0) - 1;
            int nhaunch = numinsup * 3 + 1;
            double[,] Ahaunch = new double[2, nhaunch];

            Ahaunch[0, 0] = Aspan[0] - Convert.ToDouble(CBox.Rows[0][0]);
            Ahaunch[1, 0] = 2;

            Ahaunch[0, nhaunch - 1] = Aspan[Aspan.GetLength(0) - 1] - Convert.ToDouble(CBox.Rows[CBox.Rows.Count - 1][1]);
            Ahaunch[1, nhaunch - 1] = 2;

            for (int i = 0; i < numinsup; i++)
            {
                Ahaunch[0, 1 + i * 3] = Convert.ToDouble(CBox.Rows[i][0]);
                Ahaunch[1, 1 + i * 3] = 1;               

                Ahaunch[0, 2 + i * 3] = Convert.ToDouble(CBox.Rows[i][1]);
                Ahaunch[1, 2 + i * 3] = 1;
            }

            if (numinsup > 1)
                for (int i = 0; i < numinsup - 1; i++)
                {
                    Ahaunch[0, 3 + i * 3] = Aspan[1 + i] - Convert.ToDouble(CBox.Rows[i][1]) - Convert.ToDouble(CBox.Rows[i+1][0]);
                    Ahaunch[1, 3 + i * 3] = 2;
                }


            return Ahaunch;
        }

        public static double [,] Bottomcon(double[] Aspan, double[,] Acon)
        {
            int pier = Aspan.GetLength(0) - 1;
            int left = 0;
            int right = 0;
            double Sleft = 0;
            double Sright = 0;
            for (int i = 0; i< Acon.GetLength(1); i++)
            {
                if (Acon[Acon.GetLength(0) - 1, i] == 1 || Acon[Acon.GetLength(0) - 1, i] == 11)
                {
                    left = left + 1;
                    Sleft = Sleft + Acon[0, i];
                }
                    
                else
                {
                    right = right + 1;
                    Sright = Sright + Acon[0, i];
                }                    
            }

            int n = (left + right + 1) * pier + 1;
            double[,] BC = new double[2, n];

            BC[0, 0] = Aspan[0] - Sleft;
            BC[1, 0] = 0;
            
            BC[0, n-1] = Aspan[Aspan.GetLength(0) - 1] - Sright;
            BC[1, n - 1] = 0;

            for (int i = 0; i < pier; i ++)
            {
                for (int j = 0; j < left; j++)
                {
                    BC[0, 1 + (left + right +1) * i + j] = Acon[0, j];
                    BC[1, 1 + (left + right + 1) * i + j] = Acon[i+1, j];
                }
                for (int k = 0; k < right; k++)
                {
                    BC[0, 1 + left + (left + right + 1) * i + k] = Acon[0, left + k];
                    BC[1, 1 + left + (left + right + 1) * i + k] = Acon[i+1, left + k];
                }
            }         

            if (pier > 1)
                for (int i = 0; i < pier - 1; i++)
                {
                    BC[0, (1 + left + right) * (i + 1)] = Aspan[1 + i] - Sleft - Sright;
                    BC[1, (1 + left + right) * (i + 1)] = 0;
                }


            return BC;
        }


        //public static DataTable AcontoDT(double [,] Acon)
        //{

        //    DataTable dt = new DataTable();
        //    int left = 0;
        //    int right = 0;

        //    for (int i = 0; i < Acon.GetLength(1); i++)
        //    {
        //        if (Acon[Acon.GetLength(0) - 1, 0] == 1 || Acon[Acon.GetLength(0) - 1, 0] == 11)
        //        {
        //            left = left + 1;                   
        //        } 
        //    }

        //    for (int i = 0; i < Acon.GetLength(1); i++)
        //    {               
        //        if (Acon[Acon.GetLength(0) - 1, 0] == 1 || Acon[Acon.GetLength(0) - 1, 0] == 11)
        //        {
        //            left = left + 1;
        //            dt.Columns.Add("Left #" + (left +1 - i).ToString());
        //        }
        //        else
        //        {
        //            right = right + 1;
        //            dt.Columns.Add("Right #" + right.ToString());
        //        }
                   
        //    }
           
            
        //    for (int i = 0; i < Acon.GetLength(0) - 1; i++)
        //    {                
        //        dt.Rows.Add();

        //        for (int j = 0; j < Acon.GetLength(1); j ++)
        //        {
        //            dt.Rows[i][j] = Acon[i, j];
        //        }
        //    }

            
        //    return dt;
        //}

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
