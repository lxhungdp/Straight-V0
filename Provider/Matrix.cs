using System;
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

        //1 - Add more column to matrix
        // ---------------------------------------        
        //Add column with ID = 11 at location index
        public static double[,] Add(double[,] a, int index, int ndiv)
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
                            if (i == 0)
                            {
                                b[i, k] = 11;  // => New id is 11
                                b[i, index] = a[0, j];
                            }
                            else if (i == 1)
                                b[i, k] = a[i, j];
                            else if (i == 2)
                                b[i, k] = a[i, j] / ndiv;
                            else
                                b[i, k] = a[i, j];
                        }
                    }
                    else
                        b[i, j + ndiv - 1] = a[i, j];
                }
           
            return b;
        }

        // Divide at index col or all into ndiv segment
        public static double [,] Divide(double[,] a, int index, int ndiv, bool divideall)
        {
            if (divideall == false)                            
                return Add(a, index, ndiv);           
                
            else
            {
                var b = (double[,])a.Clone();                           
                for (int i = 0; i < a.GetLength(1); i++)                
                    b = Add(b, i * ndiv, ndiv);
                return b;
            }
        }

        //Add column for Asection: 1st row length : divide, 2nd row the same
        public static double[,] Add_section(double[,] a, int index, int ndiv)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + 1];

            for (int j = 0; j < b.GetLength(1); j++)
            {
                if (j < index)
                {
                    for (int i = 0; i < a.GetLength(0); i++)
                        b[i, j] = a[i, j];                   
                }

                else if (j == index)
                {
                    for (int i = 0; i < a.GetLength(0); i++)
                        b[i, j] = a[i, j];
                    b[0, j] = a[0, j] / 2;
                    
                }
                else if (j == index + 1)
                {
                    for (int i = 0; i < a.GetLength(0); i++)
                        b[i, j] = a[i, j - 1];
                    b[0, j] = a[0, j - 1] / 2;                    
                }
                else
                {
                    for (int i = 0; i < a.GetLength(0); i++)
                        b[i, j] = a[i, j - 1];                    
                }
            }
            return b;
        }
        //Add colum to Acon
        public static double[,] Add_con(double[,] a, int index)
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

        //2 - Remove colum from matrix
        // -------------------------------------------
        // Delete column at index - column. The loc-row is combined
        public static double[,] Delete(double[,] a, int index, int loc)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) - 1];

            for (int i = 0; i < b.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (j < index - 1)
                        b[i, j] = a[i, j];
                    else if (j == index - 1)
                    {
                        if (i == loc) //Location of row is combined
                            b[i, j] = a[i, j] + a[i, j + 1];
                        else
                            b[i, j] = a[i, j]; 
                    }
                    else
                        b[i, j] = a[i, j + 1];
                }

            return b;
        }

        public static double[,] Delete_con(double[,] a, int index)
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


        // 3 - Update cell changed
        // -------------------------------------------
        //1. Update the cell has ID<=10
        public static double[,] Cellchanged(double[,] a, double[] span)
        {

            double[,] b = new double[a.GetLength(0), a.GetLength(1)];
            int[] id = new int[span.GetLength(0) + 1];
            int j = 0;

            for (int i = 0; i < a.GetLength(1); i++)
            {
                if (a[0, i] <=10) //If < 10 => sum
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
        //2. Update the last cell
        public static double[,] Update(double[,] a, double spanlength)

        {
            double[,] b = a;
            double sum = 0;

            int col = a.GetLength(1);
            for (int i = 0; i < a.GetLength(1); i++)
                sum = sum + a[0, i];

            b[0, 0] = a[0, 0] - (sum - spanlength);

            return b;
        }

        public static bool checkAsection(double[,] Asection)
        {
            int le = Asection.GetLength(1);
            double[] id = new double[le];
            for (int i = 0; i < le; i++)
                id[i] = Asection[1, i];
            
            bool r;

            if (le == 3)
            {
                if (id[0] == 1 && id[1] == 2 && id[2] == 1)
                    r = true;
                else
                    r = false;
            }
            else if (le == 4)
            {
                if ((id[0] == 1 && id[1] == 3 && id[2] == 2 && id[3] == 1) || (id[0] == 1 && id[1] == 2 && id[2] == 3 && id[3] == 1))
                    r = true;
                else
                    r = false;
            }
            else if (le == 5)
            {
                if ((id[0] == 1 && id[1] == 2 && id[2] == 1 && id[3] == 2 && id[4] == 1) || (id[0] == 1 && id[1] == 3 && id[2] == 2 && id[3] == 3 && id[4] == 1))
                    r = true;
                else
                    r = false;
            }
            else if (le == 6)
            {
                if ((id[0] == 1 && id[1] == 3 && id[2] == 2 && id[3] == 1 && id[4] == 2 && id[5] == 1) || (id[0] == 1 && id[1] == 2 && id[2] == 1 && id[3] == 2 && id[4] == 3 && id[5] == 1))
                    r = true;
                else
                    r = false;
            }
            else if (le == 7)
            {
                if (id[0] == 1 && id[1] == 3 && id[2] == 2 && id[3] == 1 && id[4] == 2 && id[5] == 3 && id[6] == 1)
                    r = true;
                else
                    r = false;
            }
            else
                r = false;
            return r;
        }

        public static List<Shoe> UpdateShoe(List<Shoe> Shoe1, int row)
        {
            int ngirder = Shoe1.LastOrDefault().Girder;
            int nshoe = Shoe1.Count;
            List<Shoe> Shoe = new List<Shoe>(Shoe1);
            for (int i = 0; i < nshoe / ngirder; i++)
                for (int j = i; j < nshoe; j = j + nshoe / ngirder)
                    if (j != row)
                        Shoe[j].Support = Shoe1[row].Support;
            return Shoe;
        }
       
    }
}
