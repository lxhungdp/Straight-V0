using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;

namespace Provider
{
    public class DGV
    {
        
        //1 -  Grid to Matrix
        // ----------------------------------------------------               
        
        //Return Matrix after cell changed. Output matrix based on input matrix
        //2 first rows are  the same input matrix, from 3rd row update from grid

        public static double[,] Cellchanged(DataGridView grid, double[,] Across)
        {
            var a = new double[grid.RowCount, grid.ColumnCount];
            foreach (DataGridViewRow i in grid.Rows)
            {
                if (i.IsNewRow) continue;
                foreach (DataGridViewCell j in i.Cells)
                {
                    a[j.RowIndex, j.ColumnIndex] = Convert.ToDouble(j.Value);
                }
            }

            double[,] b = new double[a.GetLength(0) + 2, a.GetLength(1)];
            for (int j = 0; j < a.GetLength(1); j++)
            {
                b[0, j] = Across[0, j];
                b[1, j] = Across[1, j];
                for (int i = 0; i < a.GetLength(0); i++)
                    b[i + 2, j] = a[i, j];
            }

            return b;
        }               

        //Return 1st row matrix after change gridSection
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



        //Return Matrix from grid
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

        //Return Acon array (last column has index 1, 11, 2, 12)
        public static double[,] GridtoArray_con(DataGridView grid, double[,] Acon)
        {
            double[,] array = new double[grid.RowCount, grid.ColumnCount];
            foreach (DataGridViewRow i in grid.Rows)
            {
                if (i.IsNewRow) continue;
                foreach (DataGridViewCell j in i.Cells)
                {
                    array[j.RowIndex, j.ColumnIndex] = Convert.ToDouble(j.Value);
                }
            }
            double[,] b = (double[,])Acon.Clone();

            for (int j = 0; j < Acon.GetLength(1); j++)
            {
                b[Acon.GetLength(0) - 1, j] = Acon[Acon.GetLength(0) - 1, j];
                for (int i = 0; i < array.GetLength(0); i++)
                    b[i, j] = array[i, j];
            }
            return b;
        }

        // 2 - Matrix to Grid
        //---------------------------------------------------------
        //Fill grid by matrix value from n-row
        public static void MatrixtoDGV(DataGridView grid, double[,] value, int n)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < value.GetLength(1); i++)
            {
                dt.Columns.Add((i + 1).ToString());
            }

            for (int i = n; i < value.GetLength(0); ++i)
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
        }

        // Fill gridTran by matrix value
        public static void MatrixAtrantoDGV(DataGridView grid, double[,] value, int n)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < value.GetLength(1); i++)
            {
                if (i == 0)
                    dt.Columns.Add("B-Left");
                else if (i == value.GetLength(1) - 1)
                    dt.Columns.Add("B-Right");
                else
                    dt.Columns.Add("A" + i.ToString());
            }


            for (int i = n; i < value.GetLength(0); ++i)
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
        }

        public static void Acontogrid(DataGridView grid, double[,] Acon, double []Aspan)
        {
            int pier = Aspan.GetLength(0) - 1;
            grid.DataSource = null;
            grid.ColumnCount = Acon.GetLength(1);
            grid.RowCount = pier + 1;
            

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
                    cheader[i] = "Left #" + (left - i).ToString();
                }
                else
                {
                    right = right + 1;
                    cheader[i] = "Right #" + right.ToString();
                }

                grid.Columns[i].HeaderText = cheader[i];
            }

            string[] rheader = new string[pier + 2];
            rheader[0] = "Length (mm)";
            for (int i = 1; i < pier + 1; i++)
            {
                rheader[i] = "Support #" + i.ToString();
            }

            for (int i = 0; i < pier + 1; i++)
            {
                grid.Rows[i].HeaderCell.Value = rheader[i];
                for (int j = 0; j < Acon.GetLength(1); j++)
                    grid.Rows[i].Cells[j].Value = Acon[i, j];
            }

        }



        // 3 - Datatable to Grid
        //---------------------------------------------------------
        // Fill gird haunch by DThaunch
        public static void DTtoGrid(DataGridView grid, DataTable dt)
        {
            grid.RowCount = dt.Rows.Count;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grid.Rows[i].HeaderCell.Value = "Support #" + (i+1).ToString();
                for (int j = 0; j < dt.Columns.Count; j++)
                    grid.Rows[i].Cells[j].Value = dt.Rows[i][j].ToString();
            }
        }


        // 4 - Grid to Datatable
        //---------------------------------------------------------
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

        // 5 - Grid to list
        public static List<KFrame> KFrame(DataGridView grid)
        {
            List<KFrame> KFrame = new List<KFrame>();
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grid.Rows[i].Cells[1].Value) && grid.Rows[i].Cells[2].Value.ToString() == "")
                    KFrame.Add(new KFrame((double)grid.Rows[i].Cells[0].Value, true, grid.Rows[i].Cells[2].Value.ToString()));
                else
                    KFrame.Add(new KFrame((double)grid.Rows[i].Cells[0].Value, false, grid.Rows[i].Cells[2].Value.ToString()));
            }
            return KFrame;
        }

        public static List<Crossbeam> Crossbeam(DataGridView grid)
        {
            List<Crossbeam> Crossbeam = new List<Crossbeam>();
            Crossbeam.Add(new Crossbeam("Stringer", 0, 0, 0, 0, 0, 0, 0));
            Crossbeam.Add(new Crossbeam("Exterior-Support Crossbeam", 0, 0, 0, 0, 0, 0, 0));
            Crossbeam.Add(new Crossbeam("General Crossbeam", 0, 0, 0, 0, 0, 0, 0));
            Crossbeam.Add(new Crossbeam("Interior-Support Crossbeam", 0, 0, 0, 0, 0, 0, 0));

            foreach (DataGridViewRow row in grid.Rows)
            {
                string type = row.HeaderCell.Value.ToString();               
                double ttop = Convert.ToDouble(row.Cells[0].Value.ToString());
                double btop = Convert.ToDouble(row.Cells[1].Value.ToString());
                double tbot = Convert.ToDouble(row.Cells[2].Value.ToString());
                double bbot = Convert.ToDouble(row.Cells[3].Value.ToString());
                double D = Convert.ToDouble(row.Cells[4].Value.ToString());
                double tw = Convert.ToDouble(row.Cells[5].Value.ToString());
                double nw = Convert.ToDouble(row.Cells[6].Value.ToString());
                if (type == "Stringer")
                    Crossbeam[0] = new Crossbeam(type, ttop, btop, tbot, bbot, D, tw, nw);
                else if (type == "Exterior-Support Crossbeam")
                    Crossbeam[1] = new Crossbeam(type, ttop, btop, tbot, bbot, D, tw, nw);
                else if (type == "General Crossbeam")
                    Crossbeam[2] = new Crossbeam(type, ttop, btop, tbot, bbot, D, tw, nw);
                else if (type == "Interior-Support Crossbeam")
                    Crossbeam[3] = new Crossbeam(type, ttop, btop, tbot, bbot, D, tw, nw);
            }
            return Crossbeam;
        }

        public static List<Parapet> Parapet(DataGridView grid)
        {
            List<Parapet> Parapet = new List<Parapet>();
            Parapet.Add(new Parapet("Left-side barrier", 0, 0, 0, 0, 0, 0, 0));
            Parapet.Add(new Parapet("Jersey barrier", 0, 0, 0, 0, 0, 0, 0));
            Parapet.Add(new Parapet("Right-side barrier", 0, 0, 0, 0, 0, 0, 0));

            foreach (DataGridViewRow row in grid.Rows)
            {
                string type = row.HeaderCell.Value.ToString();
                double H1 = Convert.ToDouble(row.Cells[0].Value.ToString());
                double H2 = Convert.ToDouble(row.Cells[1].Value.ToString());
                double H3 = Convert.ToDouble(row.Cells[2].Value.ToString());
                double B1 = Convert.ToDouble(row.Cells[3].Value.ToString());
                double B2 = Convert.ToDouble(row.Cells[4].Value.ToString());
                double B3 = Convert.ToDouble(row.Cells[5].Value.ToString());
                double e = Convert.ToDouble(row.Cells[6].Value.ToString());
                
                if (type == "Left-side barrier")
                    Parapet[0] = new Parapet(type, H1, H2, H3, B1, B2, B3, e);
                else if (type == "Jersey barrier")
                    Parapet[1] = new Parapet(type, H1, H2, H3, B1, B2, B3, e);
                else if (type == "Right-side barrier")
                    Parapet[2] = new Parapet(type, H1, H2, H3, B1, B2, B3, e);
            }
            return Parapet;
        }

        public static List<Shoe> Shoe(DataGridView grid, List<Shoe> Shoe1)
        {
            List<Shoe> Shoe = new List<Shoe>(Shoe1);

            int i = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {                
                string Label =row.Cells[0].Value.ToString();
                int EA = Convert.ToInt32(row.Cells[1].Value.ToString());
                double A = Convert.ToDouble(row.Cells[2].Value.ToString());
                double B = Convert.ToDouble(row.Cells[3].Value.ToString());

                Shoe[i].Label = Label;
                Shoe[i].EA = EA;
                Shoe[i].A = A;
                Shoe[i].B = B;
                i = i + 1;
            }
            return Shoe;
        }




        public static List<int> Selectindex(CheckBox KFrame, CheckBox Secchanged)
        {
            List<int> kindex = new List<int>();
            if (Secchanged.Checked == false)
            {
                kindex.Add(5);
                kindex.Add(6);
            }                         
                           
            if (KFrame.Checked == false)            
                kindex.Add(7); 
            return kindex;
        }
    }
}
