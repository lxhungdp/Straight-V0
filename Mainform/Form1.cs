using Mainform.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mainform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Change color
            Setinitialize();

            
            //MessageBox.Show(a.GetLength(0).ToString());


            //MessageBox.Show(b[1, 1].ToString());
        }


        //Change background color
        private void Setinitialize() 
        {
            flowLayoutPanel1.BackColor = Color.FromArgb(33, 115, 70);
            panel1.BackColor = Color.FromArgb(33, 115, 70);
            btGeneral.BackColor = Color.FromArgb(33, 115, 70);
            btBridge.BackColor = Color.FromArgb(33, 115, 70);
            button3.BackColor = Color.FromArgb(44, 152, 93);
            button4.BackColor = Color.FromArgb(44, 152, 93);
            button5.BackColor = Color.FromArgb(44, 152, 93);
            button6.BackColor = Color.FromArgb(44, 152, 93);
            button7.BackColor = Color.FromArgb(44, 152, 93);
            button8.BackColor = Color.FromArgb(44, 152, 93);
            button9.BackColor = Color.FromArgb(44, 152, 93);
            btBack.BackColor = Color.FromArgb(33, 115, 70);
            btApply.BackColor = Color.FromArgb(33, 115, 70);
            btNext.BackColor = Color.FromArgb(33, 115, 70);
            btAnalysis.BackColor = Color.FromArgb(33, 115, 70);

            gridAB.RowCount = 1;
            gridAB.Rows[0].Height = 30;
            cbType.SelectedIndex = 0;
            labelEx.Text = "Ex. For a bridges with 3 spans, the lengths are 30m, 40m, 30m, and distance from end-beam to";
            labelEx2.Text = "support point is 0.5m, input string should be: 0.5+30+40+30+0.5";

            labelCode1.Text = "(1) 도로교 설계기준 (한계상태설계법) 해설, (2015) - (사)한국교량및구조공화회∙교량설계핵심기술연구단";
            labelCode2.Text = "(2) 강구조설계기준 및 해설 (하중저항계수법), (2018) - (사)한국강구조학회";
            labelCode3.Text = "(3) AASHTO LRFD, (2017)";


            metroTabControl1.SelectedTab = pageGeneral;

            Setgridview(gridTop);
            Setgridview(gridBot);
            Setgridview(gridWeb);
            Setgridview(gridCon);

            pictureBox1.Load(@"C:\Home1\Picture\Section.PNG");
        }

        private void Setgridview(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(189, 215, 238);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
        }

        private bool isCollapsed;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                btBridge.Image = Resources.Collapse_Arrow_20px;
                panelBP.Height += 10;
                if (panelBP.Size == panelBP.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                btBridge.Image = Resources.Expand_Arrow_20px;
                panelBP.Height -= 10;
                if (panelBP.Size == panelBP.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }

            }
        }

        private void btBridge_Click(object sender, EventArgs e)
        {
            timer1.Start();


            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            
        }

        private void btGeneral_Click(object sender, EventArgs e)
        {

            TabPage[] a = { pageGeneral };
            showtabpage(a);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

            TabPage[] a = { pageAnalysis };
            showtabpage(a);

           

        }

        
        void showtabpage(TabPage[] a)
        {   
            metroTabControl1.TabPages.Clear();
            foreach (TabPage page in metroTabControl1.TabPages)
                metroTabControl1.TabPages.Remove(page);
            foreach (TabPage a1 in a)
                metroTabControl1.TabPages.Add(a1); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageSteel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageDeck;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageStiffeners;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageCross;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageSectional;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageSupports;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageProp;
        }

        
        // Limit the input value in datagridview is only numec
       
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow only number and dot
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') )
            {
                e.Handled = true;
            }
            //only allow one decimal point
            if (e.KeyChar == '.'  && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void gridAB_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (gridAB.CurrentCell.ColumnIndex == 0 || gridAB.CurrentCell.ColumnIndex == 1 || gridAB.CurrentCell.ColumnIndex == 2 || gridAB.CurrentCell.ColumnIndex == 3 || gridAB.CurrentCell.ColumnIndex == 4 || gridAB.CurrentCell.ColumnIndex == 5 || gridAB.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                
                TextBox tb = e.Control as TextBox;
                if (tb != null) 
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

               
        //Allow only input number and "+"
        private void txtSpan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageGeneral":
                    {
                        TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
                        showtabpage(a);
                        metroTabControl1.SelectedTab = pageSteel;
                    }
                    break;
                case "pageSteel":
                    {                        
                        metroTabControl1.SelectedTab = pageDeck;
                        

                    }
                    break;

            }


        }

        private void btBack_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageSteel":
                    {
                        TabPage[] a = { pageGeneral };
                        showtabpage(a);                        
                    }
                    break;
                case "pageDeck":
                    {
                        metroTabControl1.SelectedTab = pageSteel;
                    }
                    break;

            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(this.numericUpDown1.Value);
        }

        

        double[,] Arr_top = new double[3, 1];
        double[,] Arr_bot = new double[3, 1];
        double[,] Arr_web = new double[3, 1];
        double[,] Arr_con = new double[2, 1];

        double[] Arr_span;
        double sumspan;
        private void btApply_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageGeneral":
                    {

                        
                        Arr_span = Array.ConvertAll(txtSpan.Text.ToString().Split('+'), Double.Parse);
                        sumspan = Arr_span.Sum();

                        Arr_top = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Arr_top[0, 0] = sumspan;
                        Tools.ArraytoGrid(gridTop, Arr_top);

                        Arr_bot = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Arr_bot[0, 0] = sumspan;
                        Tools.ArraytoGrid(gridBot, Arr_bot);

                        Arr_web = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Arr_web[0, 0] = sumspan;
                        Tools.ArraytoGrid(gridWeb, Arr_web);

                        Arr_con = new double[2, 1] { { 0 }, { 0 }};
                        Arr_con[0, 0] = sumspan;
                        Tools.ArraytoGrid(gridCon, Arr_con);



                    }
                    break;
                case "pageSteel":
                    {
                        double a;
                        a = gridTop.Rows[2].Height;
                        MessageBox.Show(a.ToString());
                    }
                    break;

            }
        }

        int index;
        DataGridView SelectedDGV;
        private void grid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if ((e.Button != MouseButtons.Right) || !(sender is DataGridView a))
                return;

            contextMenuStrip1.Show(Cursor.Position);
            index = e.ColumnIndex;
            if (index == a.ColumnCount-1)
                deleteTool.Enabled = false;
            else
                deleteTool.Enabled = true;
            SelectedDGV = a;

        }

        

        private void addTool_Click(object sender, EventArgs e)
        {

            if (SelectedDGV == gridTop)
            {
                Arr_top = Matrix.Seperate(Arr_top, index);
                Tools.ArraytoGrid(gridTop, Arr_top);
            }
            
            else if (SelectedDGV == gridBot)
            {
                Arr_bot = Matrix.Seperate(Arr_bot, index);
                Tools.ArraytoGrid(gridBot, Arr_bot);
            }
            else if (SelectedDGV == gridWeb)
            {
                Arr_web = Matrix.Seperate(Arr_web, index);
                Tools.ArraytoGrid(gridWeb, Arr_web);
            }
            else if (SelectedDGV == gridCon)
            {
                Arr_con = Matrix.Seperate(Arr_con, index);
                Tools.ArraytoGrid(gridCon, Arr_con);
            }



            if (SelectedDGV.Columns.Count > 10)
                foreach (DataGridViewColumn c in SelectedDGV.Columns)
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    c.MinimumWidth = 80;
                    SelectedDGV.Height = SelectedDGV.Rows.Count * 23 + 32 + 19;
                }
            SelectedDGV.ClearSelection();
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (sender == gridTop)
            {               
                Arr_top = Tools.GridtoArray(gridTop);
                Arr_top = Matrix.Update(Arr_top, sumspan);
                Tools.ArraytoGrid(gridTop, Arr_top);
                gridTop.MultiSelect = false;
            }
            else if (sender == gridBot)
            {                
                Arr_bot = Tools.GridtoArray(gridBot);
                Arr_bot = Matrix.Update(Arr_bot, sumspan);
                Tools.ArraytoGrid(gridBot, Arr_bot);
                gridBot.MultiSelect = false;
            }
            else if (sender == gridWeb)
            {
                Arr_web = Tools.GridtoArray(gridWeb);
                Arr_web = Matrix.Update(Arr_web, sumspan);
                Tools.ArraytoGrid(gridWeb, Arr_web);
                gridWeb.MultiSelect = false;
            }
            else if (sender == gridCon)
            {
                Arr_con = Tools.GridtoArray(gridCon);
                Arr_con = Matrix.Update(Arr_con, sumspan);
                Tools.ArraytoGrid(gridCon, Arr_con);
                gridCon.MultiSelect = false;
            }


            //gridTop.Rows[row+1].Cells[col].Selected = true;
        }

        private void deleteTool_Click(object sender, EventArgs e)
        {

            if (SelectedDGV == gridTop)
            {
                Arr_top = Matrix.Combine(Arr_top, index);
                Tools.ArraytoGrid(gridTop, Arr_top);
               

            }
            else if (SelectedDGV == gridBot)
            {
                Arr_bot = Matrix.Combine(Arr_bot, index);
                Tools.ArraytoGrid(gridBot, Arr_bot);                

            }
            else if (SelectedDGV == gridWeb)
            {
                Arr_web = Matrix.Combine(Arr_web, index);
                Tools.ArraytoGrid(gridWeb, Arr_web);

            }
            else if (SelectedDGV == gridCon)
            {
                Arr_con = Matrix.Combine(Arr_con, index);
                Tools.ArraytoGrid(gridCon, Arr_con);

            }


            if (SelectedDGV.Columns.Count <= 10)
                foreach (DataGridViewColumn c in SelectedDGV.Columns)
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    SelectedDGV.Height = SelectedDGV.Rows.Count * 23 + 32;
                }
            SelectedDGV.ClearSelection();



        }

        //OK
    }

    
}
