using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Mainform.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Provider;
using Classes;


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
            btGrid.BackColor = Color.FromArgb(44, 152, 93);
            btDim.BackColor = Color.FromArgb(44, 152, 93);
            btStif.BackColor = Color.FromArgb(44, 152, 93);            
            btBack.BackColor = Color.FromArgb(33, 115, 70);
            btApply.BackColor = Color.FromArgb(33, 115, 70);
            btNext.BackColor = Color.FromArgb(33, 115, 70);
            btMaterial.BackColor = Color.FromArgb(33, 115, 70);
            btAnalysis.BackColor = Color.FromArgb(33, 115, 70);

            gridAB.RowCount = 1;
            gridAB.Rows[0].Height = 30;
            cbType.SelectedIndex = 0;
            labelEx.Text = "Ex. For a bridges with 3 spans, the lengths are 30m, 40m, 30m, and distance from end-beam to";
            labelEx2.Text = "support point is 0.5m, input string should be: 0.5+30+40+30+0.5";

            labelCode1.Text = "(1) 도로교 설계기준 (한계상태설계법) 해설, (2015) - (사)한국교량및구조공화회∙교량설계핵심기술연구단";
            labelCode2.Text = "(2) 강구조설계기준 및 해설 (하중저항계수법), (2018) - (사)한국강구조학회";
            labelCode3.Text = "(3) AASHTO LRFD, (2017)";


            TabPage[] a = { pageGeneral };
            showtabpage(a);

            Setgridview(gridTop);
            Setgridview(gridBot);
            Setgridview(gridWeb);
            Setgridview(gridCon);
            Setgridview(gridRibtop);
            Setgridview(gridRibbot);
            Setgridview(gridStif);
            Setgridview(gridTranstif);

            pictureBox1.Load(Const.Constring + @"\Picture\Section.PNG");
            pictureBox2.Load(Const.Constring + @"\Picture\Section all.PNG");

            //Tag material
            txtMatname.Text = "Mat1";
            cbMattype.SelectedIndex = 0;
            numWs.Value = 75;
            numEs.Value = 210000;
            numG.Value = 81000;
            numFy.Value = 380;
            numFu.Value = 500;
            numWc.Value = 25;
            numFc.Value = 35;
            pictureMat.Load(Const.Constring + @"\Picture\Mat.PNG");

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


            TabPage[] a = { pageCross, pageGirder, pageStiffeners};
            showtabpage(a);

        }

        private void btGeneral_Click(object sender, EventArgs e)
        {

            TabPage[] a = { pageGeneral };
            showtabpage(a);


        }

        private void button1_Click(object sender, EventArgs e)
        {

            TabPage[] a = { pageMaterial };
            showtabpage(a);



        }

        private void btAnalysis_Click(object sender, EventArgs e)
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
            TabPage[] a = { pageCross, pageGirder, pageStiffeners };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageCross;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageCross, pageGirder, pageStiffeners };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageGirder;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageCross, pageGirder, pageStiffeners };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageStiffeners;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageGirder, pageMaterial, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageCross;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageGirder, pageMaterial, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageSectional;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageGirder, pageMaterial, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageSupports;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageGirder, pageMaterial, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageProp;
        }


        // Limit the input value in datagridview is only numec

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow only number and dot
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
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
                        TabPage[] a = { pageCross, pageGirder, pageStiffeners };
                        showtabpage(a);
                        metroTabControl1.SelectedTab = pageCross;
                        

                    }
                    break;


                case "pageCross":
                    {
                        
                        metroTabControl1.SelectedTab = pageGirder;
                    }
                    break;



                case "pageGirder":
                    {
                        
                        metroTabControl1.SelectedTab = pageStiffeners;


                    }
                    break;

                case "pageStiffeners":
                    {
                        TabPage[] a = { pageMaterial };
                        showtabpage(a);


                    }
                    break;

                case "pageMaterial":
                    {
                        TabPage[] a = { pageAnalysis };
                        showtabpage(a);


                    }
                    break;
            }


        }

        private void btBack_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageCross":
                    {
                        TabPage[] a = { pageGeneral };
                        showtabpage(a);
                    }
                    break;
                case "pageGirder":
                    {
                        
                        metroTabControl1.SelectedTab = pageCross;
                    }
                    break;
                case "pageStiffeners":
                    {
                        
                        metroTabControl1.SelectedTab = pageGirder;
                    }
                    break;
                case "pageMaterial":
                    {
                        TabPage[] a = { pageCross, pageGirder, pageStiffeners };
                        showtabpage(a);
                        metroTabControl1.SelectedTab = pageStiffeners;
                    }
                    break;
                case "pageAnalysis":
                    {
                        TabPage[] a = { pageMaterial };
                        showtabpage(a);
                    }
                    break;

            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(this.numgirder.Value);
        }


        static string DBstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Const.Constring + @"\PUS1.accdb";
        OleDbConnection con = new OleDbConnection(DBstring);


        double[,] Atop = new double[3, 1];
        double[,] Abot = new double[3, 1];
        double[,] Aweb = new double[3, 1];
        double[,] Acon = new double[2, 1];
        double[,] Aribtop = new double[4, 1];
        double[,] Aribbot = new double[4, 1];
        double[,] Astif = new double[4, 1];
        double[,] Atranstif;
        double[,] Atranstif_grid;

        double[,] Across;
        double[,] Across_grid;
        double[] Across1;
        
        double[,] Atran;        

        double[] Aspan;

        double sumspan;
        int ngirder;
        List<Node> Node;
        List<Node> Node1;

        private void btApply_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageGeneral":
                    {

                        ngirder = Convert.ToInt32(numgirder.Value);
                       
                        // Atran has 3 rows
                        // 1st row: length
                        //2nd row : order of sections - to set color
                        //3rd row: BeamID : main beam 1->5; stringer: 21 ->51
                        Atran = new double[3, ngirder + 1];
                        for (int i = 0; i < Atran.GetLength(1); i++)
                        {
                            if (i == 0 || i == Atran.GetLength(1) - 1)
                            {
                                Atran[1, i] = 0;
                                Atran[2, i] = 0;
                            }
                            else
                            {
                                Atran[1, i] = i;
                                Atran[2, i] = i;

                            }

                        }
                        gridTran.DataSource = null;
                        DGV.ArraytoGrid_tran(gridTran, Atran);
                        Deco(gridTran, Atran);


                        Aspan = Array.ConvertAll(txtSpan.Text.ToString().Split('+'), Double.Parse);
                        for (int i = 0; i < Aspan.GetLength(0); i++)
                            Aspan[i] = Aspan[i] * 1000;
                        sumspan = Aspan.Sum();

                        Atop = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Atop[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridTop, Atop);

                        Abot = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Abot[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridBot, Abot);

                        Aweb = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Aweb[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridWeb, Aweb);

                        Acon = new double[2, 1] { { 0 }, { 0 } };
                        Acon[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridCon, Acon);

                        Aribtop = new double[4, 1];
                        Aribtop[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridRibtop, Aribtop);

                        Aribbot = new double[4, 1];
                        Aribbot[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridRibbot, Aribbot);

                        Astif = new double[4, 1];
                        Astif[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridStif, Astif);

                        Atranstif = new double[1, 1];
                        Atranstif[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridTranstif, Atranstif);

                        // Convert 1D array Across and Across_grid
                        //Description about Across_grid
                        //Across_grid has 3 rows: 
                        //1st row = Type: 1 abut, 2 pier, 3 cross, 4 section changed
                        // 2nd row = order of section, to set different color for different sections
                        // 3rd row = length

                        Across_grid = new double[3, Aspan.GetLength(0)];
                        Across = new double[1, Aspan.GetLength(0)];
                        for (int i = 0; i < Aspan.GetLength(0); i++)
                        {
                            Across_grid[2, i] = Aspan[i];
                            Across_grid[0, i] = 2.0;
                            Across_grid[1, i] = i + 1;
                            Across[0, i] = Aspan[i];
                        }
                        Across_grid[0, 0] = 1.0;

                        gridCross.DataSource = null;
                        DGV.ArraytoGrid(gridCross, Across);
                        Deco(gridCross, Across_grid);
                        
                        

                    }
                    break;
                case "pageCross":
                    {
                        // Generate List of grid bridge
                        Node = Matrix.Gridarrtolist(Across_grid, Atran, ngirder);
                        
                        
                        //Write to Database
                        
                        //Access.writeList(Node, "Node",con, "All");

                        // Plot to the chart
                        Chart.Bridgegrid(Node, gridchart);

                        //Fill the transverse stiffener grid
                        Atranstif = Across;
                        Atranstif_grid = Across_grid;
                        Across1 = new double[Across.GetLength(1)];
                        for (int i = 0; i < Atranstif_grid.GetLength(1); i++)
                        {
                            Atranstif_grid[1, i] = i + 1;
                            Atranstif_grid[0, i] = 1;
                            Across1[i] = Across[0, i];
                        }
                            
                       
                        
                        DGV.ArraytoGrid(gridTranstif, Atranstif);


                    }
                    break;
                
                case "pageGirder":
                    {
                        //Select node without type 4 again
                        
                        Node = Node.Where(p => p.Type != 4).ToList();

                        //Insert node
                        Node = Matrix.Addpoint(Node, Atop);
                        Node = Matrix.Addpoint(Node, Abot);
                        Node = Matrix.Addpoint(Node, Aweb);
                        Node = Matrix.Addpoint(Node, Acon);

                        //Insert top, bottom flange, web, bottom concrete
                        Node = Matrix.Addprop(Node, Atop, "btop,ttop");
                        Node = Matrix.Addprop(Node, Abot, "bbot,tbot");
                        Node = Matrix.Addprop(Node, Aweb, "D,tw");
                        Node = Matrix.Addprop(Node, Acon, "Hc");

                        //Insert others to Node

                        double [] S = new double[] { radioRa.Checked == Enabled ? Convert.ToDouble(numSr.Value) : Math.Tan(Convert.ToDouble(numSd.Value) * Math.PI / 180.0) };
                        decimal[] ts = new decimal[] {numcbot.Value, numctop.Value, numts.Value, numth.Value, numbh.Value, numdrt.Value, numart.Value, numcrt.Value, numdrb.Value, numarb.Value, numcrb.Value };
                        double[] ts1 = Array.ConvertAll(ts, p => (double)p);
                        Node = Matrix.Add1prop(Node, ts1, "cbot,ctop,ts,th,bh,drt,art,crt,drb,arb,crb");
                        Node = Matrix.Add1prop(Node, S, "S");

                        //Write to DB
                        //Access.writeList(Node, "Node", con, "All");

                       

                    }
                    break;
                
                case "pageStiffeners":
                    {
                        //Select node without type 5 again
                        
                        Node = Node.Where(p => p.Type != 5).ToList();

                        //Insert node
                        Node = Matrix.Addpoinstiff(Node, Aribtop);
                        Node = Matrix.Addpoinstiff(Node, Aribbot);
                        Node = Matrix.Addpoinstiff(Node, Astif);

                        Node = Matrix.Addprop(Node, Aribtop, "nst,Hst,tst");
                        Node = Matrix.Addprop(Node, Aribbot, "nsb,Hsb,tsb");
                        Node = Matrix.Addprop(Node, Astif, "ns,ds1,ds2");

                        Node = Matrix.Addd0(Node, Across, "Lp");
                        Node = Matrix.Addd0(Node, Atranstif, "d0");
                        //Write to DB
                        Access.writeList(Node, "Node", con, "All");
                    }
                    break;



            }
        }


        public void Deco(DataGridView dgv, double[,] arr)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (arr[1, i] == 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(146, 205, 220);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(146, 205, 220);
                }
                else if (arr[1, i] % 2 == 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);
                }

                else
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;

                }
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

            if (a == gridCross)
            {
                divideTool.Enabled = true;
                addTool.Enabled = true;
                if (Across_grid[0, index] == 1 || Across_grid[0, index] == 2)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }
            else if (a == gridTranstif)
            {
                divideTool.Enabled = true;
                addTool.Enabled = true;
                if (Atranstif_grid[0, index] == 1)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }

            else if (a == gridTran)
            {
                divideTool.Enabled = false;
                if (Atran[2, index] < 10)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;

                if (Atran[2, index] == 0)                
                    addTool.Enabled = false; 
                else
                    addTool.Enabled = true;
            }

            else
            {
                divideTool.Enabled = true;
                addTool.Enabled = true;
                if (index == a.ColumnCount - 1)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }
            SelectedDGV = a;
        }



        private void addTool_Click(object sender, EventArgs e)
        {
            ndiv = 2;
            if (SelectedDGV == gridTop)
            {
                Atop = Matrix.Seperate(Atop, index,ndiv);
                DGV.ArraytoGrid(gridTop, Atop);
            }

            else if (SelectedDGV == gridBot)
            {
                Abot = Matrix.Seperate(Abot, index, ndiv);
                DGV.ArraytoGrid(gridBot, Abot);
            }
            else if (SelectedDGV == gridWeb)
            {
                Aweb = Matrix.Seperate(Aweb, index, ndiv);
                DGV.ArraytoGrid(gridWeb, Aweb);
            }
            else if (SelectedDGV == gridCon)
            {
                Acon = Matrix.Seperate(Acon, index, ndiv);
                DGV.ArraytoGrid(gridCon, Acon);
            }
            else if (SelectedDGV == gridCross)
            {
                Across = Matrix.Seperate_cross(Across, index,ndiv);
                Across_grid = Matrix.Seperate_cross(Across_grid, index,ndiv);
                DGV.ArraytoGrid(gridCross, Across);
                Deco(SelectedDGV, Across_grid);
            }
            else if (SelectedDGV == gridTranstif)
            {
                Atranstif = Matrix.Seperate_cross(Atranstif, index, ndiv);
                Atranstif_grid = Matrix.Seperate_cross(Atranstif_grid, index, ndiv);
                DGV.ArraytoGrid(gridTranstif, Atranstif);
                Deco(SelectedDGV, Atranstif_grid);
            }
            else if (SelectedDGV == gridTran)
            {               
                Atran = Matrix.Seperate_tran(Atran, index);
                DGV.ArraytoGrid_tran(gridTran, Atran);
                Deco(SelectedDGV, Atran);

            }
            else if (SelectedDGV == gridRibtop)
            {
                Aribtop = Matrix.Seperate(Aribtop, index, ndiv);
                DGV.ArraytoGrid(gridRibtop, Aribtop);
            }
            else if (SelectedDGV == gridRibbot)
            {
                Aribbot = Matrix.Seperate(Aribbot, index, ndiv);
                DGV.ArraytoGrid(gridRibbot, Aribbot);
            }
            else if (SelectedDGV == gridStif)
            {
                Astif = Matrix.Seperate(Astif, index, ndiv);
                DGV.ArraytoGrid(gridStif, Astif);
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
                Atop = DGV.GridtoArray(gridTop);
                Atop = Matrix.Update(Atop, sumspan);
                DGV.ArraytoGrid(gridTop, Atop);
                gridTop.MultiSelect = false;
            }
            else if (sender == gridBot)
            {
                Abot = DGV.GridtoArray(gridBot);
                Abot = Matrix.Update(Abot, sumspan);
                DGV.ArraytoGrid(gridBot, Abot);
                gridBot.MultiSelect = false;
            }
            else if (sender == gridWeb)
            {
                Aweb = DGV.GridtoArray(gridWeb);
                Aweb = Matrix.Update(Aweb, sumspan);
                DGV.ArraytoGrid(gridWeb, Aweb);
                gridWeb.MultiSelect = false;
            }
            else if (sender == gridCon)
            {
                Acon = DGV.GridtoArray(gridCon);
                Acon = Matrix.Update(Acon, sumspan);
                DGV.ArraytoGrid(gridCon, Acon);
                gridCon.MultiSelect = false;
            }
            else if (sender == gridCross)
            {
                Across = DGV.GridtoArray(gridCross);
                for (int i = 0; i < Across.GetLength(1); i++)
                    Across_grid[2, i] = Across[0, i];
                Across_grid = Matrix.Update_cross(Across_grid, Aspan);
                for (int i = 0; i < Across.GetLength(1); i++)
                    Across[0, i] = Across_grid[2, i];
                DGV.ArraytoGrid(gridCross, Across);
                gridCross.MultiSelect = false;
            }
            else if (sender == gridTranstif)
            {
                Atranstif = DGV.GridtoArray(gridTranstif);
                for (int i = 0; i < Atranstif.GetLength(1); i++)
                    Atranstif_grid[2, i] = Atranstif[0, i];
                Atranstif_grid = Matrix.Update_cross(Atranstif_grid, Across1); //??
                for (int i = 0; i < Atranstif.GetLength(1); i++)
                    Atranstif[0, i] = Atranstif_grid[2, i];
                DGV.ArraytoGrid(gridTranstif, Atranstif);
                gridTranstif.MultiSelect = false;
            }
            else if (sender == gridTran)
            {
                Atran = DGV.GridtoArray_tran(gridTran,Atran);
            }
            else if (sender == gridRibtop)
            {
                Aribtop = DGV.GridtoArray(gridRibtop);
                Aribtop = Matrix.Update(Aribtop, sumspan);
                DGV.ArraytoGrid(gridRibtop, Aribtop);
                gridRibtop.MultiSelect = false;
            }
            else if (sender == gridRibbot)
            {
                Aribbot = DGV.GridtoArray(gridRibbot);
                Aribbot = Matrix.Update(Aribbot, sumspan);
                DGV.ArraytoGrid(gridRibbot, Aribbot);
                gridRibbot.MultiSelect = false;
            }
            else if (sender == gridStif)
            {
                Astif = DGV.GridtoArray(gridStif);
                Astif = Matrix.Update(Astif, sumspan);
                DGV.ArraytoGrid(gridStif, Astif);
                gridStif.MultiSelect = false;
            }
            //gridTop.Rows[row+1].Cells[col].Selected = true;
        }

        private void deleteTool_Click(object sender, EventArgs e)
        {

            if (SelectedDGV == gridTop)
            {
                Atop = Matrix.Combine(Atop, index);
                DGV.ArraytoGrid(gridTop, Atop);


            }
            else if (SelectedDGV == gridBot)
            {
                Abot = Matrix.Combine(Abot, index);
                DGV.ArraytoGrid(gridBot, Abot);

            }
            else if (SelectedDGV == gridWeb)
            {
                Aweb = Matrix.Combine(Aweb, index);
                DGV.ArraytoGrid(gridWeb, Aweb);

            }
            else if (SelectedDGV == gridCon)
            {
                Acon = Matrix.Combine(Acon, index);
                DGV.ArraytoGrid(gridCon, Acon);

            }
            else if (SelectedDGV == gridCross)
            {
                Across = Matrix.Combine_cross(Across, index);
                Across_grid = Matrix.Combine_cross(Across_grid, index);
                DGV.ArraytoGrid(gridCross, Across);
                Deco(SelectedDGV, Across_grid);
            }

            else if (SelectedDGV == gridTranstif)
            {
                Atranstif = Matrix.Combine_cross(Atranstif, index);
                Atranstif_grid = Matrix.Combine_cross(Atranstif_grid, index);
                DGV.ArraytoGrid(gridTranstif, Atranstif);
                Deco(SelectedDGV, Atranstif_grid);
            }
            else if (SelectedDGV == gridTran)
            {
                Atran = Matrix.Combine_tran(Atran, index);
                DGV.ArraytoGrid_tran(gridTran, Atran);
                Deco(SelectedDGV, Atran);
            }
            else if (SelectedDGV == gridRibtop)
            {
                Aribtop = Matrix.Combine(Aribtop, index);
                DGV.ArraytoGrid(gridRibtop, Aribtop);


            }
            else if (SelectedDGV == gridRibbot)
            {
                Aribbot = Matrix.Combine(Aribbot, index);
                DGV.ArraytoGrid(gridRibbot, Aribbot);


            }
            else if (SelectedDGV == gridStif)
            {
                Astif = Matrix.Combine(Astif, index);
                DGV.ArraytoGrid(gridStif, Astif);


            }

            if (SelectedDGV.Columns.Count <= 10)
                foreach (DataGridViewColumn c in SelectedDGV.Columns)
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    SelectedDGV.Height = SelectedDGV.Rows.Count * 23 + 32;
                }
            SelectedDGV.ClearSelection();



        }

        int ndiv;
        private void divideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAddmore f = new fAddmore();
            if (f.ShowDialog() == DialogResult.OK)
            {
                ndiv = f.ndiv;
                if (SelectedDGV == gridTop)
                {
                    Atop = Matrix.Seperate(Atop, index, ndiv);
                    DGV.ArraytoGrid(gridTop, Atop);
                }

                else if (SelectedDGV == gridBot)
                {
                    Abot = Matrix.Seperate(Abot, index, ndiv);
                    DGV.ArraytoGrid(gridBot, Abot);
                }
                else if (SelectedDGV == gridWeb)
                {
                    Aweb = Matrix.Seperate(Aweb, index, ndiv);
                    DGV.ArraytoGrid(gridWeb, Aweb);
                }
                else if (SelectedDGV == gridCon)
                {
                    Acon = Matrix.Seperate(Acon, index, ndiv);
                    DGV.ArraytoGrid(gridCon, Acon);
                }
                else if (SelectedDGV == gridCross)
                {
                    Across = Matrix.Seperate_cross(Across, index,ndiv);
                    Across_grid = Matrix.Seperate_cross(Across_grid, index,ndiv);
                    DGV.ArraytoGrid(gridCross, Across);
                    Deco(SelectedDGV, Across_grid);
                }
                else if (SelectedDGV == gridTranstif)
                {
                    Atranstif = Matrix.Seperate_cross(Atranstif, index, ndiv);
                    Atranstif_grid = Matrix.Seperate_cross(Atranstif_grid, index, ndiv);
                    DGV.ArraytoGrid(gridTranstif, Atranstif);
                    Deco(SelectedDGV, Atranstif_grid);
                }
                else if (SelectedDGV == gridTran)
                {
                    Atran = Matrix.Seperate_tran(Atran, index);
                    DGV.ArraytoGrid_tran(gridTran, Atran);
                    Deco(SelectedDGV, Atran);

                }
                else if (SelectedDGV == gridRibtop)
                {
                    Aribtop = Matrix.Seperate(Aribtop, index, ndiv);
                    DGV.ArraytoGrid(gridRibtop, Aribtop);
                }
                else if (SelectedDGV == gridRibbot)
                {
                    Aribbot = Matrix.Seperate(Aribbot, index, ndiv);
                    DGV.ArraytoGrid(gridRibbot, Aribbot);
                }
                else if (SelectedDGV == gridStif)
                {
                    Astif = Matrix.Seperate(Astif, index, ndiv);
                    DGV.ArraytoGrid(gridStif, Astif);
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
        }

        string Scheck;
        private void gridchart_DataClick(object sender, ChartPoint chartPoint)
        {
            var asPixels = gridchart.Base.ConvertToPixels(chartPoint.AsPoint());
            fRestrain f = new fRestrain();
            f.Location = new Point(Cursor.Position.X - f.Size.Width / 2, Cursor.Position.Y - f.Size.Height);
            if (f.ShowDialog() == DialogResult.OK )
            {
                Scheck = f.Scheck;
                if (Scheck != "" && Scheck != null)
                    Node.Where(p => p.X == chartPoint.X && p.Y == chartPoint.Y).ToList().ForEach(i => i.Restrain = Scheck);

                Chart.Bridgegrid(Node, gridchart);
                // MessageBox.Show(Scheck);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMattype.SelectedIndex == 0)
            {                
                groupSteel1.Visible = false;
                groupSteel2.Visible = false;
                groupSteel3.Visible = false;
                checkSteel.Enabled = false;
                comboSteel.Enabled = false;
                groupConcrete.Visible = true;

            }

            else
            {                
                groupSteel1.Visible = true;
                groupSteel2.Visible = true;
                groupSteel3.Visible = false;
                groupConcrete.Visible = false;
                checkSteel.Enabled = true;               
                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (fcktofcm.ContainsKey(Convert.ToDouble(numFc.Value)))
                    numEc.Value = Convert.ToDecimal(0.077 * Math.Pow(2500, 1.5) * Math.Pow(fcktofcm[Convert.ToDouble(numFc.Value)], (1 / 3.0)));
                else
                {
                    MessageBox.Show("Please input f'c again");
                    checkBox1.Checked = false;
                    numEc.Value = 0;
                }                    
            }
        }

        private static Dictionary<double, double> fcktofcm = new Dictionary<double, double>()
        {
            { 18, 22 }, { 21, 25 }, { 24, 28 }, { 27, 31 }, { 30, 34 }, { 35, 39 }, { 40, 44 }, { 50, 56 }, { 60, 66 }, { 70, 76 }, { 80, 86 }, { 90, 96 }
        };

        private void checkSteel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSteel.Checked == true)
            {
                comboSteel.Enabled = true;
                groupSteel2.Visible = false;
                groupSteel3.Visible = true;
                BindingSource bs = new BindingSource();
                bs.DataSource = new List<string> { "SS235", "SS275", "SM275", "SM355", "SM420", "SM460", "HSB380", "HSB460", "HSB690" };
                comboSteel.DataSource = bs;
            }                

            else
            {
                comboSteel.Enabled = false;
                groupSteel2.Visible = true;
                groupSteel3.Visible = false;
            }
                
        }












        //OK
    }


}
