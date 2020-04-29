using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mainform
{
    public class Matrix
    {
        public static double[,] Seperate(double [,] a, int index)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1)+1];
            for (int i = 0; i< a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (j < index)
                        b[i, j] = a[i, j];
                    else if (j == index)
                    {
                        b[i, j] = a[i, j];
                        b[i, j + 1] = a[i, j];
                        b[0, j] = a[0, j] / 2.0;
                        b[0, j+1] = a[0, j] / 2.0;
                    }
                    else
                        b[i, j+1] = a[i, j];
                }
            
            return b;
        }

        public static double [,] Update(double[,] a, double spanlength)
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
                        b[i, j] = a[i, j+1];                        
                        b[0, j] = a[0, j] + a[0, j+1];                        
                    }
                    else
                        b[i, j ] = a[i, j+1];
                }

            return b;
        }
    }
}
