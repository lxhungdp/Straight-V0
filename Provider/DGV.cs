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

        public static double[,] GridtoArray_sec(DataGridView grid)
        {
            var array = new double[grid.RowCount, grid.ColumnCount];
            foreach (DataGridViewRow i in grid.Rows)
            {
                if (i.IsNewRow) continue;
                foreach (DataGridViewCell j in i.Cells)
                {
                    if (j.RowIndex == 0)
                        array[j.RowIndex, j.ColumnIndex] = Convert.ToDouble(j.Value);
                }
            }
            return array;
        }

        public static void DTtoGrid(DataGridView grid, DataTable dt, string header)
        {


            grid.RowCount = dt.Rows.Count;
            string[] name = header.Split(',');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grid.Rows[i].HeaderCell.Value = name[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                    grid.Rows[i].Cells[j].Value = dt.Rows[i][j].ToString();
            }
        }

        public static void Acontogrid(DataGridView grid, double[,] Acon)
        {
            grid.DataSource = null;
            grid.ColumnCount = Acon.GetLength(1);
            grid.RowCount = Acon.GetLength(0) - 1;

            int left = 0;
            int right = 0;
            string[] cheader = new string[Acon.GetLength(1)];

            for (int i = 0; i < Acon.GetLength(1); i++)
            {
                if (Acon[Acon.GetLength(0) - 1, i] == 1 || Acon[Acon.GetLength(0) - 1, i] == 11)
                {
                    left = left + 1;
                }
            }

            for (int i = 0; i < Acon.GetLength(1); i++)
            {

                if (Acon[Acon.GetLength(0) - 1, i] == 1 || Acon[Acon.GetLength(0) - 1, i] == 11)
                {
                    cheader[i] = "Left #" + (left  - i).ToString();
                }
                else
                {
                    right = right + 1;
                    cheader[i] = "Right #" + right.ToString();
                }

                grid.Columns[i].HeaderText = cheader[i];
            }

            string[] rheader = new string[Acon.GetLength(0) - 1];
            rheader[0] = "Length (mm)";
            for (int i = 1; i < Acon.GetLength(0) - 1; i++)
            {
                rheader[i] = "Support #" + i.ToString();
            }

            for (int i = 0; i < Acon.GetLength(0) - 1; i++)
            {
                grid.Rows[i].HeaderCell.Value = rheader[i];
                for (int j = 0; j < Acon.GetLength(1); j++)
                    grid.Rows[i].Cells[j].Value = Acon[i, j];
            }

        }

        public static DataTable GridtoDT(DataGridView grid)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn dataGridViewColumn in grid.Columns)
            {
                if (dataGridViewColumn.Visible)
                {
                    dt.Columns.Add();
                }
            }
            var cell = new object[grid.Columns.Count];
            foreach (DataGridViewRow dataGridViewRow in grid.Rows)
            {
                for (int i = 0; i < dataGridViewRow.Cells.Count; i++)
                {
                    cell[i] = dataGridViewRow.Cells[i].Value;
                }
                dt.Rows.Add(cell);
            }
            return dt;
        }
    }
}
