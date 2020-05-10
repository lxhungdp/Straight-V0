using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Provider
{
    public class DGV
    {
        public static double[,] GridtoArray(DataGridView grid)
        {
            var array = new double[grid.RowCount, grid.ColumnCount];
            foreach (DataGridViewRow i in grid.Rows)
            {
                if (i.IsNewRow) continue;
                foreach (DataGridViewCell j in i.Cells)
                {
                    array[j.RowIndex, j.ColumnIndex] = Convert.ToDouble(j.Value);
                }
            }
            return array;
        }

        public static void ArraytoGrid(DataGridView grid, double[,] value)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < value.GetLength(1); i++)
            {
                dt.Columns.Add((i + 1).ToString());
            }

            for (int i = 0; i < value.GetLength(0); ++i)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < value.GetLength(1); ++j)
                {
                    row[j] = value[i, j];
                }
                dt.Rows.Add(row);
            }

            grid.DataSource = dt;
            grid.ClearSelection();
            grid.CurrentCell = null;

            //for (int r = 0; r < height; r++)
            //{
            //    grid.Rows[r].HeaderCell.Value = cellheader[r];                
            //}

        }

        public static void ArraytoGrid_tran(DataGridView grid, double[,] value)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < value.GetLength(1); i++)
            {

                if (value[1, i] == 0)
                    dt.Columns.Add("A" + i.ToString());
                else
                    dt.Columns.Add("B" + i.ToString());


            }


            DataRow row = dt.NewRow();
            for (int j = 0; j < value.GetLength(1); ++j)
            {
                row[j] = value[0, j];
            }
            dt.Rows.Add(row);


            grid.DataSource = dt;

        }

        public static double[,] GridtoArray_tran(DataGridView grid, double[,] value)
        {
            var array = new double[grid.RowCount, grid.ColumnCount];
            foreach (DataGridViewRow i in grid.Rows)
            {
                if (i.IsNewRow) continue;
                foreach (DataGridViewCell j in i.Cells)
                {
                    array[j.RowIndex, j.ColumnIndex] = Convert.ToDouble(j.Value);
                }
            }

            var b = value;
            for (int i = 0; i < value.GetLength(1); i++)
                b[0, i] = array[0, i];

            return b;
        }
    }
}
