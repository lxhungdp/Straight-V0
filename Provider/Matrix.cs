﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Provider
{
    public class Matrix
    {
        public static double[,] Seperate(double[,] a, int index, int ndiv)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + ndiv-1];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (j < index)
                        b[i, j] = a[i, j];
                    else if (j == index)
                    {
                        
                        for (int k = j; k < j + ndiv; k++ )
                        {
                            b[i, k] = a[i, j];
                            b[0, k] = a[0, j] / ndiv;
                        }
                        
                    }
                    else
                        b[i, j + ndiv-1] = a[i, j];
                }

            return b;
        }

        public static double[,] Update(double[,] a, double spanlength)
        {
            double[,] b = a;
            double sum = 0;
            int col = a.GetLength(1);
            for (int i = 0; i < a.GetLength(1); i++)
                sum = sum + a[0, i];

            b[0, col - 1] = a[0, col - 1] - (sum - spanlength);

            return b;
        }

        public static double[,] Combine(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) - 1];
            for (int i = 0; i < b.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (j < index)
                        b[i, j] = a[i, j];
                    else if (j == index)
                    {
                        b[i, j] = a[i, j + 1];
                        b[0, j] = a[0, j] + a[0, j + 1];
                    }
                    else
                        b[i, j] = a[i, j + 1];
                }

            return b;
        }
        public static double[,] Seperate_cross(double[,] a, int index, int ndiv)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + ndiv - 1];
            if (a.GetLength(0) > 1)
            {
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (j < index)
                            b[i, j] = a[i, j];
                        else if (j == index)
                        {
                            for (int k = j; k < j + ndiv; k++)
                            {
                                b[2, k] = a[2, j] / ndiv;
                                b[1, k] = a[1, j];
                                b[0, k] = 3.0;
                                b[0, index] = a[0, j];
                            }
                           
                        }
                        else
                            b[i, j + ndiv - 1] = a[i, j];
                    }
            }
            else
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (j < index)
                        b[0, j] = a[0, j];
                    else if (j == index)
                    {
                        for (int k = j; k < j + ndiv; k++)
                        {
                            b[0, k] = a[0, j] / ndiv;
                        }                        
                    }
                    else
                        b[0, j + ndiv - 1] = a[0, j];
                }
            }




            return b;
        }

        public static double[,] Combine_cross(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) - 1];
            if (b.GetLength(0) > 1)
            {
                for (int i = 0; i < b.GetLength(0); i++)
                    for (int j = 0; j < b.GetLength(1); j++)
                    {
                        if (j < index - 1)
                            b[i, j] = a[i, j];
                        else if (j == index - 1)
                        {
                            b[0, j] = a[0, j];
                            b[1, j] = a[1, j];
                            b[2, j] = a[2, j] + a[2, j + 1];
                        }
                        else
                            b[i, j] = a[i, j + 1];
                    }
            }
            else
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (j < index - 1)
                        b[0, j] = a[0, j];
                    else if (j == index - 1)
                    {
                        b[0, j] = a[0, j] + a[0, j + 1];
                    }
                    else
                        b[0, j] = a[0, j + 1];
                }
            }


            return b;
        }

        public static double[,] Update_cross(double[,] a, double[] span)
        {

            double[,] b = new double[a.GetLength(0), a.GetLength(1)];
            int[] id = new int[span.GetLength(0) + 1];
            int j = 0;

            for (int i = 0; i < a.GetLength(1); i++)
            {
                if (a[0, i] == 1 || a[0, i] == 2)
                {
                    id[j] = i;
                    j = j + 1;
                }
            }

            id[j] = a.GetLength(1);

            double[,] s = new double[1, a.GetLength(1)];


            for (int i = 0; i < id.GetLength(0) - 1; i++)
            {
                for (int k = id[i]; k < id[i + 1]; k++)
                    s[0, i] = s[0, i] + a[2, k];
            }

            b = a;

            for (int i = 0; i < span.GetLength(0); i++)
            {

                b[2, id[i]] = span[i] - s[0, i] + a[2, id[i]];

            }

            return b;

        }
        public static double[,] Seperate_tran(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + 1];
            if (a.GetLength(0) > 1)
                for (int i = 0; i < b.GetLength(0); i++)
                    for (int j = 0; j < b.GetLength(1); j++)
                    {
                        if (j <= index)
                            b[i, j] = a[i, j];
                        else if (j == index + 1)
                        {
                            b[0, j] = a[0, j - 1];
                            b[1, j] = a[1, j - 1];
                            b[2, j] = a[1, j - 1] > 10 ? a[1, j - 1] : a[1, j - 1] * 10 + 1;
                        }
                        else
                            b[i, j] = a[i, j - 1];
                    }
            else
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (j <= index)
                        b[0, j] = a[0, j];
                    else if (j == index + 1)
                    {
                        b[0, j] = a[0, j - 1];
                    }
                    else
                        b[0, j] = a[0, j - 1];
                }


            return b;
        }
        public static double[,] Combine_tran(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) - 1];
            for (int i = 0; i < b.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (j < index)
                        b[i, j] = a[i, j];
                    else
                        b[i, j] = a[i, j + 1];
                }


            return b;
        }
        public static List<Node> Gridarrtolist(double[,] Across_grid, double [,] Atran, int ngirder)
        {
            double[] Longcu = new double[Across_grid.GetLength(1) + 1];
            Longcu[0] = 0;
            for (int i = 1; i < Across_grid.GetLength(1) + 1; i++)
            {
                Longcu[i] = 0;
                for (int j = 0; j < i; j++)
                    Longcu[i] = Across_grid[2, j] + Longcu[i];
            }

            double[] Trancu = new double[Atran.GetLength(1) - 1];

            Trancu[0] = 0;
            for (int i = 1; i < Atran.GetLength(1) - 1; i++)
            {
                Trancu[i] = 0;
                for (int j = 0; j < i; j++)
                    Trancu[i] = Atran[0, j + 1] + Trancu[i];
            }

            List<Node> Node = new List<Node>();
            int k = 1;
            for (int i = 0; i < Trancu.GetLength(0); i++)
            {
                for (int j = 0; j < Longcu.GetLength(0); j++)
                {
                    Node a = new Node();
                    a.Type = j == Longcu.GetLength(0) - 1 ? 1 : Across_grid[0, j];
                    a.BeamID = i == Trancu.GetLength(0) - 1 ? ngirder : Atran[2, i + 1];
                    a.X = Longcu[j];
                    a.Y = Trancu[i];
                    a.Z = 0;
                    a.Label = a.BeamID < 10 ? a.BeamID * 100 + j + 1 : (ngirder + k) * 100 + j + 1;

                    //Determine default restrain
                    // Not work if adding crossbeam or stringer
                    //will be updated later
                    
                    if ((a.Type == 1 || a.Type ==2) && (a.BeamID < 10))
                    {
                        if (i == Convert.ToInt32(Math.Floor(Atran.GetLength(1) / 2.0 - 1)))
                        {
                            if (j == Convert.ToInt32(Math.Floor(Across_grid.GetLength(1) / 2.0)) )
                                a.Restrain = "Fixed";
                            else
                                a.Restrain = "TranFixed";
                        }
                        else
                        {
                            if (j == Convert.ToInt32(Math.Floor(Across_grid.GetLength(1) / 2.0)))
                                a.Restrain = "LongFixed";
                            else
                                a.Restrain = "Free";
                        }
                    }
                    else                    
                    a.Restrain = "";

                    
                    Node.Add(a);
                }
                k = Atran[2, i + 1] > 10 ? k + 1 : k;
            }
            Node = Node.OrderBy(n => n.Label).ToList();
            return Node;
        }

        //Cumulate length of matrix Atop, Abot, ...
        public static double[,] Arrcumulate (double[,] a)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + 1];

            b[0, 0] = 0;
            for (int i = 1; i < a.GetLength(1) + 1; i++)
            {
                b[0, i] = 0;
                for (int j = 0; j < i; j++)
                    b[0, i] = a[0, j] + b[0, i];
            }


            for (int i = 1; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    b[i, j] = a[i, j];
            }

            return b;
           
        }


        public static List<Node> Addtoptolist(List<Node> Node1, double[,] Atop)
        {
            //Cumulate the Atop
            var b = Arrcumulate(Atop);

            for (int i = 0; i < b.GetLength(1); i++)
            {
                if (Node1.Select(p=>p.X).ToList().IndexOf(b[0,i]) == -1)
                {
                    Node a = new Node();
                    a.X = b[0, i];
                    a.Type = 4;
                    Node1.Add(a);
                }
            }

            return Node1;
        }
    }
}
