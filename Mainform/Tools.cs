using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mainform
{
    public class Tools
    {
        
        public static double [,] GridtoArray(DataGridView grid)
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
            
            //for (int r = 0; r < height; r++)
            //{
            //    grid.Rows[r].HeaderCell.Value = cellheader[r];                
            //}

        }
    }
}
