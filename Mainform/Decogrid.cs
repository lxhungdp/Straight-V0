using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Provider;
using Classes;
using System.Reflection;

namespace Mainform
{
    public class Decogrid
    {
        public static void gridCross(DataGridView dgv, double[,] arr)
        {
            dgv.DataSource = null;
            DGV.MatrixtoDGV(dgv, arr, 2);

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (arr[1, i] % 2 != 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.White;

                }
                else
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(217, 217, 217);

                }

                if (arr[0, i]  <=10)
                {
                    dgv.Rows[0].Cells[i].ReadOnly = true;
                    dgv.Rows[0].Cells[i].Style = new DataGridViewCellStyle { ForeColor = Color.Blue };
                }
            }
            resizegrid(dgv);
            dgv.ClearSelection();
        }

        public static void gridTran(DataGridView dgv, double[,] arr)
        {
            dgv.DataSource = null;
            DGV.MatrixAtrantoDGV(dgv, arr, 2);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (arr[1, i] == 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(218, 238, 243);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(218, 238, 243);
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(218, 238, 243);
                    
                }

                else if (arr[1, i] % 2 == 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(217, 217, 217);
                    dgv.Columns[i].ReadOnly = false;
                    dgv.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.White;
                    dgv.Columns[i].ReadOnly = false;
                    dgv.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
            resizegrid(dgv);
            dgv.ClearSelection();
        }

        public static void resizegrid(DataGridView dgv)
        {
            if (dgv.Columns.Count > 10)
            {
                foreach (DataGridViewColumn c in dgv.Columns)
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    c.MinimumWidth = 80;
                }
                dgv.Height = dgv.Rows.Count * 23 + 32 + 19;
            }
            else
            {
                foreach (DataGridViewColumn c in dgv.Columns)
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Height = dgv.Rows.Count * 23 + 32;
            }
        }

        public static void gridSection(DataGridView gridSection, double[,] Asection)
        {   
            DGV.MatrixtoDGV(gridSection, Asection, 0);

            for (int i = 0; i < Asection.GetLength(1); i++)
            {
                DataGridViewComboBoxCell cbx = new DataGridViewComboBoxCell();
                cbx.Items.Add("Barrier");
                cbx.Items.Add("Liveload");
                cbx.Items.Add("Pedestrian");
                gridSection.Rows[1].Cells[i].Value = null;
                gridSection.Rows[1].Cells[i] = cbx;

                //Set the default value for combobox
                if (Asection[1, i] == 3) //Pedestrian
                {
                    gridSection.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(218, 238, 243);
                    gridSection.EnableHeadersVisualStyles = false;
                    gridSection.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(218, 238, 243);
                    gridSection.Columns[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(218, 238, 243);
                    gridSection.Rows[1].Cells[i].Value = (cbx.Items[2]).ToString();
                }

                else if (Asection[1, i] == 2) //Liveload
                {
                    gridSection.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    gridSection.EnableHeadersVisualStyles = false;
                    gridSection.Columns[i].HeaderCell.Style.BackColor = Color.White;
                    gridSection.Columns[i].DefaultCellStyle.SelectionBackColor = Color.White;
                    gridSection.Rows[1].Cells[i].Value = (cbx.Items[1]).ToString();
                }

                else //Barrier
                {
                    gridSection.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                    gridSection.EnableHeadersVisualStyles = false;
                    gridSection.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);
                    gridSection.Columns[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(217, 217, 217);
                    gridSection.Rows[1].Cells[i].Value = (cbx.Items[0]).ToString();
                }
            }
            gridSection.Rows[0].Cells[0].ReadOnly = true;
            gridSection.Rows[0].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Blue };
            
            resizegrid(gridSection);
            gridSection.ClearSelection();
        }

        public static void girdHaunch(DataGridView dgv, double[,] Ahaunch)
        {
            DataTable DThaunch = new DataTable();
                DThaunch.Columns.Add("L1");
                DThaunch.Columns.Add("L2");
                DThaunch.Columns.Add("L3");
                DThaunch.Columns.Add("H1");
                DThaunch.Columns.Add("H2");
                DThaunch.Columns.Add("H3");
                string Hauheader = "Support #1";

                for (int i = 0; i < Ahaunch.GetLength(0); i++)
                {
                    if (i > 0)
                        Hauheader = Hauheader + ",Support #" + (i + 1).ToString();

                DThaunch.Rows.Add(Ahaunch[i, 0], Ahaunch[i, 1], Ahaunch[i, 2], Ahaunch[i, 3], Ahaunch[i, 4], Ahaunch[i, 5]);
                } 

            DGV.DTtoGrid(dgv, DThaunch);
           
            if (dgv.Rows.Count < 5)
                dgv.Height = dgv.Rows.Count * 23 + 32;
            else
                dgv.Height = 4 * 23 + 32;
            dgv.ClearSelection();
        }

        public static void gridCBox(DataGridView dgv, double[,] Acbox)
        {
            DataTable DThaunch = new DataTable();
            DThaunch.Columns.Add("L1");
            DThaunch.Columns.Add("L2");
            
            string Hauheader = "Support #1";

            for (int i = 0; i < Acbox.GetLength(0); i++)
            {
                if (i > 0)
                    Hauheader = Hauheader + ",Support #" + (i + 1).ToString();

                DThaunch.Rows.Add(Acbox[i, 0], Acbox[i, 1]);
            }

            DGV.DTtoGrid(dgv, DThaunch);
            
            if (dgv.Rows.Count < 5)
                dgv.Height = dgv.Rows.Count * 23 + 32;
            else
                dgv.Height = 4 * 23 + 32;
            dgv.ClearSelection();
        }
                     
        public static void gridBCon(DataGridView dgv, double[,] Acon, double[] Aspan)
        {
            DGV.Acontogrid(dgv, Acon, Aspan);
            
            if (dgv.Rows.Count < 5)
                dgv.Height = dgv.Rows.Count * 23 + 32;
            else
                dgv.Height = 4 * 23 + 32;
            dgv.ClearSelection();
        }

        public static void gridTop(DataGridView dgv, double[,] arr)
        {
            dgv.DataSource = null;
            DGV.MatrixtoDGV(dgv, arr, 2);

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (arr[1, i] % 2 != 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.White;

                }
                else
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(217, 217, 217);

                }

                if (arr[0, i] <= 10)
                {
                    dgv.Rows[0].Cells[i].ReadOnly = true;
                    dgv.Rows[0].Cells[i].Style = new DataGridViewCellStyle { ForeColor = Color.Blue };

                    
                }

                if (arr[1, i] % 2 == 0)
                {
                    dgv.Rows[1].Cells[i].ReadOnly = true;
                    dgv.Rows[1].Cells[i].Style = new DataGridViewCellStyle { ForeColor = Color.Blue };
                }
            }
            resizegrid(dgv);
            dgv.ClearSelection();
        }

        public static void gridBot(DataGridView dgv, double[,] arr)
        {
            dgv.DataSource = null;
            DGV.MatrixtoDGV(dgv, arr, 0);
            dgv.Rows[0].Cells[0].ReadOnly = true;
            dgv.Rows[0].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Blue };
            resizegrid(dgv);
            dgv.ClearSelection();
        }

        public static void gridRibtop(DataGridView dgv, double[,] arr)
        {
            dgv.DataSource = null;
            DGV.MatrixtoDGV(dgv, arr, 2);

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (arr[1, i] % 2 == 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.White;

                }
                else
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.Columns[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(217, 217, 217);
                    dgv.Columns[i].ReadOnly = true;
                    dgv.Columns[i].DefaultCellStyle.ForeColor = Color.Blue;

                }

                if (arr[0, i] <= 10)
                {
                    dgv.Rows[0].Cells[i].ReadOnly = true;
                    dgv.Rows[0].Cells[i].Style = new DataGridViewCellStyle { ForeColor = Color.Blue };


                }               
            }
            resizegrid(dgv);
            dgv.ClearSelection();
        }

        public static void gridKFrame(DataGridView dgv, List<KFrame> KFrame)
        {
            dgv.DataSource = KFrame;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells[2].Value.ToString() == "Exterior Support" || dgv.Rows[i].Cells[2].Value.ToString() == "Interior Support")
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                    dgv.Rows[i].Cells["Position"].ReadOnly = false;
                }
            }
            dgv.Height = dgv.Rows.Count <= 25 ? dgv.Rows.Count * 23 + 45 : 25 * 23 + 45 + 16;
            dgv.ClearSelection();
        }

        public static void gridCrossbeam<T>(DataGridView dgv, List<T> Crossbeam)
        {
            
            dgv.RowCount = Crossbeam.Count;
            
            var prop = Crossbeam[0].GetType().GetProperties();

            for (int i = 0; i < Crossbeam.Count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = prop[0].GetValue(Crossbeam[i], null);

                for (int j = 1; j < prop.GetLength(0); j++)
                    dgv.Rows[i].Cells[j-1].Value = prop[j].GetValue(Crossbeam[i], null);
            }
            dgv.Height = dgv.Rows.Count <= 4 ? dgv.Rows.Count * 24 + 42 : 4 * 24 + 42;
            dgv.ClearSelection();
            
        }

        public static void gridShoe (DataGridView dgv, List<Shoe> Shoe)
        {
            int ngirder = Shoe.LastOrDefault().Girder;
            int nshoe = Shoe.Count;
            dgv.RowCount = nshoe;            
                        
            for (int i = 0; i < nshoe; i++)
            {
                if (i % (nshoe / ngirder) == 0)
                 dgv.Rows[i].HeaderCell.Value = "Girder #" + Shoe[i].Girder.ToString();

                dgv.Rows[i].Cells[0].Value = Shoe[i].Label;
                dgv.Rows[i].Cells[1].Value = Shoe[i].EA;
                dgv.Rows[i].Cells[2].Value = Shoe[i].A;
                dgv.Rows[i].Cells[3].Value = Shoe[i].B;

                if (Shoe[i].Girder % 2 == 0)
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                else
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            dgv.Height = dgv.Rows.Count <= 4 ? dgv.Rows.Count * 24 + 42 : 4 * 24 + 42;
            dgv.ClearSelection();

            
        }

    }
}
