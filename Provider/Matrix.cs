﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;

namespace Provider
{
    public class Matrix
    {

        public static double[,] Seperate_con(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + 1];

            if (a[a.GetLength(0) - 1, index] == 1 || a[a.GetLength(0) - 1, index] == 11)
            {
                b[0, 0] = 5000;
                for (int i = 1; i < a.GetLength(0) - 1; i++)
                    b[i, 0] = 500;
                b[a.GetLength(0) - 1, 0] = 11;
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                        b[i, j + 1] = a[i, j];
            }
            else
            {
                b[0, a.GetLength(1)] = 5000;
                for (int i = 1; i < a.GetLength(0) - 1; i++)
                    b[i, a.GetLength(1)] = 500;
                b[a.GetLength(0) - 1, a.GetLength(1)] = 12;
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                        b[i, j] = a[i, j];

            }

            return b;
        }

        public static double[,] Combine_con(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) - 1];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1) - 1; j++)
                {
                    if (j < index)
                        b[i, j] = a[i, j];
                    else
                        b[i, j] = a[i, j + 1];
                }

            return b;
        }
        public static double[,] Update_con(double[,] a, double[,] c)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1)];

            for (int j = 0; j < a.GetLength(1); j++)
            {
                b[a.GetLength(0) - 1, j] = a[a.GetLength(0) - 1, j];
                for (int i = 0; i < a.GetLength(0) - 1; i++)
                    b[i, j] = c[i, j];
            }

            return b;
        }

        public static double[,] Seperate(double[,] a, int index, int ndiv)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + ndiv - 1];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (j < index)
                        b[i, j] = a[i, j];
                    else if (j == index)
                    {

                        for (int k = j; k < j + ndiv; k++)
                        {
                            b[i, k] = a[i, j];
                            b[0, k] = a[0, j] / ndiv;
                        }

                    }
                    else
                        b[i, j + ndiv - 1] = a[i, j];
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

        public static double[,] Seperate_transtif(double[,] a, int index, int ndiv)
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
                                b[0, k] = 6.0;
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

        public static double[,] Update_transtif(double[,] a, double[] span)
        {

            double[,] b = new double[a.GetLength(0), a.GetLength(1)];
            int[] id = new int[span.GetLength(0) + 1];
            int j = 0;

            for (int i = 0; i < a.GetLength(1); i++)
            {
                if (a[0, i] == 1 || a[0, i] == 2 || a[0, i] == 3)
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
        public static double[,] Seperate_sec(double[,] a, int index, int ndiv)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + ndiv - 1];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (j < index)
                        b[i, j] = a[i, j];
                    else if (j == index)
                    {

                        for (int k = j; k < j + ndiv; k++)
                        {
                            b[i, k] = a[i, j];
                            b[0, k] = a[0, j] / ndiv;
                        }

                    }
                    else
                        b[i, j + ndiv - 1] = a[i, j];
                }

            return b;
        }

        public static double[,] Seperate_top(double[,] a, int index, int ndiv)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + ndiv - 1];
            if (a.GetLength(0) > 4)
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
                                b[i, k] = a[i, j];
                                b[0, k] = 4.0;
                                b[0, index] = a[0, j];
                                b[2, k] = a[2, j] / ndiv;

                            }
                        }
                        else
                            b[i, j + ndiv - 1] = a[i, j];
                    }
            }
            else
            {
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (j < index)
                        {
                            b[i, j] = a[i, j];
                        }

                        else if (j == index)
                        {
                            for (int k = j; k < j + ndiv; k++)
                            {
                                b[i, k] = a[i, j];
                                b[0, k] = a[0, j] / ndiv;
                            }
                        }
                        else
                        {
                            b[i, j + ndiv - 1] = a[i, j];
                        }
                    }
                }
            }




            return b;
        }
        public static double[,] Combine_top(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) - 1];
            int n = b.GetLength(0) > 4 ? 2 : 0;
            for (int i = 0; i < b.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (j < index - 1)
                        b[i, j] = a[i, j];
                    else if (j == index - 1)
                    {
                        b[i, j] = a[i, j];
                        b[n, j] = a[n, j] + a[n, j + 1];
                    }
                    else
                        b[i, j] = a[i, j + 1];
                }

            return b;
        }

        public static double[,] Atop_CBox(DataTable DThaunch, double[] Aspan, int n)
        {
            int numinsup = DThaunch.Rows.Count;
            double[,] Atop2 = new double[n, 2 * numinsup + 1];
            Atop2[0, 0] = Aspan[0] - Convert.ToDouble(DThaunch.Rows[0][0]);
            Atop2[0, 2 * numinsup] = Aspan[numinsup] - Convert.ToDouble(DThaunch.Rows[numinsup - 1][1]);
            for (int i = 0; i < numinsup; i++)
                Atop2[0, i * 2 + 1] = Convert.ToDouble(DThaunch.Rows[i][0]) + Convert.ToDouble(DThaunch.Rows[i][1]);
            for (int i = 0; i < numinsup - 1; i++)
                Atop2[0, i * 2 + 2] = Aspan[i + 1] - Convert.ToDouble(DThaunch.Rows[i][1]) - Convert.ToDouble(DThaunch.Rows[i + 1][0]);

            return Atop2;
        }


        public static List<Node> GenerateNode(double[,] Across_grid, double[,] Atran, int ngirder)
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
                    a.Haunch = 0;
                    a.X = Longcu[j];
                    a.Y = Trancu[i];
                    a.Z = 0;
                    a.Joint = a.BeamID < 10 ? a.BeamID * 100 + j + 1 : (ngirder + k) * 100 + j + 1;
                    a.Label = a.Type == 1 ? "Exterior Support" : (a.Type == 2 ? "Interior Support" : "Cross Beam");

                    //Determine default restrain
                    // Not work if adding crossbeam or stringer
                    //will be updated later

                    if ((a.Type == 1 || a.Type == 2) && (a.BeamID < 10))
                    {
                        if (i == Convert.ToInt32(Math.Floor(Atran.GetLength(1) / 2.0 - 1)))
                        {
                            if (j == Convert.ToInt32(Math.Floor(Across_grid.GetLength(1) / 2.0)))
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
            Node = Node.OrderBy(n => n.BeamID).ThenBy(p=>p.X).ToList();
            
            return Node;
        }

        //Cumulate length of matrix Atop, Abot, ...
        public static double[,] Arrcumulate(double[,] a)
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


        //Add the node which is changed section to Node;
        public static List<Node> AddKframe(List<Node> Node, List<KFrame> KK)
        {
            var Node1 = Node.Where(p => p.BeamID == 1).ToList();
            var nlong = Node1.Count;
            var n = Node.Count / nlong;

            var K = KK.Where(p => p.Location == true).ToList();

            for (int i = 0; i < K.Count; i++)
            {
                int index = Node1.Select(p => p.X).ToList().IndexOf(K[i].Station * 1000);
                if (index == -1)
                {
                    int id = Node1.FindLastIndex(p => p.X <= K[i].Station * 1000);
                    for (int j = 0; j < n; j++)
                    {
                        Node a = Node[j * nlong + id].ShallowCopy();                        
                        a.X = K[i].Station * 1000;                        
                        a.Type = 7;
                        a.Label = "K - Frame";
                        a.Restrain = "";
                        Node.Add(a);
                    }

                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        Node[j * nlong + index].Type = 7;
                        Node[j * nlong + index].Label = "K - Frame";
                    }
                }
            }


            //Reorder by X and Y
            Node = Node.OrderBy(p => p.BeamID).ThenBy(p => p.X).ToList();

            //Rename the label
            for (int i = 0; i < n; i++)
                for (int j = 0; j < Node.Count / n; j++)
                {
                    Node[i * Node.Count / n + j].Joint = (i + 1) * 100 + j + 1;
                }

            // Set Lb
            Node1 = Node.Where(p => p.BeamID == 1).ToList();
            var Lb = Node.Where(p => (p.Type == 1 || p.Type == 2 || p.Type == 3 || p.Type == 7) && p.BeamID == 1).Select(p => p.X).ToList();
            nlong = Node1.Count;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < nlong - 1; j++)
                {
                    int Lbindex = Lb.FindLastIndex(p => p <= Node1[j].X);
                    Node[i * nlong + j].Lb = Lb[Lbindex + 1] - Lb[Lbindex];
                }
                Node[i * nlong + nlong - 1].Lb = Lb[Lb.Count-1] - Lb[Lb.Count - 2];
            }
            

            return Node;
        }

        // This function is to add to Existing List Node, by the matrix: 1st-row: spacing, 2nd ... property of node, Pro: corrresponding property)
        public static List<Node> Addnode(List<Node> Node, double[,] Atop, string pro, int Type, string expro)
        {
            //Cumulate the Atop
            //var Node = Nodein;
            var b = Arrcumulate(Atop);
            var nlong = Node.Where(p => p.BeamID == 1).ToList().Count;
            var n = Node.Count / nlong;
            string[] exprostr = expro.Split(',');
            int index;
            Node = Node.OrderBy(p => p.Y).ThenBy(p => p.X).ToList();
            //Insert the point if X is the new X
            for (int i = 0; i < b.GetLength(1); i++)
            {
                if (Node.Select(p => p.X).ToList().IndexOf(b[0, i]) == -1)
                {
                    for (int j = 0; j < n; j++) //Do for all girder
                    {
                        Node a = new Node();
                        a.X = b[0, i];
                        a.Z = 0;
                        a.Y = Node[j * nlong].Y;
                        a.BeamID = Node[j * nlong].BeamID;
                        a.Type = Type;
                        a.Label = "Section Changed";

                        index = Node.FindLastIndex(p => p.X <= b[0, i]);
                        for (int k = 0; k < exprostr.GetLength(0); k++)
                        {
                            PropertyInfo newpro = a.GetType().GetProperty(exprostr[k]);
                            PropertyInfo oldpro = Node[index].GetType().GetProperty(exprostr[k]);

                            newpro.SetValue(a, oldpro.GetValue(Node[index], null));
                        }

                        Node.Add(a);
                    }

                }
            }

            //Reorder by X and Y
            Node = Node.OrderBy(p => p.BeamID).ThenBy(p => p.X).ToList();

            //Rename the label
            for (int i = 0; i < n; i++)
                for (int j = 0; j < Node.Count / n; j++)
                {
                    Node[i * Node.Count / n + j].Joint = (i + 1) * 100 + j + 1;
                }

            //Add pro
            string[] prostring = pro.Split(',');
            double provalue = 0;

            for (int i = 0; i < prostring.GetLength(0); i++)
            {
                for (int j = 0; j < Node.Count; j++)
                {
                    for (int k = 0; k < b.GetLength(1); k++)
                    {
                        if (b[0, k] == Node[j].X)
                            provalue = b[i + 1, k];
                    }
                    PropertyInfo propertyInfo = Node[j].GetType().GetProperty(prostring[i]);
                    propertyInfo.SetValue(Node[j], provalue);
                }
            }


            return Node;
        }


        //Add node properties to Node;
        public static List<Node> Add1prop(List<Node> Node, double[] b, string a)
        {
            string[] pro = a.Split(',');

            for (int i = 0; i < pro.GetLength(0); i++)
            {
                for (int j = 0; j < Node.Count; j++)
                {
                    PropertyInfo propertyInfo = Node[j].GetType().GetProperty(pro[i]);
                    propertyInfo.SetValue(Node[j], b[i]);
                }
            }

            return Node;
        }

        public static List<Node> Addd0(List<Node> Node, double[,] Atop, string a)
        {

            var c = Arrcumulate(Atop);
            double b = 0;

            for (int j = 0; j < Node.Count; j++)
            {
                for (int k = 0; k < c.GetLength(1) - 1; k++)
                {
                    if (c[0, k] <= Node[j].X)
                        b = Atop[0, k];
                }
                PropertyInfo propertyInfo = Node[j].GetType().GetProperty(a);
                propertyInfo.SetValue(Node[j], b);
            }

            return Node;
        }
    }
}
