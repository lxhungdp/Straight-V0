using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mainform
{
    public class Matrix
    {
        public static double[,] Seperate(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + 1];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (j < index)
                        b[i, j] = a[i, j];
                    else if (j == index)
                    {
                        b[i, j] = a[i, j];
                        b[i, j + 1] = a[i, j];
                        b[0, j] = a[0, j] / 2.0;
                        b[0, j + 1] = a[0, j] / 2.0;
                    }
                    else
                        b[i, j + 1] = a[i, j];
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
        public static double[,] Seperate_cross(double[,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + 1];
            if (a.GetLength(0) > 1)
            {
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (j < index)
                            b[i, j] = a[i, j];
                        else if (j == index)
                        {
                            b[2, j] = a[2, j] / 2.0;
                            b[2, j + 1] = a[2, j] / 2.0;
                            b[0, j] = a[0, j];
                            b[0, j + 1] = 3.0;
                            b[1, j] = a[1, j];
                            b[1, j + 1] = a[1, j];
                        }
                        else
                            b[i, j + 1] = a[i, j];
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
                        b[0, j] = a[0, j] / 2.0;
                        b[0, j + 1] = a[0, j] / 2.0;
                    }
                    else
                        b[0, j + 1] = a[0, j];
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
                    else if (j == index+1)
                    {
                        b[0, j] = a[0, j-1];
                        b[1, j] = a[1, j - 1];
                        b[2, j] = a[1, j - 1] > 10 ? a[1, j - 1] : a[1, j - 1]*10+1;
                    }
                    else
                        b[i, j] = a[i, j-1];
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
    }
}
